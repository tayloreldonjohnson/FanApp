class AccountController {
    constructor($accountService, $location) {
        this.accountService = $accountService;
        this.location = $location;
        this.externalLogins = null;
        this.getExternalLogins().then((results) => {
            this.externalLogins = results;
        });
    }

    getUserName() {
        return this.accountService.getUserName();
    }

    getClaim(type) {
        return this.accountService.getClaim(type);
    }

    isLoggedIn() {
        return this.accountService.isLoggedIn();
    }

    logout() {
        this.accountService.logout();
        this.location.path('/');
    }

    getExternalLogins() {
        return this.accountService.getExternalLogins();
    }
}