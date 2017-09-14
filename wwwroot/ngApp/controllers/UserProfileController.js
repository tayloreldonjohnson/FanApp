class UserProfileController {
	constructor($UserProfileService, $http, $state) {
		this.$state = $state;
		this.$UserProfileService = $UserProfileService;
		this.$http = $http;
		this.email = sessionStorage.getItem("email");
        this.posts = sessionStorage.getItem("userid");
		this.getPost();
		this.getUserProfile(); 
        this.user;
        this.getFollowInfo();
		this.getNumberOfPosts();  
		this.postinfo;
    }

    getFollowInfo() {
        this.$http.get("api/UserFollowers/" + this.posts)
            .then(res => {
                this.postinfo = res.data;
                
                console.log(this.posts.numberOfFollowing);
            });
    }
    
    getNumberOfPosts() {


        this.$http.get("api/posts/numberOfPosts/" + this.posts)
            .then(res => {
                this.post = res.data;
                console.log("amount of Posts " + this.post.numberOfPosts);
            });


    }

    getUserProfile() {

        this.$UserProfileService.getUserProfile(this.email)
            .then((res) => {               
                this.user = res.data;
             
				console.log(res.data);
            });       
    } 


	getPost() {
		this.$http.get("api/Posts/" + this.posts)
			.then(res => {
				this.posts = res.data;
				console.log(res.data);
			});
	}

	deletePost(postid) {
		this.$http.delete("api/Posts/" + postid)
			.then((res) => {
				this.postid = res.data;
				this.$state.reload();
			});
	}
}

   
