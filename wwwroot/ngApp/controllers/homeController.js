class HomeController {
    constructor($accountService, $location) {
        this.message = 'Hello from the home page!';

        
        this.accountService = $accountService;
        this.location = $location;

    }

    var client = filestack.init("AdqhVmjnDSXuLRRPEfvdbz");

    pickPhoto() {
        console.log("picking photo");
    }
}