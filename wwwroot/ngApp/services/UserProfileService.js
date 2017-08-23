class UserProfileService {
    constructor($http, $state) {
        this.http = $http;
        this.route = "api/users/";       
    }

    getUsers() { 
        return this.http.get(this.route);    
    }

    getUserProfile(email) {
        return this.http.get(this.route + email);
    }
}
