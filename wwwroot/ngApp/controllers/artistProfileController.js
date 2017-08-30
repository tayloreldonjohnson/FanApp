class ArtistProfileController {
	constructor($ArtistProfileService, $stateParams, $http) {
		this.$ArtistProfileService = $ArtistProfileService;
		this.name = $stateParams["name"];
        this.getArtist();
        this.clientId = 'client_id=444f00b2a47f4ceeb7f8367170a45960';
        this.secretKey = 'client_secret=69ca8bd0bd574c7f9e2c321c16c40cdb';
        this.apiAuthorize = 'https://accounts.spotify.com/authorize';
        this.$http = $http;

        this.getSpotifyArtist();
	}
	getArtist() {
		this.$ArtistProfileService.getArtist(this.name)
			.then((res) => {
				this.artist = res.data;
				console.log(res.data);
			});
    }

    getSpotifyArtist() {
        this.$http.get(this.apiAuthorize + "&" + this.clientId)
            .then((res) => {
                this.accessToken = res.data;
                console.log(this.accessToken);
            })
    }




}