class OtherUserProfileController {
    constructor($UserProfileService , $stateParams, $http) {
		this.$UserProfileService = $UserProfileService;
		this.$http = $http;
		this.email = $stateParams["email"];
		this.getUserProfile(); 
    }
    getUserProfile() {
        this.$UserProfileService.getUserProfile(this.email)
            .then((res) => {
                this.user = res.data;
                console.log(res.data);
            });
	}
}