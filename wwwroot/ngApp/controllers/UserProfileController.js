class UserProfileController {
    constructor($UserProfileService, $http) {
		this.$UserProfileService = $UserProfileService;
		this.$http = $http;
		this.email = sessionStorage.getItem("email");
		this.posts = sessionStorage.getItem("userid");
		this.getPost();
		this.getUserProfile(); 
		this.user;
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

	getPost() {
		this.$http.get("api/Posts/" + this.posts)
			.then(res => {
				this.posts = res.data;
				console.log(res.data);
			});
	}
}

   
