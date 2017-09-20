class HomeFeedController {
    constructor($http, $stateParams) {
        this.$http = $http;
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
    }

    getPostWithProfile() {
        this.$http.get("api/UserFollowers/postandprofile/" + this.user)
            .then(res => {
                this.post = res.data;
                console.log(res.data);
            });
    }
    likePost(post) {
        console.log("likePost");
        this.like.Post = post;
        console.log(this.post);
        this.$http.post("api/Likes" , this.like)
            .then(res => {
                console.log("after put");
            });
    }
}