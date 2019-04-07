

﻿//////////////////////////////////////////
// Client AngularJS Service
//////////////////////////////////////////
(function () {
    'use strict';

    angular.module('steamSearch')
        .service('steamService', ['$rootScope', '$http', 'WebApiUrl', function ($rootScope, $http, WebApiUrl) {

            this.userData = {};
            this.recentGames = [];
            this.ownedGames = [];
            this.dataLoaded = false;
            this.allGames = [];
            this.friends = [];
            this.steamId = "";



            this.GetRecentPlayedGames = function (steamId) {


                var service = this;
                $http({
                    method: 'GET',
                    url: WebApiUrl + "/api/steam/getrecentgames/" + steamId
                }).then(function successCallback(response) {


                    service.recentGames = JSON.parse(response.data).response.games;
                    console.log(service.recentGames);
                    service.GetOwnedGames(steamId);

                    // this callback will be called asynchronously
                    // when the response is available
                }, function errorCallback(response) {
                    service.dataLoaded = true;
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
            }
            this.GetSteamUserInfo = function (steamId) {

                var service = this;

                $http({
                    method: 'GET',
                    url: WebApiUrl + "/api/steam/getuser/" + steamId
                }).then(function successCallback(response) {


                    service.userData = response.data;
                    console.log(service.userData);
                    service.steamId = service.userData.steamid;
                    service.GetRecentPlayedGames(service.steamId);
                    // this callback will be called asynchronously
                    // when the response is available
                }, function errorCallback(response) {


                    alert(response.data.ExceptionMessage);
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });


            }
            this.GetOwnedGames = function (steamId) {

                var service = this;
                $http({
                    method: 'GET',
                    url: WebApiUrl + "/api/steam/getownedgames/" + service.steamId
                }).then(function successCallback(response) {


                    service.ownedGames = JSON.parse(response.data).response.games;
                    console.log(service.ownedGames);
                    service.dataLoaded = true;
                    // this callback will be called asynchronously
                    // when the response is available
                }, function errorCallback(response) {
                    service.dataLoaded = true;
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });

            }
            this.GetAllGames = function () {

                var service = this;
                $http({
                    method: 'GET',
                    url: WebApiUrl + "/api/steam/getallgames"
                }).then(function successCallback(response) {


                    service.allGames = JSON.parse(response.data).applist.apps;
                    console.log(service.allGames);
                    // this callback will be called asynchronously
                    // when the response is available
                }, function errorCallback(response) {
                    service.dataLoaded = true;
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });


            }
            this.GetAllFriends = function (steamId) {

                var service = this;
                $http({
                    method: 'GET',
                    url: WebApiUrl + "/api/steam/getFriends/" + service.steamId
                }).then(function successCallback(response) {


                    service.friends = JSON.parse(response.data).friendslist.friends;
                    console.log(service.service.friends);
                    // this callback will be called asynchronously
                    // when the response is available
                }, function errorCallback(response) {
                    service.dataLoaded = true;
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });

            }





        }]);
})();
