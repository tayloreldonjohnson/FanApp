class CreateProfileController {
    constructor($http, $UserProfileService, $location) {
        this.location = $location;
        this.http = $http;
        this.route = "api/users/";  
        this.email = sessionStorage.getItem("email");
		this.$UserProfileService = $UserProfileService;
		this.getUserProfile(); 
		this.user = {
			aboutMe: "",
			imageUrl: "",
            email: ""
        };
    }
	getUserProfile() {
		this.$UserProfileService.getUserProfile(this.email)
			.then((res) => {
				this.user = res.data;
                console.log(res.data);
			});
	}

   PostUserProfile(aboutMe, imageUrl) {
		this.user.email = this.email;
		this.user.aboutMe = aboutMe;
		this.user.imageUrl = imageUrl;

		this.http.post(this.route, this.user)
            .then((res) => {
				this.user = {};
				this.getUserProfile();
                //this.location.path('/userProfile');
            });
    }
}