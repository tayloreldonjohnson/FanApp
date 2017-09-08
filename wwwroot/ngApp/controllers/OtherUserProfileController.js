class OtherUserProfileController {
    constructor($UserProfileService , $stateParams, $http) {
		//this.$UserProfileService = $UserProfileService;
		this.$http = $http;
        this.email = $stateParams["email"];
        this.followeduser = {};
        this.id = $stateParams["id"];
        sessionStorage.setItem("otherid", this.id);
      
        this.userid = sessionStorage.getItem("userid");
        this.otherid = sessionStorage.getItem("otherid");
        this.UserFollower = {
          FollowedUserId: this.otherId,
            FollowingUserId: this.userid,
           
        };
     
		//this.getUserProfile(); 
		this.getOtherUserProfile();
    }



    addFollower() {

        //this.UserFollower.FollowingUserId = you;
        //this.UserFollower.FollowedUserId = 
        this.$http.post("api/UserFollowers", this.UserFollower)
            .then(res => {
                this.followeduser = {};
                console.log(res.data);
            });
    }
 //   getUserProfile() {
 //       this.$UserProfileService.getUserProfile(this.email)
 //           .then((res) => {
 //               this.user = res.data;
 //               console.log(res.data);
 //           });
	//}

	getOtherUserProfile() {
		this.$http.get("api/Users/email/" + this.id)
			.then((res) => {
				this.user = res.data;
				console.log(res.data);
			});
	}
}