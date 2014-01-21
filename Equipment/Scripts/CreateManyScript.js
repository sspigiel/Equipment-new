$('document').ready(function () {
        $('.datepicker').datepicker({ autoclose: true, todayHighlight: true, language: "pl" });
    $("#DeviceDictionaryId").attr("data-bind", "value:DeviceId");
    $("#DeviceDictionaryId").attr("required", "true");
    $("#DeviceDictionaryId").addClass("form-control");
    var Eq = function (name, price) {
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
            self.data.push(new Eq("", ""));
        };

        self.removeGift = function (eq) {
            self.data.remove(eq);
        };

        self.click = function () {
            var temp = $("#UserName").val();
            self.DeviceUser(temp);
        }

        self.save = function (form) {
            $.ajax({
                url: "/Devices/Read",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                datatype: "json",
                data: ko.toJSON(self),
                error: function (xmlHttpRequest, errorText, thrownError) {
                    alert("Wystąpił błąd "+ xmlHttpRequest+" "+errorText+" "+thrownError)
                },
                success: function (data) {
                    if (data != null) {
                        window.location.reload(true);
                    }
                }
            });
            //alert("Could now transmit to server: " + ko.utils.stringifyJson(self.data));
            // To actually transmit to server as a regular form post, write this: ko.utils.postJson($("form")[0], self.gifts);
        };
        self.number.subscribe(function (newValue) {
            if (newValue < self.data().length) {
                var temp = self.data().length - newValue;
                self.data.splice(self.data().length - temp, temp)
            }
            else {
                for (var i = self.data().length; i < newValue; i++) {
                    self.data.push(new Eq("", ""));
                }
            }
            
                $('.datepicker').datepicker({ autoclose: true, todayHighlight: true, language: "pl" });
            
        });
        
        self.initialize = function () {
            self.data.push(new Eq("1", "1"));
            
        };

        self.initialize();
    };
    ko.applyBindings(new ViewModel());


})
