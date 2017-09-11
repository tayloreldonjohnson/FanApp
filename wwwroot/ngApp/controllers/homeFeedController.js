class HomeFeedController{
    constructor($http) {
        this.$http = $http;
        this.Id = sessionStorage.getItem("userid");
        this.getPosts();
    }
    getPosts() {
        this.$http.get("api/Posts/", this.Id)
            .then(res => {
                this.Id = res.data;
                console.log(res.data);
            });
    }
}