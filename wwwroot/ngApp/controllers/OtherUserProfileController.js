class OtherUserProfileController {
    constructor($stateParams, $http, $state, $templateCache, $uibModal) {
        this.$templateCache = $templateCache;
        this.$state = $state;
        this.$http = $http;
        this.email = $stateParams["email"];
        this.followeduser;
        this.id = $stateParams["id"];
        sessionStorage.setItem("otherid", this.id);
        this.userid = sessionStorage.getItem("userid");
        this.otherid = sessionStorage.getItem("otherid");
        this.getNumberOfPosts();
        this.userfollower;
        this.userfollowerinfo;
        this.getFollowInfo();
        this.isFollowing();
        this.isfollowing;
        this.inbox = {

            DateCreated: new Date(),
            MessagerUserId: this.userid,
            RecieverOfMessageId: this.otherid

        };
        this.getOtherUserProfile();
        this.userfollower = {
            FollowingUserId: this.userid,
            FollowedUserId: this.otherid
        };
        this.getComments();
        this.comment;
        this.$uibModal = $uibModal;  
	}

	likePost(postId) {
		this.$http.post("api/Likes/", { DateLiked: new Date(), UserId: this.user.userId, PostId: postId })
			.then((res) => {

			});
	}

    addFollower() {
        this.$http.post("api/UserFollowers", this.userfollower)
            .then(res => {
                this.followeduser = res.data;
				console.log(res.data);
				this.$state.reload();
            });
        //this.$templateCache.removeAll();
        //this.state.reload();
    }
        getFollowInfo() {
			this.$http.get("api/UserFollowers/" + this.otherid)
                .then(res => {
                    //posts #1 by userid
					this.posts = res.data;
					console.log(this.posts);
                    console.log(this.posts.numberOfFollowing);
                });
		}
        deleteFollower() {
            this.$http.delete("api/UserFollowers/unfollow/" + this.otherid + "/" + this.userid)
                .then(res => {
                    console.log(res.data);
					this.$state.reload();
                });
        }


        getNumberOfPosts() {
            this.$http.get("api/posts/numberOfPosts/" + this.otherid)
                .then(res => {
                    this.post = res.data;
                    console.log("amount of Posts " + this.post.numberOfPosts);
                });
		}

        getOtherUserProfile() {
            this.$http.get("api/Users/email/" + this.id)
                .then((res) => {
                    this.user = res.data;
                    console.log(res.data);
                });
        }
     
           isFollowing() {
            
               this.$http.get("api/UserFollowers/isFollowing/" + this.otherid + "/" + this.userid)
                   .then((res) => {
                       this.isfollowing = res.data;
                       console.log(res.data);
                       console.log("after put");
                   });
        }
           AddComment(postId, text) {
               this.$http.post("api/Comments", { PostId: postId, Text: text, UserId: this.userid })
                   .then((res) => {

                       this.$state.reload();
                       console.log("comments");
                   });

           }
           showModal() {
               this.$uibModal.open({
                   templateUrl: '/ngApp/views/modalMessages.html',
                   controller: ModalController,
                   controllerAs: 'controller',
                   resolve: {
                       message: () => this.inbox
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
     }
        
class ModalController {
    constructor($stateParams, $http, $state, $uibModalInstance) {
        this.$http = $http;
        this.id = $stateParams["id"];
        sessionStorage.setItem("otherid", this.id);
        this.userid = sessionStorage.getItem("userid");
        this.otherid = sessionStorage.getItem("otherid");
        this.inbox = {
            Message: "",
            DateCreated: new Date(),
            MessagerUserId: this.userid,
            RecieverOfMessageId: this.otherid

        };
        this.modal = $uibModalInstance;
    }
    InboxUser(message) {
        console.log("addMessage");
        this.inbox.Message = message;
        console.log(this.post);
        this.$http.post("api/Inboxes", this.inbox)
            .then((res) => {
				console.log("after put");
				this.modal.close();
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
