class ArtistProfileController {
	constructor($ArtistProfileService, $stateParams, $http) {
		this.$ArtistProfileService = $ArtistProfileService;
		this.name = $stateParams["name"];
        this.getArtist();
        this.clientId = 'client_id=444f00b2a47f4ceeb7f8367170a45960';
        this.secretKey = 'client_secret=69ca8bd0bd574c7f9e2c321c16c40cdb';
        this.apiAuthorize = 'https://accounts.spotify.com/authorize';
        this.$http = $http;
        this.proxyurl = "https://cors-anywhere.herokuapp.com/";
        //this.getSpotifyArtist();
	}
	getArtist() {
		this.$ArtistProfileService.getArtist(this.name)
			.then((res) => {
				this.artist = res.data;
				console.log(res.data);
			});
    }

    getSpotifyArtist() {
        this.$http.get(this.proxyurl + this.apiAuthorize + "&" + this.clientId)
            .then((res) => {
                this.accessToken = res.data;
                console.log(this.accessToken);
            })
    }
    //access token: code=AQBqGgUBYWJkDhUgBdLR36nXd8OcZK9lpivxLBqD6ujpfaIBQGEclCPYX2Zz4jvlX51ohFOD3E-NvMWwOMZtpC5nBN7L8_0fDzy3q3pSM8I6CvGsgp0q3g6oKrVo8C7iB3ac4uDQLX8SMZyxBA4wUG2unWQER6eJUE8SsZC9p_DgdqeOQHTBnCbPj6LzjoRvu7zNQi8yfZPP5X6n9th6mRp-UMAMkvwOtUCF_DSR7WQ2&state=34fFs29kd09



}