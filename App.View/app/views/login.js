SINGLEAPP.controller('loginCtrl', 
    ['$scope', '$http', 'authentication.model', '$location', 'roles',
    function ($scope, $http, authModel, $location, rolesModule) {

        delete localStorage.session;
        var auth = authModel.shared;

        $scope.SignIn = function (credentials) {

            auth.login(credentials).then(function (roles) {
                rolesModule.setRoles(roles);
                localStorage.roles = JSON.stringify(roles);
                $location.path('/loggedIn');

                angular.forEach(roles, function (role) {
                    if (role.RoleType == rolesModule.RoleType.Admin)
                    {
                        $location.path('/admin');
                    }
                });
               
            });
        }

    }
]);