class MessagesController {
    constructor($http){
        this.$http = $http;



        this.userid = sessionStorage.getItem("userid");
        this.getMessages();

        this.messages;




    }

    getMessages() {
        this.$http.get("api/Inboxes/messageandprofile/" + this.userid )
            .then(res => {
                this.messages = res.data;
                console.log(res.data);
            });
    }






}