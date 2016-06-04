angular.module('bookApp.controllers',[]).controller('BookListController',function($scope,$state,$window,popupService, Api){

    $scope.books = Api.Book.query();

    $scope.deleteBook = function (book) {
        if (popupService.showPopup('Are you sure you want to delete "' + book.Title + '"?')) {
            Api.Book.delete({ 'id': book.Id }, function () {
                $state.go($state.current, {}, { reload: true });
            });
        }
    }

}).controller('BookViewController',function($scope,$stateParams,Api){

    $scope.book=Api.BookDetails.get({ id: $stateParams.id });

}).controller('BookCreateController', function ($scope, $state, $stateParams, Api) {

    $scope.book = new Api.Book();

    $scope.authors = Api.Author.query();

    $scope.addBook = function(){
        $scope.book.$save(function(){
            $state.go('books');
        });
    }

}).controller('BookEditController', function ($scope, $state, $stateParams, Api) {

    $scope.loadBook = function () {
        $scope.book = Api.Book.get({ id: $stateParams.id }, function () {
            $scope.authors = Api.Author.query(function (authors) {
                $.each(authors, function (index, author) {
                    if ($scope.book.AuthorId == author.Id) {
                        $scope.AuthorName = author.Name;
                    }
                });
            });
        });
    };

    $scope.loadBook();

    $scope.updateBook = function () {
        $scope.book.$update(function () {
            $state.go('books');
        });
    };
});
