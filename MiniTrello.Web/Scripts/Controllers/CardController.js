/*'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')



    // Path: /login
    .controller('CardController', ['$scope', '$location', '$window', 'CardServices','$stateParams', function ($scope, $location, $window, cardServices, $stateParams) {


      // $scope.boardDetailId = $stateParams.boardId;
         
        //console.log($location.search().boardId);

        console.log($scope.boardDetailId);
        $scope.createCardModel = {LanesId: '', Name: '', Description: '' };
        $scope.cards = [];
       
        
        //var board1 = { Id: 2, Name: 'Myboard2', Description: 'Description2' };
       // $scope.boards.push(board);
     //   $scope.boards.push(board1);
        $scope.createCard= function () {

            cardServices
           .createCard($scope.createCardModel)
           .success(function (data, status, headers, config) {
               console.log(data);
               $location.path('/lanes/'+$stateParams.laneId+'/cards');
           })
           .error(function (data, status, headers, config) {
               console.log(data);
           });
        };

        $scope.getCardsForLoggedUser = function () {

            boardServices
                .geCardsForLoggedUser($stateParams.laneId)
              .success(function (data, status, headers, config) {
                   $scope.cards = data;
                })
              .error(function (data, status, headers, config) {
                console.log(data);
            });
        
        };

    if ($scope.boardDetailId > 0)
    {
        //get board details
    }
    else
    {
        $scope.getBoardsForLoggedUser();
    }
    
    
       

        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);

*/