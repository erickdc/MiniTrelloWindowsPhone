/*'use strict';

angular.module('app.services').factory('BoardServices', ['$http', '$window', function ($http, $window) {

    var board = {};

    var baseRemoteUrl = "http://minitrelloapi.apphb.com";
    var baseLocalUrl = "http://localhost:1416";
    var baseUrl = baseLocalUrl;

    board.getBoardsForLoggedUser = function (id) {
        return $http.get(baseUrl + '/organizations/' + id + '/boards/' + $window.sessionStorage.token);
    };

    
    board.createBoard = function (model) {
        return $http.post(baseUrl + '/createBoard/' + $window.sessionStorage.token, model);
    };
    return board;
    //
}]);*/