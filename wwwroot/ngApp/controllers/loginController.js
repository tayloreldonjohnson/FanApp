class LoginController {
    constructor($accountService, $location) { 
        this.loginUser = null;
        this.validationMessages = null;
        this.accountService = $accountService;
        this.location = $location;
    }

    login() {
        this.accountService.login(this.loginUser)
            .then(() => {
                this.location.path('/');
            }).catch((results) => {
                this.validationMessages = results;
            });
    }
}