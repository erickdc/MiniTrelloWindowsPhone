'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')



    // Path: /login
    .controller('LaneController', ['$scope', '$location', '$window', 'LaneServices','$stateParams', function ($scope, $location, $window, laneServices, $stateParams) {


      // $scope.boardDetailId = $stateParams.boardId;
         
        //console.log($location.search().boardId);

     //   console.log($scope.boardDetailId);
        $scope.crearLaneModel = {Name: '', Description: '' };
        $scope.lanes = [];
       
        
        //var board1 = { Id: 2, Name: 'Myboard2', Description: 'Description2' };
       // $scope.boards.push(board);
     //   $scope.boards.push(board1);
        $scope.crearLane = function () {

            laneServices
           .crearLane($scope.crearLaneModel, $stateParams.boardId)
           .success(function (data, status, headers, config) {
               console.log(data);
               $location.path('/boards/'+$stateParams.boardId+'/lanes');
           })
           .error(function (data, status, headers, config) {
               console.log(data);
           });
        };

        $scope.getLanesForLoggedUser = function () {

            laneServices
                .getLanesForLoggedUser($stateParams.boardId)
              .success(function (data, status, headers, config) {
                   $scope.lanes = data;
                })
              .error(function (data, status, headers, config) {
                console.log(data);
            });
        
        };

   
        $scope.getLanesForLoggedUser();
    
    

       

        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
}]);
