class CreateProfileController {
    constructor($http) {
        this.http = $http;
        this.route = "api/users/";  
        this.email = sessionStorage.getItem("email");
        this.user = {
			aboutMe: "",
			imageUrl: "",
            email: ""
        };
    }


    //getUsers() {
    //    this.$UserProfileService.getUsers();
    //}

   PostUserProfile(aboutMe, imageUrl) {
       console.log("postMethod");
       this.user.email = this.email;
	   this.user.aboutMe = aboutMe;
	   this.user.imageUrl = imageUrl;

       this.http.post(this.route, this.user)
            .then((res) => {
                this.user = {};
           
            });
    }
}