'use strict';

angular.module('app.services').factory('OrganizationServices', ['$http', '$window', function ($http, $window) {

    var organization = {};

    var baseRemoteUrl = "http://minitrelloapierick.apphb.com";
    var baseLocalUrl = "http://localhost:1416";
    var baseUrl = baseRemoteUrl;

    organization.getOrganizationsForLoggedUser = function () {
        return $http.get(baseUrl + '/organizations/' + $window.sessionStorage.token);
    };

    organization.createOrganization = function (model) {
        return $http.post(baseUrl + '/createOrganization/' + $window.sessionStorage.token, model);
    };
    organization.deleteOrganization = function (Id) {
        return $http.put(baseUrl + '/deleteOrganization/'+Id+'/'+ $window.sessionStorage.token);
    };
    return organization;

}]);