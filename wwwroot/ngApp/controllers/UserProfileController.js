class UserProfileController {
    constructor($UserProfileService, $filepicker) {
        this.$UserProfileService = $UserProfileService;
        this.email = sessionStorage.getItem("email");
        this.file;
        this.filepicker = $filepicker;
      
        this.user; 
        this.getUserProfile();  
        //console.log("words");

       this.filepicker.setKey('AdqhVmjnDSXuLRRPEfvdbz'); 
    }


    //getUsers() {
    //    this.$UserProfileService.getUsers();
    //}

    getUserProfile() {

        this.$UserProfileService.getUserProfile(this.email)
            .then((res) => {               
				this.user = res.data;
				console.log(res.data);
            });       
    }    

    pickFile() {
        this.filepicker.pick(
            {
                mimetype: 'image/*',
                imageQuality: 60
            },
            this.fileUploaded.bind(this)
        );
    }

    fileUploaded(file) {
        // save file url to database
        this.file = file;
        console.log(this.file);
        console.log(this);
        this.postToSave["item"] = this.file.url;  //change 'item' to imageUrl property
        console.log(this.postToSave);
        this.$scope.$apply(); // force page to update
    }
}

   
