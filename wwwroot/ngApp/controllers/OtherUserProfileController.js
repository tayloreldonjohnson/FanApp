class OtherUserProfileController {
    constructor($UserProfileService , $stateParams, $http) {
		this.$UserProfileService = $UserProfileService;
		this.$http = $http;
		this.email = $stateParams["email"];
		this.getUserProfile(); 
		this.getPost();
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