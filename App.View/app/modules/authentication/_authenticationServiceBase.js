 
'use strict';
angular.module('authentication', []);
angular.module('authentication')
    .factory('authentication.service', ['$http', '$q', function ($http, $q) {
        //Return an instance of our backend service
        return function () {
            var service = {}      //Reference to the service we will return
        
              var loginRequest = {
                method: 'POST',
                url: API_BASE_URL + 'Authentication/Login',
                data: { }
            }

                    

  
            /**
             * Fetch login
             * @returns $http
             */
            service.login = function (options) {
                var deferred = $q.defer();
                loginRequest.data = options;
                $http(loginRequest).success(function (data) {
                    deferred.resolve(data);
                }).error(function (data, status, headers, config) {
                    if (status === 401) {
                        deferred.reject("Unauthorized");
                    } else {
                        deferred.reject(data);
                    }
                });
                return deferred.promise;
            }

            
            return service;
        }
    }]);
