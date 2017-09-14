class OtherUserProfileController {
    constructor( $stateParams, $http, $state) {
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
     
  
        this.getOtherUserProfile();
        this.userfollower = {         
            FollowingUserId: this.userid,
            FollowedUserId: this.otherid
		};
    }

        addFollower() {
            //this.userfollower.FollowedUserId = user;
            //this.userfollower.FollowingUserId = otheruser;
            this.$http.post("api/UserFollowers", this.userfollower)
                .then(res => {
                    this.followeduser = res.data;
                    console.log(res.data);
                });
            this.state.reload();
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
     }
        
    