class createProfileController {
    constructor($state , $http) {
        this.$state = $state;
        this.$http = $http;
        this.user = {};
        
    }
    postUsers() {
        this.http.post("api/Users", this.user)
            .then(res => {
                this.user = {};    
            });
    }

}