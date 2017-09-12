class HomeFeedController{
    constructor($http, $stateParams) {
        this.$http = $http;
        this.posts = sessionStorage.getItem("id");
        this.user = sessionStorage.getItem("userId");
        this.post = {
            ApplicationArtistId: this.posts,
            ApplicationUserId: this.user,
            DateCreated: new Date(),
            Media: ""
        };
        this.getPosts();
        this.getUsers();
    }
    getUsers() {
        this.$http.get("api/Users")
            .then(res => {
                this.user = res.data;
                console.log(res.data);
            });
    }
    getPosts() {
        this.$http.get("api/Posts/")
            .then(res => {
                this.posts = res.data;
                console.log(res.data);
            });
    }
}