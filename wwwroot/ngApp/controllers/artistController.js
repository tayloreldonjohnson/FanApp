class artistController {
    constructor($http) {
        this.$http = $http;
        this.getArtists();
        this.artists = [];
    }


    getArtists() {

        this.$http.get("api/Artists")
            .then(res => {
                this.artists = res.data;
                console.log(res.data);
            });
    }
}