$('document').ready(function () {
    var Eq = function () {
        var self = this;
        self.DeviceSerialNumber = ko.observable();
        self.Batch = ko.observable();
        self.StartDate = ko.observable();
        self.EndDate = ko.observable();

    };
    var ViewModel = function () {
        var self = this;
        self.data = ko.observableArray([]);
        self.DeviceUser = ko.observable();
        self.DeviceId = ko.observable();
        self.number = ko.observable(1);

        self.addGift = function () {
            self.data.push(new Eq());
        };

        self.removeGift = function (eq) {
            self.data.remove(eq);
            self.number(self.number - 1);
        };

        self.click = function () {
            var temp = $("#UserName").val();
            self.DeviceUser(temp);
        };

        self.save = function () {
            $.ajax({
                url: "/Devices/WriteToDatabase",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                datatype: "json",
                data: ko.toJSON(self),
                error: function (xmlHttpRequest, errorText, thrownError) {
                    alert("Wystąpił błąd " + xmlHttpRequest + " " + errorText + " " + thrownError);
                },
                success: function (data) {
                    if (data !== null) {
                        window.location.reload(true);
                    }
                }
            });
        };
        self.number.subscribe(function (newValue) {
            if (newValue < self.data().length) {
                var temp = self.data().length - newValue;
                self.data.splice(self.data().length - temp, temp);
            } else {
                var i;
                for (i = self.data().length; i < newValue; i++) {
                    self.data.push(new Eq());
                }
            }
            $('.datepicker').datepicker({ autoclose: true, todayHighlight: true, language: "pl" });
        });

        self.initialize = function () {
            self.data.push(new Eq());
        };

        self.initialize();
    };
    ko.applyBindings(new ViewModel());
    $('.datepicker').datepicker({ autoclose: true, todayHighlight: true, language: "pl" });
});
