class UserProfileController {
    constructor($UserProfileService) {
        this.$UserProfileService = $UserProfileService;
        this.email = sessionStorage.getItem("email");
     
        this.getUserProfile(); 
        this.user; 
        //this.planets = [
        //    {
        //        name: 'Mercury',
        //        distance: 0.4,
        //        mass: 0.055
        //    },
        //    {
        //        name: 'Venus',
        //        distance: 0.7,
        //        mass: 0.815
        //    },
            
        
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

   
