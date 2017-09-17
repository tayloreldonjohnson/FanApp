
class UserMessageController {

    constructor($stateParams, $http, $state) {

        this.$state = $state;
        this.$http = $http;
        //this.name = $stateParams["name"];
        this.id = $stateParams["id"];
        this.userid = sessionStorage.getItem("userid");
        this.messages;
        this.getMessage();


    }

    getMessage() {

        this.$http.get("api/Inboxes/message/" + this.userid + "/" + this.id)
            .then(res => {
            this.messages = res.data;
            console.log(res.data);
        });
    }
}