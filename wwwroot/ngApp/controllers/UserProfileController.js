class UserProfileController {
    constructor($UserProfileService) {
        this.$UserProfileService = $UserProfileService;
        this.email = sessionStorage.getItem("email");
      
        this.user; 
        this.getUserProfile();  
        //console.log("words");
    }


    //getUsers() {
    //    this.$UserProfileService.getUsers();
    //}

    getUserProfile() {

        this.$UserProfileService.getUserProfile(this.email)
            .then((res) => {               
				this.user = res.data;
				console.log(res.data);
            });       
    }
    }
   
