class ArtistProfileController {
	constructor($ArtistProfileService, $stateParams, $http) {
		this.$ArtistProfileService = $ArtistProfileService;
		this.$http = $http;
		//this.name = $stateParams["name"];
		this.id = $stateParams["id"];
		sessionStorage.setItem("id", this.id);
		this.posts = sessionStorage.getItem("id");
		this.getArtist();
		this.getPost();
	}
	getArtist() {
		this.$ArtistProfileService.getArtist(this.id)
			.then((res) => {
				this.artist = res.data;
				console.log(res.data);
			});
	}

	getPost() {
		this.$http.get("api/Posts", this.posts)
			.then((res) => {
				this.posts = res.data;
				console.log("postdata" + res.data);
			});
	}
}