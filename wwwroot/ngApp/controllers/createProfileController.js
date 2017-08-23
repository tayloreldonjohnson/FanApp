class CreateProfileController {
    constructor($http) {
        this.http = $http;
        this.userProfile = {};
        this.addProfile();
    }
    addProfile() {
        this.http.post("/createProfile", this.userProfile)
            .then(res => {
                this.userProfile = {};
                this.state.go('UserProfile');
            });
    }
}