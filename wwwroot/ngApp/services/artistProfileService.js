class ArtistProfileService {
	constructor($http, $state) {
		this.http = $http;
		this.route = "api/artists/";
	}

	getArtist(name) {
		return this.http.get(this.route + name);
	}
}