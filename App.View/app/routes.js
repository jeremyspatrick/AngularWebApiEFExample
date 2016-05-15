SINGLEAPP.config(['$routeProvider', function($routeProvider) {
   $routeProvider
      .when('/', {
         templateUrl: 'app/views/login.html',
         controller: 'loginCtrl'
      })
      
      .otherwise({
         redirectTo: '/'
      });
   }]);