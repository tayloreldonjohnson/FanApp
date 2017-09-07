class OtherUserProfileController {
    constructor($UserProfileService , $stateParams, $http) {
		this.$UserProfileService = $UserProfileService;
		this.$http = $http;
        this.email = $stateParams["email"];
        this.userFollow = $stateParams["userid"] ;
        this.addUserFollower();
		this.getUserProfile(); 
		this.getPost();
    }
    addUserFollower() {
        this.$http.post("api/UserFollowers", this.userFollow, this.email)
            .then(res => {
                this.userFollow = res.data;
                this.email = res.data;
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