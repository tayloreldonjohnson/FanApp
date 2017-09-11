class OtherUserProfileController {
    constructor($UserProfileService, $stateParams, $http) {
		this.$UserProfileService = $UserProfileService;
		this.$http = $http;
        this.email = $stateParams["email"];
        //this.userFollowId = $stateParams["userfollowid"];
        //sessionStorage.setItem("userfollowid", this.userFollowId);
        //this.postUserFollower();   
		this.getUserProfile(); 
    }
    //postUserFollower() {
    //    this.$http.post("api/UserFollowers", this.userFollowId)
    //        .then(res => {
    //            this.userFollowId = {};
    //            console.log(res.data);
    //        });
    //}
    getUserProfile() {
        this.$UserProfileService.getUserProfile(this.email)
            .then((res) => {
                this.user = res.data;
                console.log(res.data);
            });
	}
}