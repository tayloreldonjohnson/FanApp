class createProfileController {
    constructor($http) {
        this.http = $http;
        this.route = "api/users/";  
        this.email = sessionStorage.getItem("email");
        this.user = {
            aboutMe: "",
            email: ""
        };
       
        console.log("words");
    }


    //getUsers() {
    //    this.$UserProfileService.getUsers();
    //}

   PostUserProfile(aboutMe) {
       console.log("postMethod");
       this.user.email = this.email;
       this.user.aboutMe = aboutMe;

       this.http.post(this.route, this.user)
            .then((res) => {
                this.user = {};
           
            });
    }
}