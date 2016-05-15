'use strict';
var SINGLEAPP = angular.module('SINGLEAPP', 
	['ngRoute', 
   'ui.bootstrap',
   'hasRolesModule',
   'authentication',
	
   'ngAnimate'
]);

SINGLEAPP.run(['$rootScope', '$location', function ($rootScope, $location) {
   $rootScope.$on('$routeChangeStart', function(next, current) { 

   	//is the route a logged in path?
   	if (OUTSIDE_PATHS.indexOf($location.path()) !== -1) {
   		$rootScope.outside = true;
   	} else {
   		$rootScope.outside = false;
   	}

	}); 
}]);	