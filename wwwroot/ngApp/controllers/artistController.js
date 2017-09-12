class ArtistController {
	constructor($http, $stateParams, $ArtistProfileService) {
        this.$http = $http;
        this.getArtists();
		this.artists = [];
        this.$stateParams = $stateParams["id"];
        this.$stateParams = $stateParams["postid"];
        this.$stateParams = $stateParams["userId"];
		this.$ArtistProfileService = $ArtistProfileService;

    }

    getArtists() {
        this.$http.get("api/Artists")
            .then(res => {
				this.artists = res.data;
				console.log(res.data);
            });
	}
	getArtist() {
		this.$ArtistProfileService.getArtist(this.id)
			.then((res) => {
				this.artist = res.data;
				console.log(res.data);
				this.location.path("/artistProfile");
			});
    }


    //service?
    //getAOArtist() {
    //    return this.$http.get('http://api.openaura.com/v1/search/artists_all')
    //        .map((res) => res.json());
    //}

}