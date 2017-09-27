class ArtistProfileController {
	constructor($ArtistProfileService, $stateParams, $http, $filepicker, $state, $uibModal) {
		this.$ArtistProfileService = $ArtistProfileService;
		this.$state = $state;
		this.$http = $http;
		//this.name = $stateParams["name"];
		this.id = $stateParams["id"];
		this.postid = $stateParams["postid"];
		sessionStorage.setItem("id", this.id);
		this.user = sessionStorage.getItem("userid");
		//this.route = "api/Posts";
        this.artistId = sessionStorage.getItem("id");
        this.posts;
        this.number;
		this.post = {
			ApplicationArtistId: this.posts,
			ApplicationUserId: this.user,
			DateCreated: new Date(),
			Media: "",
            Caption: "",
            Video: "",
			Type: ""
		};
		this.getArtist();
		this.getPostId();
        this.likes = {};
        this.artists;

        this.getComments();
        this.comment;
        this.$uibModal = $uibModal;      
   
		//this.getlastfm();
		
		//this.getNumberOfLikes();

	}    

	likePost(postId) {
		this.$http.post("api/Likes/", { DateLiked: new Date(), UserId: this.user, PostId: postId })
			.then((res) => {
				this.$state.reload();
			});
	}

    findPostId() {
        this.$http.get("api/Posts/" + this.post.ApplicationArtistId)
            .then((res) => {
                this.posts = res.data;
                //console.log("postdata" + res.date);
              

            });
	}

	getArtist() {
		this.$ArtistProfileService.getArtist(this.id)
			.then((res) => {
				this.artist = res.data;
				//console.log(this.artist);
			});
	}

	//getPostId() {
	//	this.$http.get("api/Posts/" + this.artistId)
	//		.then((res) => {
	//			this.posts = res.data;
 //               console.log(res.data);
 //               //console.log(this.posts.media);

 //           });
	//}

    getPostId() {
        this.$http.get("api/Posts/artist/" + this.artistId)
            .then((res) => {
                this.posts = res.data;
                console.log(res.data);
                //console.log(this.posts.media);

            });
    }

	getNumberOfLikes() {
		this.$http.get("api/Likes/numberlikes/" + { PostId: this.postId })
			.then(res => {
				this.like = res.data;
				//console.log("amount of Likes " + this.post.numberOfLikes);
			});
	}

    showModalPost() {
        this.$uibModal.open({
            templateUrl: '/ngApp/views/modalPost.html',
            controller: ModalPostController,
            controllerAs: 'controller',
            resolve: {
                post: () => this.post
            }
        }).closed.then(() => {
           // this.addPost();
        });
    }

    getComments() {
        this.$http.get("api/Comments/")
            .then(res => {
                this.comments = res.data;
                console.log(res.data);
            });
    }
    showModalComments(postId) {
        this.$uibModal.open({
            templateUrl: '/ngApp/views/modalComments.html',
            controller: ModalCommentController,
            // controller: controller,
            controllerAs: 'controller',
            resolve: {
                postId: postId,     // JLT: this will get passed to the postId param in the constructor of ModalCommentController
                comment: () => this.comment
            }
        }).closed.then(() => {
            // this.addPost();
        });
    }

    AddComment(postId, text) {
        this.$http.post("api/Comments", { PostId: postId, Text: text, UserId: this.user })
            .then((res) => {

                this.$state.reload();
                //console.log("comments");
            });

    }


}

   	//getlastfm() {
  	//getlastfm() {

	//	this.$http.get("http://ws.audioscrobbler.com/2.0/?method=chart.gettopartists&api_key=87bdb2c24f5d7ea2e34ac5d1bdc419f1&format=json&limit=1000")
	//		.then((res) => {
	//			this.artists = res.data;
	//			console.log(this.artists);
	//			this.$http.post("api/artists", this.artists);
	//		});
	//}



class ModalPostController {
    constructor($ArtistProfileService, $stateParams, $http, $filepicker, $state, $uibModalInstance) {

        this.$http = $http;
        this.id = $stateParams["id"];
        sessionStorage.setItem("id", this.id);
        this.user = sessionStorage.getItem("userid");
        this.filepicker = $filepicker;
        this.filepicker.setKey('Aowd5dVQ06CyRYPl9EaAVz');
        this.artistId = sessionStorage.getItem("id");

        this.post = {
            ApplicationArtistId: this.artistId,
            ApplicationUserId: this.user,
            DateCreated: new Date(),
            Media: "",
            Caption: "",
            Video: "",
            Type: ""
        };
        this.$state = $state;
        this.modal = $uibModalInstance;
        this.aps = $ArtistProfileService;
        this.file;
        this.video;


	}

    getPostId() {
        this.$http.get("api/Posts/" + this.posts)
            .then((res) => {
                this.posts = res.data;
                console.log("postdata" + this.posts.id);
            });
    }
    addPost(media) {
        console.log("addPost");
        this.post.Media = media;
        console.log(this.post);
        this.$http.post("api/Posts", this.post)
            .then((res) => {
                this.getPostId();
                this.$state.reload();
                console.log("after put");
            });
    }

    pickFile() {
        this.filepicker.pick(
            {
                cropRatio: 1 / 1,
                mimetype: 'image/*',
                container: 'window',
                imageQuality: 80,
                conversions: ['crop', 'rotate']
            },
            this.fileUploaded.bind(this)
        );
    }
    fileUploaded(file) {
        this.file = file;
        this.post.Type = "image";
        console.log(this.file.url);
        return this.file.url;
    }

    addVideoPost(video , caption) {
        console.log("addVideoPost");
        this.post.Video = video;
        this.post.Caption = caption;
        this.post.Type = 'video';
        console.log(this.post);
        this.$http.post("api/Posts", this.post)
            .then((res) => {
                this.getPostId();
                this.$state.reload();
                console.log("after put");
                this.modal.close();
            });
    }
    pickVideo() {
        this.filepicker.pick(
            {
                mimetype: 'video/*',
                container: 'window'
            },
            this.videoUpload.bind(this)
        );
    }
    videoUpload(video) {
        this.file = video;
        this.post.Type = "video";
        console.log(this.post.Type);
        //return this.video.url;
    }
    addCaption(caption) {
        console.log("addCaption");
        this.post.Caption = caption;
        console.log(this.post);
        this.$http.post("api/Posts", this.post)
            .then((res) => {
                this.getPostId();
                this.$state.reload();
                console.log("after put");
                this.modal.close();
            });
    }

    savePost(caption) {
        this.post.Media = this.file.url;
        this.post.Caption = caption;
        this.aps.savePost(this.post)
            .then(() => {
                this.$state.reload();
                this.modal.close();
            });
    }
    closeModal() {
        this.modal.dismiss();
    }
 }


 
//class ModalCommentController {
//    constructor($stateParams, $http, $state, $uibModalInstance) {
//        this.$http = $http;
//        this.$state = $state;
//        this.modal = $uibModalInstance;
//        this.id = $stateParams["id"];
//        sessionStorage.setItem("otherid", this.id);
//        this.otherid = sessionStorage.getItem("otherid");
//        this.post = sessionStorage.getItem("postid");
//        this.user = sessionStorage.getItem("userid");
//        this.comment;
//        this.getComments();
//    }
//    getComments(postId, text) {
//        this.$http.get("api/Comments", { PostId: postId, Text: text, UserId: this.user })
//            .then(res => {
//                this.comment = res.data;
//                console.log(res.data);
//            });
//    }
//}
