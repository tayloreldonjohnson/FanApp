class HomeFeedController {
    constructor($http, $stateParams) {
        this.$http = $http;
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
    }

    getPostWithProfile() {
        this.$http.get("api/UserFollowers/postandprofile/" + this.user)
            .then(res => {
                this.post = res.data;
                console.log(res.data);
            });





    }
}