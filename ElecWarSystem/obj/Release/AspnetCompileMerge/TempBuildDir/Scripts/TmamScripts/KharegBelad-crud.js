window.onload = function () {
    setOutOfCountryCouter();
    RequestTmamStatus();
    numbersE2A();
}

function IsAllFieldsFilled() {
    var result =
        $("#FardDetails-name").val() !== "" &&
        $("#FardDetails-Rotba").val() !== "" &&
        $("#country").val() !== "" &&
        $("#purpose").val() !== "" &&
        $("#date-from").val() !== "" &&
        $("#date-to").val() !== ""
    return result;
}

function disableBtn() {
    if (IsAllFieldsFilled()) {
        $(".popup-submit-btn").removeAttr('disabled');
    }
    else {
        $(".popup-submit-btn").attr('disabled', 'disabled');
    }
}

function Add() {
    $.ajax({
        url: window.location.origin + "/KharegBelad/Create",
        type: "POST",
        async: false,
        data: {
            "outOfCountryDetail.FardID": $("#FardDetails-name").val(),
            "outOfCountryDetail.Balad": $("#country").val(),
            "outOfCountryDetail.Sabab": $("#purpose").val(),
            "outOfCountryDetail.DateFrom": $("#date-from").val(),
            "outOfCountryDetail.DateTo": $("#date-to").val()
        },
        success: function (result) {
            if (result == -1) {
                alert("يوجد خطأ فى النواريخ");
            }
            else {
                closePop();
                UpdateOutOfCountriesTable();
                IncreaseOutOfCountryCounter()
                emptyFormField()
            }
        }
    })
}


function emptyFormField() {
    $("#FardDetails-name").val(null)
    $("#FardDetails-Rotba").val(null)
    $("#country").val(null)
    $("#purpose").val(null)
    $("#date-from").val(null)
    $("#date-to").val(null)
}

function UpdateOutOfCountriesTable() {
    $.ajax({
        url: window.location.origin + "/KharegBelad/GetOutOfCountries",
        type: "GET",
        async: false,
        success: function (result) {
            fillOutOfCountriesTable(result);
        }
    })
}

function fillOutOfCountriesTable(result) {
    $("#KharegBelad-table").empty();

    var tableHead = `
        <thead>
            <th>م</th>
            <th>الرتبة / الدرجة</th>
            <th>الإسم </th>
            <th>جهة السفر</th>
            <th>الغرض من السفر</th>
            <th>المدة من</th>
            <th>المدة إلى</th>
            <th></th>
        </thead>`;
    $("#KharegBelad-table").append(tableHead);

    for (var index in result) {
        var tableItem = `
            <tbody style="font-size:14px;">
                <td>${parseInt(index) + 1}</td>
                <td>${result[index]['KharegBeladDetails']['FardDetails']['Rotba']['RotbaName']}</td>
                <td>${result[index]['KharegBeladDetails']['FardDetails']['FullName']}</td>
                <td>${result[index]['KharegBeladDetails']['Balad']}</td>
                <td>${result[index]['KharegBeladDetails']['Sabab']}</td>
                <td>${getDateFormated(result[index]['KharegBeladDetails']['DateFrom'])}</td>
                <td>${getDateFormated(result[index]['KharegBeladDetails']['DateTo'])}</td>
                <td>
                    <button class="btn btn-danger" onclick="deleteOutOfCountry(${result[index]["ID"]})">
                        حذف
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>
                    
                </td>
            </tbody>`;
        $("#KharegBelad-table").append(tableItem);
    }
}

function openKharegBeladPopup() {
    document.querySelector(".KharegBelad-popup").classList.add("act");
}

function closePop() {
    document.querySelector(".KharegBelad-popup").classList.remove("act");
}

function setOutOfCountryCouter() {
    var listOfOutOfCountryNumbers = $("#KharegBelad-counter").text().split("/");
    var currentOutOfCountryCount = parseInt(listOfOutOfCountryNumbers[0]);
    var totalOutOfCountryCount = parseInt(listOfOutOfCountryNumbers[1]);
    if (totalOutOfCountryCount === currentOutOfCountryCount || timeOutCounter) {
        $("#add-KharegBelad-btn").attr('disabled', 'disabled');
    } else {
        $("#add-KharegBelad-btn").removeAttr('disabled');
    }
}

function IncreaseOutOfCountryCounter() {
    var listOfOutOfCountryNumbers = $("#KharegBelad-counter").text().split("/");
    var currentOutOfCountryCount = parseInt(listOfOutOfCountryNumbers[0]);
    var totalOutOfCountryCount = parseInt(listOfOutOfCountryNumbers[1]);
    currentOutOfCountryCount += 1;
    if (totalOutOfCountryCount === currentOutOfCountryCount) {
        $("#add-KharegBelad-btn").attr('disabled', 'disabled');
    }
    var newStr = `${currentOutOfCountryCount} / ${totalOutOfCountryCount}`;
    $("#KharegBelad-counter").text(newStr);
}

function DecreaseOutOfCountryCounter() {
    var listOfOutOfCountryNumbers = $("#KharegBelad-counter").text().split("/");
    var currentOutOfCountryCount = parseInt(listOfOutOfCountryNumbers[0]);
    var totalOutOfCountryCount = parseInt(listOfOutOfCountryNumbers[1]);
    currentOutOfCountryCount -= 1;
    $("#add-KharegBelad-btn").removeAttr('disabled');
    var newStr = `${currentOutOfCountryCount} / ${totalOutOfCountryCount}`;
    $("#KharegBelad-counter").text(newStr);
}



function deleteOutOfCountry(id) {
    $.ajax({
        url: window.location.origin + "/KharegBelad/Delete",
        type: "POST",
        async: false,
        data: {
            "id": id,
        },
        success: function (result) {
            UpdateOutOfCountriesTable();
            DecreaseOutOfCountryCounter();
        }
    })
}