class RegisterController {
    constructor($accountService, $location) { 
        this.registerUser = null;
        this.validationMessages = null;
        this.accountService = $accountService;
        this.location = $location;
    }

    register() {
        this.accountService.register(this.registerUser)
            .then(() => {
                this.location.path('/');
            }).catch((results) => {
                this.validationMessages = results;
            });
    }
}