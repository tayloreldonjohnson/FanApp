class ArtistProfileController {
	constructor($ArtistProfileService, $stateParams) {
		this.$ArtistProfileService = $ArtistProfileService;
		this.name = $stateParams["name"];
		this.getArtist();
	}
	getArtist() {
		this.$ArtistProfileService.getArtist(this.name)
			.then((res) => {
				this.artist = res.data;
				console.log(res.data);
			});
	}
}