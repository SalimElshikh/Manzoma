window.onload = function () {
    setMaradyCouter();
    RequestTmamStatus();
    numbersE2A();
}



function IsAllFieldsFilled() {
    var result =
        $("#FardDetails-name").val() !== "" &&
        $("#FardDetails-Rotba").val() !== "" &&
        $("#Ma2moreya-place").val() !== "" &&
        $("#Ma2moreya-commandor").val() !== "" &&
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
        url: window.location.origin + "/Ma2moreya/Create",
        type: "POST",
        async: false,
        data: {
            "Ma2moreyaDetails.FardID": $("#FardDetails-name").val(),
            "Ma2moreyaDetails.Ma2moreyaPlace": $("#Ma2moreya-place").val(),
            "Ma2moreyaDetails.Ma2moreyaCommandor": $("#Ma2moreya-commandor").val(),
            "Ma2moreyaDetails.DateFrom": $("#date-from").val(),
            "Ma2moreyaDetails.DateTo": $("#date-to").val()
        },
        success: function (result) {
            if (result == -1) {
                alert("يوجد خطأ فى تاريخ المأموريات");
            }
            else {
                closePop();
                UpdateMa2moreyasTable();
                IncreaseMa2moreyaCounter();
                emptyFormField()
            }
        }
    })
}

function emptyFormField() {
    $("#FardDetails-name").val(null)
    $("#FardDetails-Rotba").val(null)
    $("#Ma2moreya-place").val(null)
    $("#Ma2moreya-commandor").val(null)
    $("#date-from").val(null)
    $("#date-to").val(null)
}

function UpdateMa2moreyasTable() {
    $.ajax({
        url: window.location.origin + "/Ma2moreya/GetMa2moreyas",
        type: "GET",
        async: false,
        success: function (result) {
            fillMaradyTable(result);
        }
    })
}

function fillMaradyTable(result) {
    $("#Ma2moreya-table").empty();
    var tableHead = `
        <thead>
            <th>م</th>
            <th>الرتبة / الدرجة</th>
            <th>الإسم </th>
            <th>جهة المأمورية</th>
            <th>الأمر بالمأمورية</th>
            <th>التاريخ من</th>
            <th>التاريخ إلى</th>
            <th></th>
        </thead>`;
    $("#Ma2moreya-table").append(tableHead);

    for (var index in result) {
        var tableItem = `
            <tbody style="font-size:14px;">
                <td>${parseInt(index) + 1}</td >
                <td>${result[index]['Ma2moreyaDetails']['FardDetails']['Rotba']['RotbaName']}</td>
                <td>${result[index]['Ma2moreyaDetails']['FardDetails']['FullName']}</td>
                <td>${result[index]['Ma2moreyaDetails']['Ma2moreyaPlace']}</td>
                <td>${result[index]['Ma2moreyaDetails']['Ma2moreyaCommandor']}</td>
                <td>${getDateFormated(result[index]['Ma2moreyaDetails']['DateFrom'])}</td>
                <td>${getDateFormated(result[index]['Ma2moreyaDetails']['DateTo'])}</td>
                <td>
                    <button class="btn btn-danger" onclick="deleteMa2moreya(${result[index]["ID"]})">
                        حذف
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>
                    
                </td>
            </tbody>`;

        $("#Ma2moreya-table").append(tableItem);
    }
}

function openMa2moreyaPopup() {
    document.querySelector(".Ma2moreya-popup").classList.add("act");
}

function setMaradyCouter() {
    var listOfMa2moreyaNumbers = $("#Ma2moreya-counter").text().split("/");
    var currentMa2moreyaCount = parseInt(listOfMa2moreyaNumbers[0]);
    var totalMa2moreyaCount = parseInt(listOfMa2moreyaNumbers[1]);
    if (totalMa2moreyaCount === currentMa2moreyaCount || timeOutCounter) {
        $("#add-Ma2moreya-btn").attr('disabled', 'disabled');
    } else {
        $("#add-Ma2moreya-btn").removeAttr('disabled');
    }
}

function IncreaseMa2moreyaCounter() {
    var listOfMa2moreyaNumbers = $("#Ma2moreya-counter").text().split("/");
    var currentMa2moreyaCount = parseInt(listOfMa2moreyaNumbers[0]);
    var totalMa2moreyaCount = parseInt(listOfMa2moreyaNumbers[1]);
    currentMa2moreyaCount += 1;
    if (totalMa2moreyaCount === currentMa2moreyaCount) {
        $("#add-Ma2moreya-btn").attr('disabled', 'disabled');
    }
    var newStr = `${currentMa2moreyaCount} / ${totalMa2moreyaCount}`;
    $("#Ma2moreya-counter").text(newStr);
}

function DecreaseMaradyCounter() {
    var listOfMa2moreyaNumbers = $("#Ma2moreya-counter").text().split("/");
    var currentMa2moreyaCount = parseInt(listOfMa2moreyaNumbers[0]);
    var totalMa2moreyaCount = parseInt(listOfMa2moreyaNumbers[1]);
    currentMa2moreyaCount -= 1;
    $("#add-Ma2moreya-btn").removeAttr('disabled');
    var newStr = `${currentMa2moreyaCount} / ${totalMa2moreyaCount}`;
    $("#Ma2moreya-counter").text(newStr);
}
function closePop() {
    document.querySelector(".Ma2moreya-popup").classList.remove("act");
}

function deleteMa2moreya(id) {
    $.ajax({
        url: window.location.origin + "/Ma2moreya/Delete",
        type: "POST",
        async: false,
        data: {
            "Ma2moreyaID": id,

        },
        success: function (result) {
            UpdateMa2moreyasTable();
            DecreaseMaradyCounter();
        }
    })
}