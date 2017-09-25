class HomeFeedController {
    constructor($http, $stateParams, $state, $uibModal) {
        this.$http = $http;
        this.$state = $state;
		this.id = $stateParams["id"];
		sessionStorage.setItem("otherid", this.id);
		this.otherid = sessionStorage.getItem("otherid");
        this.user = sessionStorage.getItem("userid");
        this.getPostWithProfile();
        this.like = {
            UserId: this.id,
            PostId: this.post
        };
        this.getComments();
		this.$uibModal = $uibModal;
		this.postId = $stateParams["postId"];
		
    }

    getPostWithProfile() {
        this.$http.get("api/UserFollowers/postandprofile/" + this.user)
            .then(res => {
                this.post = res.data;
                console.log(res.data);
            });
	}

	likePost(postId) {
		this.$http.post("api/Likes/", { DateLiked: new Date(), UserId: this.user, PostId: postId })
			.then((res) => {

			});
	}
    AddComment(postId, text) {
        this.$http.post("api/Comments", { PostId: postId, Text: text, UserId: this.user })
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
                comment : () => this.comment
            }
        }).closed.then(() => {
            // this.addPost();
        });
    }
}
 
class ModalCommentController {
	// JLT: adding postId as a param of this constructor.  postId is passed via the 'resolve' property above
	constructor(postId, $stateParams, $http, $state, $uibModalInstance) {
		this.postId = postId;
		this.$http = $http;
		this.$state = $state;
		this.modal = $uibModalInstance;
		this.getComment();
	}

	getComment() {
		this.$http.get("api/Comments/" + this.postId)
			.then(res => {
				this.comment = res.data;
			});
	}
}
