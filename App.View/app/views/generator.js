
SINGLEAPP.controller('generatorCtrl',
	[
		'$scope', '$http', '$compile','$timeout',
        function ($scope, $http, $compile, $timeout) {

            var REMOVEHOST = "http://localhost/SINGLEAPP/api/";


            $scope.generatedObjects = [];
            $scope.postmanErrors = [];


            $scope.download = function (value) {

                $scope.moduleName = value.moduleName;
                $scope.methods = value.methods;

                var newServiceData = document.getElementById('new-service');

                $timeout(function () {

                    var anchor = angular.element('<a/>');
                    anchor.attr({
                        href: 'data:attachment/text;charset=utf-8,' + encodeURI(newServiceData.outerHTML.replace(/<\/?[^>]+>/gi, '')),
                        target: '_blank',
                        download: "_" + $scope.moduleName + 'ServiceBase.js.txt'
                    })[0].click();

                });


                var baseModelData = document.getElementById('base-model');

                $timeout(function () {
                    var anchor = angular.element('<a/>');
                    anchor.attr({
                        href: 'data:attachment/text;charset=utf-8,' + encodeURI(baseModelData.outerHTML.replace(/<\/?[^>]+>/gi, '').replace(/&amp;/g, '&')),
                        target: '_blank',
                        download: "_" + $scope.moduleName + 'ModelBase.js.txt'
                    })[0].click();
                });


                var newModelData = document.getElementById('new-model');

                $timeout(function () {
                    var anchor = angular.element('<a/>');
                    anchor.attr({
                        href: 'data:attachment/text;charset=utf-8,' + encodeURI(newModelData.outerHTML.replace(/<\/?[^>]+>/gi, '')),
                        target: '_blank',
                        download: $scope.moduleName + 'Model.js.txt'
                    })[0].click();
                });
            }

            $scope.importPostmanJSON = function (postmanJSON) {

                var postmanObj = JSON.parse(postmanJSON);
                var requests = [];

                if (typeof postmanObj.collections !== 'undefined') {

                    angular.forEach(postmanObj.collections, function (collection) {
                        angular.forEach(collection.requests, function (request) {
                            requests.push(request);
                        });
                        
                    });
                }
                else {
                    requests = postmanObj.requests;

                }


                var newModules = [];

                //second pass lets build up the modules
                angular.forEach(requests, function (request) {

                    

                    var api = request.url.replace(REMOVEHOST, "");
                    var miniModule = api.split("/");
                    if (miniModule.length != 2) {
                        $scope.postmanErrors.push(miniModule);
                    }
                    else {

                        miniModule[0] = miniModule[0].toCamelCase().replace('_', '');
                        miniModule[1] = miniModule[1].toCamelCase().replace('_', '');

                        var newModule = _.find(newModules, function (obj) { return obj.moduleName == miniModule[0] })

                        if (typeof newModule === 'undefined') {
                            newModule = {};
                            newModule.moduleName = miniModule[0];
                            newModule.methods = [];
                            newModules.push(newModule);
                        }

                        var findMethodName = _.find(newModule.methods, function (method) { return method.name == miniModule[1] })

                        if (typeof findMethodName === 'undefined') {
                            var newMethod = {};
                            newMethod.name = miniModule[1];
                            newMethod.api = api;

                            newModule.methods.push(newMethod);
                        }
                        
                           

                    }
                    

                });

                //combine objexts
                angular.forEach(newModules, function (newModule) {
                    var findModule = _.find($scope.generatedObjects, function (obj) { return obj.moduleName == newModule.moduleName });

                    if (typeof findModule === 'undefined') {
                        findModule = newModule;
                        $scope.generatedObjects.push(findModule);
                    }

                    angular.forEach(newModule.methods, function (newMethod) {
                        var findMethodName = _.find(findModule.methods, function (method) { return method.name == newMethod.name })

                        if (typeof findMethodName === 'undefined') {
                            findModule.methods.push(newMethod);
                        }

                    });


                    

                });
                
           
            }


           

        }


	]
);