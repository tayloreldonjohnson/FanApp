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
        //this.addFollower();
		//this.getUserProfile(); 
		this.getOtherUserProfile();
		this.userfollower = {
			FollowingUserId: this.userid,
			FollowedUserId: this.otherid,
		};
    }
	addFollower() {
		//this.userfollower.FollowedUserId = user;
		//this.userfollower.FollowingUserId = otheruser;
        this.$http.post("api/UserFollowers", this.userfollower)
            .then(res => {
				this.userfollower = {};
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