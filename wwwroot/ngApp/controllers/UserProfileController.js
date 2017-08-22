class UserProfileController {
    constructor($http, $state, $cookies) {
        this.http = $http;
        this.User = {};
    
        //this.state = $state
        //this.cookies = $cookies;
    }


    getUsers() {
        this.http.get("api/AspNetUsers").then(res => {
            this.User = res.data;

        });


    
        }

    }
    //addCustomers() {
    //    this.http.post("api/Customers", this.customer)
    //        .then(res => {
    //            this.customer = {};
    //            //console.log(res);
    //            this.state.go('orderpage');
    //            this.cookies.put("customerId", res.data.customerId);
    //        });
    //}
