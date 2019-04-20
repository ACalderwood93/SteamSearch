

﻿//////////////////////////////////////////
// Client AngularJS Service
//////////////////////////////////////////
(function () {
    'use strict';

    angular.module('steamSearch')
        .service('steamService', ['$rootScope', '$http', 'WebApiUrl', '$timeout', function ($rootScope, $http, WebApiUrl,$timeout) {

            this.userData = {};
            this.recentGames = [];
            this.ownedGames = [];
            this.dataLoaded = null;
            this.allGames = [];
            this.friends = [];
            this.steamId = "";
            this.appsInCommon = [];
            this.featuredGames = [];
            this.catagories = [];



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

                if (steamId != undefined && steamId.length > 0) {
                    service.friends = [];
                    this.dataLoaded = false;
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

                        M.toast({ html: response.data.ExceptionMessage });

                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                    });
                } else {

                    M.toast({ html: 'Please enter a steamId or username before search' });
                }


            }
            this.GetOwnedGames = function (steamId) {

                var service = this;
                $http({
                    method: 'GET',
                    url: WebApiUrl + "/api/steam/getownedgames/" + service.steamId
                }).then(function successCallback(response) {


                    service.ownedGames = response.data;
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


                    service.friends = response.data;
                    service.GetAllCatagories();
                    

                    // this callback will be called asynchronously
                    // when the response is available
                }, function errorCallback(response) {
                    service.dataLoaded = true;
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });

            }
            this.GetGamesInCommon = function () {

                var selectedFriends = this.friends.filter(function (item) {
                    return item.selected;
                });

                selectedFriends.push(this.userData);// add yourself to the list;
                M.toast({ html: "Searching..." });
                var service = this;
                $http({
                    method: 'POST',
                    url: WebApiUrl + "/api/steam/getGamesInCommon",
                    data: selectedFriends


                }).then(function successCallback(response) {
                    service.appsInCommon = response.data;
                    M.toast({ html: `${service.appsInCommon.length} Game(s) Found in Common!!!` });

                }, function errorCallback(response) {

                });

            }
            this.GetTopGames = function () {
                var service = this;
                $http({
                    method: 'GET',
                    url: WebApiUrl + "/api/steam/gettopgames"
                }).then(function successCallback(response) {
                    var games = JSON.parse(response.data)["featured_win"];
                    service.featuredGames = games;

                    console.log(service.featuredGames);

                }, function errorCallback(response) {

                });

            }
            this.GetAllCatagories = function () {

                var service = this;
                $http({
                    method: 'GET',
                    url: WebApiUrl + "/api/steam/getallcatagories"

                }).then((response) => {

                    service.catagories = response.data;

                    $timeout(function () {
                        $('select').formSelect();
                    });
                    

                }, (errorResponse) => {

                });

            }


        }]);
})();
