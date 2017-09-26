class UserProfileController {
    constructor($UserProfileService, $http, $state, $uibModal) {
		this.$state = $state;
		this.$UserProfileService = $UserProfileService;
		this.$http = $http;
		this.email = sessionStorage.getItem("email");
        this.userid = sessionStorage.getItem("userid");
		this.getPost();
		this.getUserProfile();
		this.user;
        this.getFollowInfo();
		this.getNumberOfPosts();  
        this.postinfo;
        this.getComments();
        this.$uibModal = $uibModal;  
    }

	likePost(postId) {
		this.$http.post("api/Likes/", { DateLiked: new Date(), UserId: this.user.userId, PostId: postId })
			.then((res) => {

			});
	}

	getFollowInfo() {
		this.$http.get("api/UserFollowers/" + this.userid)
            .then(res => {
                this.postinfo = res.data;
                
                console.log(this.userid.numberOfFollowing);
            });
    }
    
    getNumberOfPosts() {
		this.$http.get("api/posts/numberOfPosts/" + this.userid)
            .then(res => {
                this.post = res.data;
                console.log("amount of Posts " + this.post.numberOfPosts);
            });
    }

    getUserProfile() {

        this.$UserProfileService.getUserProfile(this.email)
            .then((res) => {               
                this.user = res.data;
             
				console.log(res.data);
            });       
    } 
	getPost() {
		this.$http.get("api/Posts/" + this.userid)
			.then(res => {
				this.posts = res.data;
				console.log(res.data);
			});
	}

	deletePost(postid) {
		this.$http.delete("api/Posts/" + postid)
			.then((res) => {
				this.postid = res.data;
				this.$state.reload();
			});
    }
	AddComment(postId, text) {
		this.$http.post("api/Comments", { PostId: postId, Text: text, UserId: this.userid })
            .then((res) => {

                this.$state.reload();
                console.log("comments");
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

   
