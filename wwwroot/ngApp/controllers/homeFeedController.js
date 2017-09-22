class HomeFeedController {
    constructor($http, $stateParams, $state, $uibModal) {
        this.$http = $http;
        this.$state = $state;
        this.id = $stateParams["id"];
        sessionStorage.setItem("otherid", this.id);
        this.otherid = sessionStorage.getItem("otherid");
        this.post = sessionStorage.getItem("postid");
        this.user = sessionStorage.getItem("userid");
        //this.allPosts = {
        //    PostId: this.post,
        //    ApplicationArtistId: this.posts,
        //    FirstNameOfPersonWhoPosted: "",
        //    LastNameOfPersonWhoPosted: "",
        //    ArtistName:"",
        //    FollowingUserdId: this.user,
        //    DateCreated: new Date(),
        //    Media: "",
        //    Video: "",
        //    Caption: "",
        //    ProfileImage:""
        //};
        this.getPostWithProfile();
        this.like = {
            UserId: this.id,
            PostId: this.post
        };
        this.comment;
        this.cid = $stateParams["id"];
        sessionStorage.setItem("commentId", this.cid);
        this.commentId = sessionStorage.getItem("commentId");
        this.getComments();
        this.$uibModal = $uibModal;
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
    getComments(commentId) {
        this.$http.get("api/Comments", { CommentId: commentId })
            .then(res => {
                this.comments = res.data;
                console.log(res.data);
            });
    }
    showModalComments() {
        this.$uibModal.open({
            templateUrl: '/ngApp/views/modalComments.html',
            controller: ModalCommentController,
            controllerAs: 'controller',
            resolve: {
                comment : () => this.comment
            }
        }).closed.then(() => {
            // this.addPost();
        });
    }
}
 
class ModalCommentController {
    constructor($stateParams, $http, $state, $uibModalInstance) {
        this.$http = $http;
        this.$state = $state;
        this.modal = $uibModalInstance;
        this.id = $stateParams["id"];
        sessionStorage.setItem("otherid", this.id);
        this.otherid = sessionStorage.getItem("otherid");
        this.post = sessionStorage.getItem("postid");
        this.user = sessionStorage.getItem("userid");
        this.cid = $stateParams["id"];
        sessionStorage.setItem("commentId", this.cid);
        this.commentId = sessionStorage.getItem("commentId");
        this.comments;
        this.comment = {
            CommentId: this.comments,
            UserId: this.otherid,
            PostId: this.post
        }
        this.findCommentId();
        //this.getComments();

        
    }
    //getComments(commentId) {
    //    this.$http.get("api/Comments", { CommentId : commentId })
    //        .then(res => {
    //            this.comments = res.data;
    //            console.log(res.data);
    //        });
    //}
    findCommentId() {
        this.$http.get("api/Comments/" + this.commentId)
            .then((res) => {
                this.comments = res.data;
                console.log("postdata" + res.date);
            });
    }
}
