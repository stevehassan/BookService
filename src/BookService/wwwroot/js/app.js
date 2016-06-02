angular.module('bookApp',['ui.router','ngResource','bookApp.controllers','bookApp.services']);

angular.module('bookApp').config(function($stateProvider,$httpProvider){
    $stateProvider.state('books',{
        url:'/books',
        templateUrl: '/Api/BookList',
        controller:'BookListController'
    }).state('viewBook',{
        url: '/books/:id/view',
        templateUrl: 'Api/BookView',
       controller:'BookViewController'
    }).state('newBook',{
        url: '/books/new',
        templateUrl: 'Api/BookAdd',
        controller:'BookCreateController'
    }).state('editBook',{
        url: '/books/:id/edit',
        templateUrl: 'Api/BookEdit',
        controller:'BookEditController'
    });
}).run(function($state){
   $state.go('books');
});