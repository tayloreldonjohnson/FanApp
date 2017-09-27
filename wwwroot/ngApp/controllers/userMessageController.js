
class UserMessageController {

    constructor($stateParams, $http, $state,) {

        this.$state = $state;
        this.$http = $http;
        //this.name = $stateParams["name"];
        this.id = $stateParams["id"];
        sessionStorage.setItem("otherid", this.id);
        this.userid = sessionStorage.getItem("userid");
        this.otherid = sessionStorage.getItem("otherid");
        this.messages;
		this.usermessage;
        this.getMessage();
        this.getSentMessage();
        this.inbox = {
            Message: "",
            DateCreated: new Date(),
            MessagerUserId: this.userid,
            RecieverOfMessageId: this.otherid


        };
        this.otheruserinfo;
        this.getOtherUserProfile();

    }

    getMessage() {

        this.$http.get("api/Inboxes/message/" + this.id + "/" + this.userid)
            .then(res => {
            this.messages = res.data;
            console.log(res.data);
        });
	}

	getSentMessage() {
		this.$http.get("api/Inboxes/message/" + this.userid + "/" + this.id)
			.then(res => {
				this.usermessage = res.data;
				console.log(res.data);
			});
	}
    InboxUser(message) {
        console.log("addMessage");
        this.inbox.Message = message;
        console.log(this.post);
        this.$http.post("api/Inboxes", this.inbox)
            .then((res) => {
                console.log("after put");
                this.$state.reload();
            });


    }
    getOtherUserProfile() {
        this.$http.get("api/Users/email/" + this.id)
            .then(res => {
                this.otheruserinfo = res.data;
                console.log(res.data);
            });
    }
}