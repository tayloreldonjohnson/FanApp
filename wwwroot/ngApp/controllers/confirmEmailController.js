class ConfirmEmailController {
    constructor($http, $accountService, $stateParams, $location) {
        let userId = $stateParams['userId'];
        let code = $stateParams['code'];
        this.validationMessages = null;

        $accountService.confirmEmail(userId, code)
            .then((result) => {
                $location.path('/');
            }).catch((result) => {
                this.validationMessages = result;
            });
    }
}