SINGLEAPP.controller('navbarTopController', 
	['$scope', '$http', 'authentication.model', '$location', '$window', '$rootScope',
	function ($scope, $http, auth, $location, $window, $rootScope) {
		
		$rootScope.Open = false;

		$scope.Back = function () {
			$window.history.back();
		}

		$scope.Menu = function () {
			$rootScope.Open = !$rootScope.Open;
		}
	}]);