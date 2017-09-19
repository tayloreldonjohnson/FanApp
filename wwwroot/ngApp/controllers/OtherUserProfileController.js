class OtherUserProfileController {
    constructor($stateParams, $http, $state, $templateCache) {
        this.$templateCache = $templateCache;
        this.state = $state;
        this.$http = $http;
        this.email = $stateParams["email"];
        this.followeduser;
        this.id = $stateParams["id"];
        sessionStorage.setItem("otherid", this.id);
        this.userid = sessionStorage.getItem("userid");
        this.otherid = sessionStorage.getItem("otherid");
        this.getNumberOfPosts();
        this.userfollower;
        this.userfollowerinfo;
        this.getFollowInfo();
        this.isFollowing();
        this.isfollowing;
        this.inbox = {

            DateCreated: new Date(),
            MessagerUserId: this.userid,
            RecieverOfMessageId: this.otherid

        };


        this.getOtherUserProfile();
        this.userfollower = {
            FollowingUserId: this.userid,
            FollowedUserId: this.otherid
        };
      
    }
    addFollower() {
        this.$http.post("api/UserFollowers", this.userfollower)
            .then(res => {
                this.followeduser = res.data;
				console.log(res.data);
				this.state.reload();
            });
        //this.$templateCache.removeAll();
        //this.state.reload();
    }
        getFollowInfo() {
			this.$http.get("api/UserFollowers/" + this.otherid)
                .then(res => {
					this.posts = res.data;
					console.log(this.posts);
                    console.log(this.posts.numberOfFollowing);
                });
		}
        deleteFollower() {
            this.$http.delete("api/UserFollowers/unfollow/" + this.otherid + "/" + this.userid)
                .then(res => {
                    console.log(res.data);
					this.state.reload();
                });
        }


        getNumberOfPosts() {
            this.$http.get("api/posts/numberOfPosts/" + this.otherid)
                .then(res => {
                    this.post = res.data;
                    console.log("amount of Posts " + this.post.numberOfPosts);
                });
		}

        getOtherUserProfile() {
            this.$http.get("api/Users/email/" + this.id)
                .then((res) => {
                    this.user = res.data;
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
                });
        }
           isFollowing() {
            
               this.$http.get("api/UserFollowers/isFollowing/" + this.otherid + "/" + this.userid)
                   .then((res) => {
                       this.isfollowing = res.data;
                       console.log(res.data);
                       console.log("after put");
                   });
           }
     }
        
    