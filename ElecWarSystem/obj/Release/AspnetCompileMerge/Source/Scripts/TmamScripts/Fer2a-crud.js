(function () {
    var mod = angular.module("Fer2aMod", []);
    mod.controller("Fer2aCrud", function ($scope) {
        $scope.Name = "تمام الفرق و الدورات التعليمية"
        $scope.Fer2a = GetFer2as();
        var numbers = GetNumbers();
        $scope.Fer2asTotal = numbers["total"];
        $scope.Fer2asEntered = numbers["entered"];
        setFer2aAddStatus($scope.Fer2asTotal, $scope.Fer2asEntered)

        $scope.Add = function () {
            $.ajax({
                url: window.location.origin + "/Fer2a/Create",
                type: "POST",
                async: false,
                data: {
                    "Fer2aDetails.FardID": $("#FardDetails-name").val(),
                    "Fer2aDetails.Fer2aName": $("#Fer2a-name").val(),
                    "Fer2aDetails.Fer2aPlace": $("#Fer2a-place").val(),
                    "Fer2aDetails.DateFrom": $("#date-from").val(),
                    "Fer2aDetails.DateTo": $("#date-to").val(),
                    "Fer2aDetails.CommandItem.Number": $("#command-number").val(),
                    "Fer2aDetails.CommandItem.Date": $("#command-date").val() 
                },
                success: function (result) {
                    if (result == -1) {
                        alert("يوجد خطأ فى تاريخ الفرقة");
                    }
                    else {
                        $scope.closePop();
                        $scope.Fer2a = GetFer2as();
                        emptyFormField();
                        $scope.Fer2asEntered = $scope.Fer2asEntered + 1;
                        setFer2aAddStatus($scope.Fer2asTotal, $scope.Fer2asEntered);
                    }
                }
            })
        }

        $scope.delete = function (id) {
            $.ajax({
                url: window.location.origin + "/Fer2a/Delete",
                type: "POST",
                async: false,
                data: {
                    "id": id,
                },
                success: function () {
                    $scope.Fer2a = GetFer2as();
                    $scope.Fer2asEntered = $scope.Fer2asEntered - 1;
                    setFer2aAddStatus($scope.Fer2asTotal, $scope.Fer2asEntered);
                }
            })
        }

        $scope.openFer2aPopup = function () {
            document.querySelector(".Fer2a-popup").classList.add("act");
        }

        $scope.closePop = function() {
            document.querySelector(".Fer2a-popup").classList.remove("act");
        }
    });
})()
function validateFer2aName() {
    disableBtn();
    if ($("#Fer2a-name").val().length >= 25) {
        $("#Fer2a-name-warn").removeAttr('hidden');
    } else {
        $("#Fer2a-name-warn").attr('hidden', 'hidden');
    }

}
function setFer2aAddStatus(total, entered)
{
    if (total == entered) {
        console.log(total);
        console.log(entered);

        $("#add-Fer2a-btn").attr('disabled', 'disabled');
    } else {
        $("#add-Fer2a-btn").removeAttr('disabled');
    }
}

function GetFer2as() {
    var Fer2a = [];
    $.ajax({
        url: window.location.origin + "/Fer2a/GetFer2as",
        type: "GET",
        async: false,
        success: function (result) {
            Fer2a = result;
            
            for (var i in Fer2a)
            {
                Fer2a[i]["Fer2aDetails"]["DateFrom"] = getDateFormated(Fer2a[i]["Fer2aDetails"]["DateFrom"]);
                Fer2a[i]["Fer2aDetails"]["DateTo"] = getDateFormated(Fer2a[i]["Fer2aDetails"]["DateTo"]);
                Fer2a[i]["Fer2aDetails"]["CommandItem"]["Date"] = getDateFormated(Fer2a[i]["Fer2aDetails"]["CommandItem"]["Date"]);
            }
        }
    })
    return Fer2a;
}
function emptyFormField()
{
    $("#FardDetails-Rotba").val(null)
    $("#FardDetails-name").val(null)
    $("#Fer2a-name").val(null)
    $("#Fer2a-place").val(null)
    $("#date-from").val(null)
    $("#date-to").val(null)
    $("#command-number").val(null)
    $("#command-date").val(null)
}
function openFer2aPopup() {
    document.querySelector(".Fer2a-popup").classList.add("act");
}
function disableBtn() {
    if (IsAllFieldsFilled()) {
        $(".popup-submit-btn").removeAttr('disabled');
    }
    else {
        $(".popup-submit-btn").attr('disabled', 'disabled');
    }
}

function IsAllFieldsFilled() {
    var result =
        $("#FardDetails-name").val() !== "" &&
        $("#FardDetails-Rotba").val() !== "" &&
        $("#Fer2a-place").val() !== "" &&
        $("#Fer2a-place").val() !== "" &&
        $("#date-from").val() !== "" &&
        $("#date-to").val() !== "" &&
        $("#command-number").val() !== "" &&
        $("#command-date").val() !== ""
    return result;
}

function GetNumbers() {
    var numbers = [];
    $.ajax({
        url: window.location.origin + "/Fer2a/GetNumbers",
        type: "GET",
        async: false,
        success: function (result) {
            numbers = result;
        }
    })
    return numbers;
}