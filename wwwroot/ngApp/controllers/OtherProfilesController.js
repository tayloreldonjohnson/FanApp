class OtherProfilesController {
    constructor($http, $userService, $state) {
        this.http = $http;
        this.users = [];
        this.users = {};
        this.getUsers();
        this.state = $state;
        this.userService = $userService;
        this.email = sessionStorage.getItem("email");
        this.user;
    }

    getUsers() {
        this.http.get("api/Users")
            .then(res => {
                this.users = res.data;
                this.state.go("OtherUserProfile");
            });
    }
    getUser() {
        this.userService.getUserProfile(this.email)
            .then((res) => {
                this.user = res.data;
                console.log(res.data);
            });
    }

}