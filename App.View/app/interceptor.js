var interceptor = ['$q', 'roles','$location', function ($q, rolesModule, $location) {
    return {
        request: function (request) {

            if (typeof localStorage.session !== 'undefined') {
                request.headers['session'] = localStorage.session;
            }
            if (typeof window.localStorageSetup === 'undefined' && typeof localStorage.roles !== 'undefined') {
                rolesModule.setRoles(JSON.parse(localStorage.roles));
                window.localStorageSetup = true;
            }
            return request;
        },

        response: function (response) {
            if (response && response.headers('session')) {
                localStorage.session = response.headers('session');
            }
            return response;
        },

        responseError: function (response) {
            // do something on error
            if (response.status == 401 || response.status == 403) {
                //toast error msg
                $location.path('/login');
                //window.location = "/login";
                return;
            }
            return $q.reject(response);
        }

    }
}];

angular.module('SINGLEAPP').config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push(interceptor);
}]);

