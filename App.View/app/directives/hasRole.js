SINGLEAPP.directive('hasRoles', function (roles) {
    return {

        link: function (scope, element, attrs) {

            var arr = attrs.hasRoles.trim().replace(' ', '').split(',');

            function toggleView() {
                var hasRole = roles.hasRoles(arr);

                if (hasRole)
                    element.removeClass("hideElement");
                else
                    element.addClass("hideElement");
            }
            toggleView();
            scope.$on('rolesChanged', toggleView);
        }
    };
});