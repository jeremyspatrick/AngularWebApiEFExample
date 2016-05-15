'use strict';
angular.module('hasRolesModule', []).factory('roles', function ($rootScope) {
    var _roles;
    var service = {};
    service.RoleType =
        {
            Admin: 1,
            Employee: 2
        }

    service.setRoles = function (roles) {
        _roles = roles;
        $rootScope.$broadcast('rolesChanged')
    }

    service.hasRoles = function (roles) {

        //TODO: Move to user object
        var RoleType = {
            "Admin": 0,
            "User": 1,
            "ReadOnly": 2
        }
        //I'm using int in this example, but a better approach would be to compare characters such as "Admin"
        var returnval = false;
        angular.forEach(roles, function (role) {
            if (_.pluck(_roles, "RoleType").indexOf(RoleType[role]) >= 0)
                returnval = true;
        });
        return returnval;
    }
    return service;
    
  });