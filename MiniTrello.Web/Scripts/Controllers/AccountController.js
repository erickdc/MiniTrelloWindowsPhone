'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')

  

    // Path: /login
    .controller('AccountController', ['$scope', '$location', '$window', 'AccountServices', function ($scope, $location, $window, AccountServices) {

        $scope.hasError = false;
    $scope.errorMessage = '';

        $scope.isLogged = function() {
            return $window.sessionStorage.token != null;
        };
        
        $scope.loginModel = { Email: '', Password: '' };
       
        $scope.registerModel = { Email: '', Password: '', FirstName: '', LastName: '', ConfirmPassword: '' };
        $scope.UpdateInformationModel = { Email: '', FirstName: '', LastName: '' };
        $scope.infoModel = { Email: '', FirstName: '', LastName: '' };
        $scope.recoverPasswordModel = { Email: '' };
        $scope.changePasswordModel = { Password: '', ConfirmPassword: '' };
        
        $scope.updateInformation = function () {

            AccountServices
                .updateInformation($scope.UpdateInformationModel)
               .success(function (data, status, headers, config) {

              })
              .error(function (data, status, headers, config) {
                  console.log(data);
              });

        };
        // TODO: Authorize a user
        $scope.login = function () {

            AccountServices
                .login($scope.loginModel)
              .success(function (data, status, headers, config) {
                  
                  $window.sessionStorage.token = data.Token;
                  $scope.goToOrganization();
              })
              .error(function (data, status, headers, config) {
                // Erase the token if the user fails to log in
                delete $window.sessionStorage.token;

                $scope.errorMessage = 'Error o clave incorrect';
                $scope.hasError = true;
                // Handle login errors here
                $scope.message = 'Error: Invalid user or password';
            });
            //$location.path('/');
        };

    $scope.goToRegister = function() {
        $location.path('/register');
    };
    $scope.goToLogin = function() {
        $location.path('/login');
    };
    $scope.goToOrganization = function () {
        $location.path('/organizations');
    };

    $scope.register = function() {
        AccountServices
            .register($scope.registerModel)
            .success(function (data, status, headers, config) {
                console.log(data);
                $scope.goToLogin();
            })
            .error(function (data, status, headers, config) {
                console.log(data);
            });
    };

    $scope.recoverPassword = function () {
        AccountServices
            .recoverPassword($scope.recoverPasswordModel)
            .success(function (data, status, headers, config) {
                console.log(data);
                $scope.goToLogin();
                
            })
            .error(function (data, status, headers, config) {
                console.log(data);
            });
    };

    
    $scope.changePassword = function () {
        AccountServices
            .changePassword($scope.changePasswordModel)
            .success(function (data, status, headers, config) {
                console.log(data);
                $scope.goToLogin();

            })
            .error(function (data, status, headers, config) {
                console.log(data);
            });
    };

    

    $scope.getInfo = function () {

        AccountServices
            .getInfo()
          .success(function (data, status, headers, config) {
              $scope.infoModel = data;
          })
          .error(function (data, status, headers, config) {
              console.log(data);
          });
        //$location.path('/');
    };

        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);