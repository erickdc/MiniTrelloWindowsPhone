'use strict';

angular.module('app.services').factory('LaneServices', ['$http', '$window', function ($http, $window) {

    var lane= {};

    var baseRemoteUrl = "http://minitrelloapierick.apphb.com";
    var baseLocalUrl = "http://localhost:1416";
    var baseUrl = baseRemoteUrl;

    lane.getLanesForLoggedUser = function (id) {
        return $http.get(baseUrl + '/boards/' + id + '/lanes/' + $window.sessionStorage.token);
    };

    
    lane.crearLane = function (model, id) {
        return $http.post(baseUrl + '/boards/' + id+ '/lanes/crearLane/' + $window.sessionStorage.token, model);
    };
    return lane;

}]);