angular.module('bookApp.services', ['ngResource']).factory('Api', function ($resource) {
    return {
        Book: $resource('/api/books/:id', { id: '@Id' }, {
            update: {
                method: 'PUT'
            },
        }),
        BookDetails: $resource('/api/books/:id/details', { id: '@Id' }, {
        }),
        Author: $resource('/api/authors/:id', { id: '@Id' }, {
            update: {
                method: 'PUT'
            }
        })
    }
})
.service('popupService',function($window){
    this.showPopup=function(message){
        return $window.confirm(message);
    }
});
