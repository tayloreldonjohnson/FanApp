class UserProfileController {
    constructor($UserProfileService, $http) {
		this.$UserProfileService = $UserProfileService;
		this.$http = $http;
		this.email = sessionStorage.getItem("email");
		this.posts = sessionStorage.getItem("userid");
		this.getPost();
        this.getUserProfile(); 
        //this.getFollowing();
        this.deletePost();
		this.user;
    }
    deletePost() {
        this.$http.delete("api/Posts/5" + this.posts)
            .then(res => {
                this.posts = res.data;
                console.log(res.data);
            });
        }     
    
    //getFollowing() {
    //    this.$http.get(this.id)
    //}
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

   
