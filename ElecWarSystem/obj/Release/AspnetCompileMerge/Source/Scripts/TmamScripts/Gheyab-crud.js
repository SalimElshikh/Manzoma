window.onload = function ()
{    
    setGheyabCouter();
    RequestTmamStatus();
    numbersE2A();
}

function IsAllFieldsFilled() {
    var result =
        $("#FardDetails-name").val() !== "" &&
        $("#FardDetails-Rotba").val() !== "" &&
        $("#Gheyab-times").val() !== "" &&
        $("#date-from").val() !== "" &&
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

function Add() {
    $.ajax({
        url: window.location.origin + "/Gheyab/Create",
        type: "POST",
        async: false,
        data: {
            "GheyabDetails.FardID": $("#FardDetails-name").val(),
            "GheyabDetails.GheyabTimes": parseInt($("#Gheyab-times").val()),
            "GheyabDetails.DateFrom": $("#date-from").val(),
            "GheyabDetails.commandItem.Number": $("#command-number").val(),
            "GheyabDetails.commandItem.Date": $("#command-date").val(),
        },
        success: function (result) {
            if (result == -1) {
                alert("يوجد خطأ فى تاريخ الأجازة");
            }
            else {
                closePop();
                UpdateGheyabsTable();
                IncreaseGheyabCounter()
                emptyFormField()
            }
        }
    })
}

function emptyFormField()
{
    $("#FardDetails-name").val(null)
    $("#FardDetails-Rotba").val(null)
    $("#Gheyab-times").val(null)
    $("#date-from").val(null)
    $("#command-number").val(null)
    $("#command-date").val(null)
}

function UpdateGheyabsTable() {
    $.ajax({
        url: window.location.origin + "/Gheyab/GetGheyabs",
        type: "GET",
        async: false,
        success: function (result) {
            fillGheyabTable(result);
        }
    })
}

function fillGheyabTable(result) {
    $("#Gheyab-table").empty();

    var tableHead = `
        <thead>
            <th>م</th>
            <th>الرتبة / الدرجة</th>
            <th>الإسم </th>
            <th>التاريخ الغياب</th>
            <th>دفعة الغياب</th>
            <th colspan="2">بند الأوامر</th>
            <th></th>
        </thead>`;
    $("#Gheyab-table").append(tableHead);

    for (var index in result) {
        var tableItem = `
            <tbody style="font-size:14px;">
                <td>${parseInt(index) + 1}</td>
                <td>${result[index]['GheyabDetails']['FardDetails']['Rotba']['RotbaName']}</td>
                <td>${result[index]['GheyabDetails']['FardDetails']['FullName']}</td>
                <td>${getDateFormated(result[index]['GheyabDetails']['DateFrom'])}</td>
                <td>${result[index]['GheyabDetails']['GheyabTimes']}</td>
                <td>${result[index]['GheyabDetails']['commandItem']['Number']}</td>
                <td>${getDateFormated(result[index]['GheyabDetails']['commandItem']['Date'])}</td>
                <td>
                    <button class="btn btn-danger" onclick="deleteGheyab(${result[index]["ID"]})">
                        حذف
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>
                    
                </td>
            </tbody>`;
        $("#Gheyab-table").append(tableItem);
    }
}

function openGheyabPopup() {
    document.querySelector(".Gheyab-popup").classList.add("act");
}


function setGheyabCouter() {
    var listOfGheyabNumbers = $("#Gheyab-counter").text().split("/");
    var currentGheyabCount = parseInt(listOfGheyabNumbers[0]);
    var totalGheyabCount = parseInt(listOfGheyabNumbers[1]);
    if (totalGheyabCount === currentGheyabCount || timeOutCounter) {
        $("#add-Gheyab-btn").attr('disabled', 'disabled');
    } else {
        $("#add-Gheyab-btn").removeAttr('disabled');
    }
}

function IncreaseGheyabCounter() {
    var listOfGheyabNumbers = $("#Gheyab-counter").text().split("/");
    var currentGheyabCount = parseInt(listOfGheyabNumbers[0]);
    var totalGheyabCount = parseInt(listOfGheyabNumbers[1]);
    currentGheyabCount += 1;
    if (totalGheyabCount === currentGheyabCount) {
        $("#add-Gheyab-btn").attr('disabled', 'disabled');
    }
    var newStr = `${currentGheyabCount} / ${totalGheyabCount}`;
    $("#Gheyab-counter").text(newStr);
}

function DecreaseGheyabCounter() {
    var listOfGheyabNumbers = $("#Gheyab-counter").text().split("/");
    var currentGheyabCount = parseInt(listOfGheyabNumbers[0]);
    var totalGheyabCount = parseInt(listOfGheyabNumbers[1]);
    currentGheyabCount -= 1;
    $("#add-Gheyab-btn").removeAttr('disabled');
    var newStr = `${currentGheyabCount} / ${totalGheyabCount}`;
    $("#Gheyab-counter").text(newStr);
}

function closePop() {
    document.querySelector(".Gheyab-popup").classList.remove("act");
}

function deleteGheyab(id) {
    $.ajax({
        url: window.location.origin + "/Gheyab/Delete",
        type: "POST",
        async: false,
        data: {
            "GheyabID": id,
        },
        success: function (result) {
            UpdateGheyabsTable();
            DecreaseGheyabCounter();
        }
    })
    console.log(id);
}