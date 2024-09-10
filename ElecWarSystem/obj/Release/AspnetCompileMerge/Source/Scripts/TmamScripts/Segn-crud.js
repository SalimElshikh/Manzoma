window.onload = function () {
    setSegnCouter();
    RequestTmamStatus();
    numbersE2A();
}
function openSegnPopup() {
    document.querySelector(".Segn-popup").classList.add("act");
}
function closePop() {
    document.querySelector(".Segn-popup").classList.remove("act");
}

function IsAllFieldsFilled() {
    var result =
        $("#FardDetails-name").val() !== "" &&
        $("#FardDetails-Rotba").val() !== "" &&
        $("#crime").val() !== "" &&
        $("#punishment").val() !== "" &&
        $("#punisher").val() !== "" &&
        $("#date-from").val() !== "" &&
        $("#date-to").val() !== "" &&
        $("#Segn-place").val() !== "" &&
        $("#command-number").val() !== "" &&
        $("#command-date").val() !== ""
    
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

function emptyFormField() {
    $("#FardDetails-name").val(null)
    $("#FardDetails-Rotba").val(null)
    $("#crime").val(null)
    $("#punishment").val(null)
    $("#punisher").val(null)
    $("#date-from").val(null)
    $("#date-to").val(null)
    $("#Segn-place").val(null)
    $("#command-number").val(null)
    $("#command-date").val(null)
}

function Add() {
    $.ajax({
        url: window.location.origin + "/Segn/Create",
        type: "POST",
        async: false,
        data: {
            "SegnDetails.FardID": $("#FardDetails-name").val(),
            "SegnDetails.Gareema": $("#crime").val(),
            "SegnDetails.Eqab": $("#punishment").val(),
            "SegnDetails.Mo3aqeb": $("#punisher").val(),
            "SegnDetails.DateFrom": $("#date-from").val(),
            "SegnDetails.DateTo": $("#date-to").val(),
            "SegnDetails.SegnPlace": $("#Segn-place").val(),
            "SegnDetails.CommandItem.Number": $("#command-number").val(),
            "SegnDetails.CommandItem.Date": $("#command-date").val()
        },
        success: function (result) {
            if (result == -1) {
                alert("يوجد خطأ فى تاريخ السجن");
            }
            else {
                closePop();
                emptyFormField();
                UpdateSegnsTable();
                IncreaseSegnCounter()
                
            }
        }
    })
}




function UpdateSegnsTable() {
    $.ajax({
        url: window.location.origin + "/Segn/GetSegns",
        type: "GET",
        async: false,
        success: function (result) {
            fillSegnTable(result);
        }
    })
}

function fillSegnTable(result) {
    $("#Segn-table").empty();

    var tableHead = `
        <thead>
            <th>م</th>
            <th>الرتبة / الدرجة</th>
            <th>الإسم </th>
            <th>الجريمة</th>
            <th>العقوبة</th>
            <th>الآمر بالعقوبة</th>
            <th>مدة التنفذ من</th>
            <th>مدة التنفذ إلى</th>
            <th>مكان التنفيذ</th>
            <th colspan="2">بند الأوامر</th>
            <th></th>
        </thead>`;
    $("#Segn-table").append(tableHead);

    for (var index in result) {
        var tableItem = `
            <tbody style="font-size:14px;">
                <td>${parseInt(index) + 1}</td>
                <td>${result[index]['SegnDetails']['FardDetails']['Rotba']['RotbaName']}</td>
                <td>${result[index]['SegnDetails']['FardDetails']['FullName']}</td>
                <td>${result[index]['SegnDetails']['Gareema']}</td>
                <td>${result[index]['SegnDetails']['Eqab']}</td>
                <td>${result[index]['SegnDetails']['Mo3aqeb']}</td>
                <td>${getDateFormated(result[index]['SegnDetails']['DateFrom'])}</td>
                <td>${getDateFormated(result[index]['SegnDetails']['DateTo'])}</td>
                <td>${result[index]['SegnDetails']['SegnPlace']}</td>
                <td>${result[index]['SegnDetails']['CommandItem']['Number']}</td>
                <td>${getDateFormated(result[index]['SegnDetails']['CommandItem']['Date'])}</td>
                <td>
                    <button class="btn btn-danger" onclick="deleteSegn(${result[index]["ID"]})">
                        حذف
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>
                    
                </td>
            </tbody>`;
        $("#Segn-table").append(tableItem);
    }
}




function setSegnCouter() {
    var listOfSegnNumbers = $("#Segn-counter").text().split("/");
    var currentSegnCount = parseInt(listOfSegnNumbers[0]);
    var totalSegnCount = parseInt(listOfSegnNumbers[1]);
    if (totalSegnCount === currentSegnCount || timeOutCounter) {
        $("#add-Segn-btn").attr('disabled', 'disabled');
    } else {
        $("#add-Segn-btn").removeAttr('disabled');
    }
}

function IncreaseSegnCounter() {
    var listOfSegnNumbers = $("#Segn-counter").text().split("/");
    var currentSegnCount = parseInt(listOfSegnNumbers[0]);
    var totalSegnCount = parseInt(listOfSegnNumbers[1]);
    currentSegnCount += 1;
    if (totalSegnCount === currentSegnCount) {
        $("#add-Segn-btn").attr('disabled', 'disabled');
    }
    var newStr = `${currentSegnCount} / ${totalSegnCount}`;
    $("#Segn-counter").text(newStr);
}

function DecreaseSegnCounter() {
    var listOfSegnNumbers = $("#Segn-counter").text().split("/");
    var currentSegnCount = parseInt(listOfSegnNumbers[0]);
    var totalSegnCount = parseInt(listOfSegnNumbers[1]);
    currentSegnCount -= 1;
    $("#add-Segn-btn").removeAttr('disabled');
    var newStr = `${currentSegnCount} / ${totalSegnCount}`;
    $("#Segn-counter").text(newStr);
}

function deleteSegn(id) {
    $.ajax({
        url: window.location.origin + "/Segn/Delete",
        type: "POST",
        async: false,
        data: {
            "id": id,
        },
        success: function () {
            UpdateSegnsTable();
            DecreaseSegnCounter();
        }
    })
}