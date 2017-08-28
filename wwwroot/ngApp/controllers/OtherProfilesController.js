class OtherProfilesController {
    constructor($http) {
        this.http = $http;
        this.users = [];
        this.getUsers();
        this.users = {};
    }

    getUsers() {
        this.http.get("api/Users")
            .then(res => {
                this.users = res.data;
            });
    }
}