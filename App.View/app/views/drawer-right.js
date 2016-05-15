SINGLEAPP.controller('drawerRightController', 
	['$scope', '$http', 'authentication.model', '$location', '$window', '$rootScope',
	function ($scope, $http, auth, $location, $window, $rootScope) {		

		$scope.CloseMenu = function () {
			$rootScope.Open = !$rootScope.Open;
		}
	}]);