'use strict';

angular.module('app.services').factory('BoardServices', ['$http', '$window', function ($http, $window) {

    var board = {};

    var baseRemoteUrl = "http://minitrelloapierick.apphb.com";
    var baseLocalUrl = "http://localhost:1416";
    var baseUrl = baseRemoteUrl;

    board.getBoardsForLoggedUser = function (id) {
        return $http.get(baseUrl + '/organizations/' + id + '/boards/' + $window.sessionStorage.token);
    };

    
    board.crearBoard = function (model, id) {
        return $http.post(baseUrl + '/organizations/' + id+ '/boards/crearBoard/' + $window.sessionStorage.token, model);
    };
    return board;

}]);