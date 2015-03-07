/// <reference path="jquery-1.8.2.js" />
/// <reference path="knockout-2.3.0.js" />
/// <reference path="paginationViewModel.js" />
/*
    Author:Stephen.Wang
    Date:2014-05-27
*/
"use strict";
(function (win, $) {
    $(function () {
        var vm = new viewModel();
        ko.applyBindings(vm, win.document.body);
        vm.goToFirst();
    });
    var viewModel = function () {
        var self = this;
        self.loading = ko.observable(true);
        self.items = ko.observableArray([]);
        paginationViewModel.apply(self, [10, function (page, pageHandler) {
            self.loading(true);
            $.ajax({
                url: "dataHandler.ashx",
                cache: false,
                data: {
                    pageIndex: page,
                    pageSize: self.pageSize
                },
                success: function (data) {
                    if (typeof data === "string") {
                        data = $.parseJSON(data);
                    }
                    pageHandler.call(self, data);
                    self.items(data.list);
                    self.loading(false);
                }
            });
        }]);
    }
})(window, jQuery);