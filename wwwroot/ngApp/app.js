var myApp = angular.module("myApp", ['ui.router', 'ngResource', 'ui.bootstrap']);

myApp.controller("AboutController", AboutController);
myApp.controller("AccountController", AccountController);
myApp.controller("ConfirmEmailController", ConfirmEmailController);
myApp.controller("ExternalRegisterController", ExternalRegisterController);
myApp.controller("HomeController", HomeController);
myApp.controller("LoginController", LoginController);
myApp.controller("RegisterController", RegisterController);
myApp.controller("SecretController", SecretController);
myApp.controller("CreateProfileController",CreateProfileController);
myApp.controller("UserProfileController", UserProfileController);
myApp.controller("SearchUserController", SearchUserController);
myApp.controller("ArtistController", ArtistController);
myApp.controller("MessagesController", MessagesController);
myApp.controller("HomeFeedController", HomeFeedController);

myApp.service("$accountService", AccountService);
myApp.service("$UserProfileService", UserProfileService);
myApp.service("$ArtistProfileService", ArtistProfileService);
myApp.service("$filepicker", function ($window) {
    return $window.filepicker;
});

myApp.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

    $stateProvider
        .state('home', {
            url: '/',
            templateUrl: '/ngApp/views/home.html',
            controller: HomeController,
            controllerAs: 'controller'
        })
        .state('homeFeed', {
            url: '/homeFeed',
            templateUrl: '/ngApp/views/homeFeed.html',
            controller: HomeFeedController,
            controllerAs: 'controller'
        })
        .state('secret', {
            url: '/secret',
            templateUrl: '/ngApp/views/secret.html',
            controller: SecretController,
            controllerAs: 'controller'
        })
        .state('login', {
            url: '/login',
            templateUrl: '/ngApp/views/login.html',
            controller: LoginController,
            controllerAs: 'controller'
        })
        .state('register', {
            url: '/register',
            templateUrl: '/ngApp/views/register.html',
            controller: RegisterController,
            controllerAs: 'controller'
        })
        .state('externalRegister', {
            url: '/externalRegister',
            templateUrl: '/ngApp/views/externalRegister.html',
            controller: ExternalRegisterController,
            controllerAs: 'controller'
        })
        .state('about', {
            url: '/about',
            templateUrl: '/ngApp/views/about.html',
            controller: AboutController,
            controllerAs: 'controller'
        })
        .state('createProfile', {
            url: '/createProfile',
            templateUrl: '/ngApp/views/createProfile.html',
            controller: CreateProfileController,
            controllerAs: 'controller'
        })
        .state('userProfile', {
            url: '/userProfile',
            templateUrl: '/ngApp/views/UserProfile.html',
            controller: UserProfileController,
            controllerAs: 'controller'
        })
        .state('searchUser', {
            url: '/searchUser',
			templateUrl: '/ngApp/views/searchUser.html',
			controller: SearchUserController,
            controllerAs: 'controller'
        })
        .state('otherUserProfile', {
            url: '/OtherUserProfile/',
            params: {id: null},
            templateUrl: '/ngApp/views/OtherUserProfile.html',
            controller: OtherUserProfileController,
            controllerAs: 'controller'
        })
        .state('artist', {
            url: '/artist',
            templateUrl: '/ngApp/views/artist.html',
            controller: ArtistController,
            controllerAs: 'controller'
		})
		.state('artistProfile', {
			url: '/artistProfile/:id',
			templateUrl: '/ngApp/views/artistProfile.html',
			controller: ArtistProfileController,
			controllerAs: 'controller'
        })
        .state('messages', {
            url: '/messages',
            templateUrl: '/ngApp/views/messages.html',
            controller: MessagesController,
            controllerAs: 'controller'
        })
           .state('notFound', {
            url: '/notFound',
            templateUrl: '/ngApp/views/notFound.html'
        });

    // Handle request for non-existent route
    $urlRouterProvider.otherwise('/notFound');

    // Enable HTML5 navigation
    $locationProvider.html5Mode(true);
});

angular.module('myApp')
       .factory('authInterceptor', ($q, $window, $location) =>
        ({
            request: function (config) {
                config.headers = config.headers || {};
                config.headers['X-Requested-With'] = 'XMLHttpRequest';
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401 || rejection.status === 403) {
                    $location.path('/login');
                }
                return $q.reject(rejection);
            }
        }));

angular.module('myApp').config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptor');
});
/*----search bar filters----*/
myApp.filter('myfilter', function () {
    function strStartsWith(str, prefix) {
        //str = str.toLowerCase();
        //prefix = prefix.toLowerCase();
        var result = (str + "").indexOf(prefix) === 0;
        return result;
    }
    return function (items, userName) {
        var filtered = [];
        angular.forEach(items, function (item) {
            if (strStartsWith(item.userName, userName)) {
                filtered.push(item);
            }
        });
        return filtered;
    };
});
myApp.filter('myArtistfilter', function () {
    function strStartsWith(str, prefix) {
        //str = str.toLowerCase();
        //prefix = prefix.toLowerCase();
        var result = (str + "").indexOf(prefix) === 0;
        return result;
    }
    return function (items, name) {
        var filtered = [];
        angular.forEach(items, function (item) {
            if (strStartsWith(item.name, name)) {
                filtered.push(item);
            }
        });
        return filtered;
    };
});






/*----------------------------------------------  FILESTACK  ---------------------------------------------------------------*/
//angular.module('myApp')
//    .service('angularFilepicker', function ($window) {
//        return $window.filepicker;
//    });


//angular.module('angularFilepickerExample')
//    .controller('UserProfileController', function ($scope, angularFilepicker) {
//        $scope.files = [];
       

//        $scope.pickFile = pickFile;

        


//        function onSuccess(Blob) {
//            $scope.files.push(Blob);
//            $scope.$apply();
//        };

//    });
