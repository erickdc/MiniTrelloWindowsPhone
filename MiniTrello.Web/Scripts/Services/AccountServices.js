'use strict';

angular.module('app.services', []).factory('AccountServices', ['$http', '$window', function ($http, $window) {

    var account = {};

    var baseRemoteUrl = "http://minitrelloapierick.apphb.com";
    var baseLocalUrl = "http://localhost:1416";
    var baseUrl = baseRemoteUrl;

    account.login = function (model) {
        return $http.post(baseUrl + '/login', model);
    };

    account.register = function (model) {
        return $http.post(baseUrl + '/register', model);
    };

    account.updateInformation = function (model) {
        return $http.put(baseUrl + '/updateInformation/' + $window.sessionStorage.token, model);
    };

    account.getInfo = function () {
        return $http.get(baseUrl + '/profile/' + $window.sessionStorage.token, model);
    };
    account.recoverPassword = function (model) {
        return $http.put(baseUrl + '/recoverPassword', model);
    };

    account.changePassword = function (model) {
        return $http.put(baseUrl + '/changePassword/' + $window.sessionStorage.token, model);
    };


    return account;

}]);