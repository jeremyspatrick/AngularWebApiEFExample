               
'use strict';
angular.module('authentication')
    .factory('authentication.model', ['authentication.baseModel', '$q', '$location', function (authenticationBase, $q, $location) {
        var service = {};   //The backend service we will return
        /**
         * Creates instance
         * @returns 
         */
        service.instance = function () {
           var serviceInstance = authenticationBase.instance();
            
            /*
                override model timeout here.  zero timeout turns off caching
            */
            
                serviceInstance.modelTimeout = 0;
            

             /*
                override methods here
            */
            /*
            serviceInstance.exampleMethod = function (options) {
                var promise = serviceInstance.base.exampleMethod(options);
                promise.then(function (response) {
                    //**********
                    // insert override logic
                    //**********
                });
                return promise;
            }
        
            */

            return serviceInstance;
        }
        service.shared = service.instance();    //Singleton used to share item among several controllers
        return service;
    }]);
