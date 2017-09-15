class UserProfileService {
    constructor($http, $state) {
        this.http = $http;
        this.route = "api/users/";  
        //this.routeToFollow = "api/userFollowers/";
    }

    getUsers() { 
        return this.http.get(this.route);    
    }

    getUserProfile(email) {
        return this.http.get(this.route + email);
    }

    //getUserFollowers(posts) {
    //    return this.http.get(this.routeToFollow + "followers/" + posts)
    //}
    //getFollowing(posts) {
    //    return this.http.get(this.routeToFollow + "count/" + posts)
    //}
}
