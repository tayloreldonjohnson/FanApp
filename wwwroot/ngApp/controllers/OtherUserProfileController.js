class OtherUserProfileController {
    constructor($UserProfileService , $stateParams, $http) {
		this.$UserProfileService = $UserProfileService;
		this.$http = $http;
        this.email = $stateParams["email"];
        this.followeduser = {};
        this.id = $stateParams["id"];
        sessionStorage.setItem("id", this.id);
        this.addFollower();
		this.getUserProfile(); 
		this.getPost();
    }
    addFollower() {
        this.$http.post("api/UserFollowers", this.followeduser)
            .then(res => {
                this.followeduser = {};
                console.log(res.data);
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
		this.$http.get("api/Posts/")
			.then(res => {
				this.posts = res.data;
				console.log(res.data);
			});
	}
}