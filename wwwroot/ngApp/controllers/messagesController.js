class MessagesController {
    constructor($http, $stateParams){
        this.$http = $http;


        this.$stateParams = $stateParams["id"];
        this.userid = sessionStorage.getItem("userid");
        this.getMessages();

        this.messages;


    }

    getMessages() {
        this.$http.get("api/Inboxes/messageandprofile/" + this.userid  )
            .then(res => {
                this.messages = res.data;
                console.log(res.data);
            });
    }

   




}