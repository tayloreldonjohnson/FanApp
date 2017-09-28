class CreateProfileController {
	constructor($http, $UserProfileService, $location, $filepicker , $state, $timeout) {
        this.location = $location;
        this.$timeout = $timeout;
        this.state = $state;
        this.http = $http;
        this.route = "api/users/";  
        this.email = sessionStorage.getItem("email");
		this.$UserProfileService = $UserProfileService;
		this.getUserProfile(); 
		this.user = {
			aboutMe: "",
			imageUrl: "",
            email: ""
		};
		//imgstuff
		this.file;
		this.filepicker = $filepicker;
        this.filepicker.setKey('Aowd5dVQ06CyRYPl9EaAVz'); 

    }
	getUserProfile() {
		this.$UserProfileService.getUserProfile(this.email)
			.then((res) => {
				this.user = res.data;
                console.log(res.data);
                
			});
	}

   PostUserProfile(aboutMe, imageUrl) {
		this.user.email = this.email;
		this.user.aboutMe = aboutMe;
		this.user.imageUrl = imageUrl;

		this.http.post(this.route, this.user)
            .then((res) => {
				this.user = {};
				this.getUserProfile();

               
            });

        this.$timeout(this.state.go.bind(null, "userProfile"), 100);
	}
	//img stuff
	pickFile() {
		this.filepicker.pick(
		{
                cropRatio: 1/1,               
                mimetype: 'image/*',
                imageQuality: 80, 
                conversions: ['crop', 'rotate']                           
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
}