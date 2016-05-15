 
    
'use strict';
angular.module('authentication')
    .factory('authentication.baseModel', ['authentication.service', '$q', '$location', '$rootScope', '$timeout', function (authentication, $q, $location, $rootScope, $timeout) {
        var service = {};   //The backend service we will return
        /**
         * Creates instance
         * @returns 
         */
        service.instance = function () {
            var serviceInstance = {};
            var _authentication = authentication();
            
            //model's data
            serviceInstance.reset = function() {

            }
            serviceInstance.reset();
            
            serviceInstance.modelTimeout = DEFAULT_CACHE_TIMEOUT;
            var cacheExists = undefined;
            //reset model after expiration
            var setModelExpiration = function () {
                if (angular.isUndefined(cacheExists) && angular.isDefined(serviceInstance.modelTimeout) && serviceInstance.modelTimeout != 0) {
                    cacheExists = true;
                    //destroy cache after default time
                    $timeout(function() {
                        serviceInstance.reset();
                        cacheExists = undefined;
                    }, serviceInstance.modelTimeout);
                }
            }

            serviceInstance.base = {} //base methods
    
    
            /**
             * 
             * @returns 
             */
            serviceInstance.base.login = function (options) 
            {
                var promise = _authentication.login(options);
                promise.then(function (response) {
 
                }, function (errorMessage) {
           
                });
                return promise;
            }
             
     
         
            
            /*
                add methods that were not overridden
            */
            var addMethods = function () {
                for (var prop in serviceInstance.base) {
                    if (serviceInstance.base.hasOwnProperty(prop) && typeof serviceInstance[prop] === 'undefined') {
                        serviceInstance[prop] = serviceInstance.base[prop];
                    }
                }
            }
            addMethods();

            return serviceInstance;
        }
        service.shared = service.instance();    //Singleton used to share item among several controllers
        return service;
    }]);
