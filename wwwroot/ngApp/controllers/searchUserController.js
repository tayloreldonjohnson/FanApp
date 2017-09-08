class SearchUserController {
    constructor($http, $location, $UserProfileService , $stateParams ) {
        this.http = $http;
        this.users = [];
        this.users = {};
        this.getUsers();
        this.$stateParams = $stateParams["id"];
        this.$UserProfileService = $UserProfileService;
        this.email = sessionStorage.getItem("id");
    }
    getUsers() {
        this.http.get("api/Users")
            .then(res => {
                this.users = res.data;
                
            });
    }
    //getUserProfile() {
    //     this.$UserProfileService.getUserProfile(this.email)
    //        .then((res) => {
    //            this.user = res.data;
    //            this.location.path("/OtherUserProfile");
    //        });  
    //}
  
  }