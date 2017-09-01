class ArtistProfileController {
	constructor($ArtistProfileService, $stateParams, $http, $filepicker) {
		this.$ArtistProfileService = $ArtistProfileService;
		this.$http = $http;
		//this.name = $stateParams["name"];
		this.id = $stateParams["id"];
        sessionStorage.setItem("id", this.id);
        this.user = sessionStorage.getItem("userid");
        //this.route = "api/Posts";
        this.posts = sessionStorage.getItem("id");
        this.post = {
            ApplicationArtistId: this.posts,
            ApplicationUserId: this.user,
            DateCreated: new Date(),
            Media: ""
        };
        
		this.getArtist();
        this.getPostId();
        this.file;
        this.filepicker = $filepicker;
		this.filepicker.setKey('AdqhVmjnDSXuLRRPEfvdbz'); 


	}
	getArtist() {
		this.$ArtistProfileService.getArtist(this.id)
			.then((res) => {
				this.artist = res.data;
				console.log(res.data);
			});
	}

	getPostId() {
		this.$http.get("api/Posts/" +  this.posts)
			.then((res) => {
				this.posts = res.data;
				console.log("postdata" + res.data);
			});
    }
    addPost(media ) {
        console.log("addPost");


        //this.post.ApplicationArtistid = this.id;
        //this.post.ApplicationUserId = artistid;
      

         this.post.Media = media;

        console.log(this.post);
        this.$http.post("api/Posts", this.post)
            .then((res) => {
              
                this.getPostId();
                console.log("after put")
                //this.addPost();
                //this.location.path('/userProfile');
              
            });
    }
	//im

    pickFile() {
        this.filepicker.pick(
            {
                mimetype: 'image/*',
                imageQuality: 60
            },
            this.fileUploaded.bind(this)
        );
    }

    fileUploaded(file) {
        // save file url to database
        this.file = file;
        console.log(this.file.url);
        console.log(this);
        /*   this.$scope.$apply(); */// force page to update
        /*this.file.url; */ //change 'item' to imageUrl property
    }
}