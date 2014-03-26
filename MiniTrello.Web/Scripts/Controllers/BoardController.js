'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')



    // Path: /login
    .controller('BoardController', ['$scope', '$location', '$window', 'BoardServices','$stateParams', function ($scope, $location, $window, boardServices, $stateParams) {


      // $scope.boardDetailId = $stateParams.boardId;
         
        //console.log($location.search().boardId);

        console.log($scope.boardDetailId);
        $scope.crearBoardModel = {Name: '', Description: '' };
        $scope.boards = [];

        $scope.crearBoard = function () {

            boardServices
           .crearBoard($scope.crearBoardModel, $stateParams.organizationId)
           .success(function (data, status, headers, config) {
               console.log(data);
               $location.path('/organizations/'+$stateParams.organizationId+'/boards');
           })
           .error(function (data, status, headers, config) {
               console.log(data);
           });
        };

        $scope.getBoardsForLoggedUser = function () {

            boardServices
                .getBoardsForLoggedUser($stateParams.organizationId)
              .success(function (data, status, headers, config) {
                   $scope.boards = data;
                })
              .error(function (data, status, headers, config) {
                console.log(data);
            });
        
        };
        $scope.getBoardsForLoggedUser();
   /* if ($scope.boardDetailId > 0)
    {
        //get board details
    }
    else
    {
        $scope.getBoardsForLoggedUser();
    }
    
    */
       

        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);