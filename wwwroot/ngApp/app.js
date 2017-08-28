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
myApp.controller("OtherProfilesController", OtherProfilesController);





myApp.service("$accountService", AccountService);
myApp.service("$UserProfileService", UserProfileService);
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
        .state('otherUsersProfiles', {
            url: '/OtherUsersProfiles',
            templateUrl: '/ngApp/views/OtherUsersProfiles.html',
            controller: OtherProfilesController,
            controllerAs: 'controller'
        })
        .state('otherUserProfile', {
            url: '/OtherUserProfile/:email',
            templateUrl: '/ngApp/views/OtherUserProfile.html',
            controller: OtherUserProfileController,
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
