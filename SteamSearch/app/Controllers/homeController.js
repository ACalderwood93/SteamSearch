(function () {
    'use strict';

    angular
        .module('steamSearch')
        .controller('homeController', HomeController)

    HomeController.$inject = ['$scope', 'steamService'];

    function HomeController($scope, steamService) {


        $scope.test = "Hello world";

        $scope.steamService = steamService;
        $scope.menuState = "navUser";


        $scope.init = function () {

            //  if ($scope.steamService.allGames.length < 1)
            //   $scope.steamService.GetAllGames();
        }

        $scope.navClicked = function (navId) {

            switch (navId) {

                case "navUser":
                    break;

                case "navRecent":
                    break;

                case "navFriends":
                    // we dont want to reload this if we have aready done it.
                    if ($scope.steamService.friends.length < 1)
                        $scope.steamService.GetAllFriends($scope.steamId);
                    break;
            }

            $scope.menuState = navId;



        }

        console.log($scope.menuState);



    }
})();