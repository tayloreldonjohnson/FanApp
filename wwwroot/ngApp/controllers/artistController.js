class ArtistController {
	constructor($http, $stateParams, $ArtistProfileService) {
        this.$http = $http;
        this.getArtists();
		this.artists = [];
		this.$stateParams = $stateParams["name"];
		this.$ArtistProfileService = $ArtistProfileService;

    }

    getArtists() {
        this.$http.get("api/Artists")
            .then(res => {
				this.artists = res.data;
            });
	}
	getArtist() {
		this.$ArtistProfileService.getArtist(this.name)
			.then((res) => {
				this.artist = res.data;
				console.log(res.data);
				this.location.path("/artistProfile");
			});
	}
}