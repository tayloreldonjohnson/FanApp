﻿class ArtistProfileController {
	constructor($ArtistProfileService, $stateParams, $http, $filepicker, $state) {
		this.$ArtistProfileService = $ArtistProfileService;
		this.$state = $state;
		this.$http = $http;
		//this.name = $stateParams["name"];
		this.id = $stateParams["id"];
		sessionStorage.setItem("id", this.id);
		this.user = sessionStorage.getItem("userid");
		//this.route = "api/Posts";
		this.posts = sessionStorage.getItem("id");
		this.post = {
			ApplicationArtistId: this.posts,
			ApplicationUserId: this.user,
			DateCreated: new Date(),
			Media: ""
		};
		this.getArtist();
		this.getPostId();
		this.file;
		this.filepicker = $filepicker;
        this.filepicker.setKey('AolWdzFvkT5aFnci5DlWbz');
		this.artists;
		//this.getlastfm();

	}
	getArtist() {
		this.$ArtistProfileService.getArtist(this.id)
			.then((res) => {
				this.artist = res.data;
				console.log(res.data);
			});
	}

	getPostId() {
		this.$http.get("api/Posts/" + this.posts)
			.then((res) => {
				this.posts = res.data;
				console.log("postdata" + res.data);
			});
	}
	addPost(media) {
		console.log("addPost");


		//this.post.ApplicationArtistid = this.id;
		//this.post.ApplicationUserId = artistid;


		this.post.Media = media;

		console.log(this.post);
		this.$http.post("api/Posts", this.post)
			.then((res) => {

				this.getPostId();
				this.$state.reload();
				console.log("after put");
				//this.addPost();
				//this.location.path('/userProfile');

			});
	}
	//im

	pickFile() {
        this.filepicker.pick(
            
            {
                cropRatio: 5/6,
				mimetype: 'image/*',
                imageQuality: 60,
                services: ['CONVERT', 'COMPUTER'],
                conversions: ['crop', 'rotate',]
			},
            this.fileUploaded.bind(this)

		);
	}

	fileUploaded(file) {
		// save file url to database
		this.file = file;
		console.log(this.file.url);
		console.log(this);
		/*this.$scope.$apply(); */// force page to update
		/*this.file.url; */ //change 'item' to imageUrl property
	}

	//getlastfm() {
	//	this.$http.get("http://ws.audioscrobbler.com/2.0/?method=chart.gettopartists&api_key=87bdb2c24f5d7ea2e34ac5d1bdc419f1&format=json&limit=1000")
	//		.then((res) => {
	//			this.artists = res.data;
	//			console.log(this.artists);
	//			this.$http.post("api/artists", this.artists);
	//		});
	//}
}