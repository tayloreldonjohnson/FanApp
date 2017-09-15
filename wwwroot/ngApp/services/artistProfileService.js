class ArtistProfileService {
	constructor($http, $state) {
		this.http = $http;
		this.route = "api/artists/";
	}

	getArtist(id) {
		return this.http.get(this.route + id);
    }

    savePost(post) {
        return this.http.post("api/Posts", post);
    }
}