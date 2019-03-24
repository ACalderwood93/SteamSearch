(function () {
    'use strict';

    angular
        .module('steamSearch')
        .controller('homeController', HomeController)

    HomeController.$inject = ['$scope','steamService'];

    function HomeController($scope, steamService) {


        $scope.test = "Hello world";

        $scope.steamService = steamService;
        $scope.menuState = "navRecent";


        
    }
})();