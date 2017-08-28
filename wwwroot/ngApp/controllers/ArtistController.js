class UserProfileController {
    constructor($ArtistService) {
        this.$ArtistService = $ArtistService;
        this.name = sessionStorage.getItem("name");

        this.getArtist();
        this.user;

        //getArtists() {
        //    this.$ArtistService.getArtists();
        //}

        getUserProfile() {

            this.$ArtistService.getArtist(this.name)
                .then((res) => {
                    this.artist = res.data;

                    console.log(res.data);
                });
        }
    }
}


