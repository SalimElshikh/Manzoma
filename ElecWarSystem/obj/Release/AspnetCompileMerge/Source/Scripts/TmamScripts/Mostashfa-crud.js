window.onload = function ()
{
    setMostashfaCouter();
    RequestTmamStatus();
    numbersE2A();
}
function IsAllFieldsFilled() {
    var result =
        $("#FardDetails-name").val() !== "" &&
        $("#FardDetails-Rotba").val() !== "" &&
        $("#Mostashfa-name").val() !== "" &&
        $("#Mostashfa-date").val() !== "" &&
        $("#diagnosis").val() !== "" &&
        $("#recommendations").val() !== ""
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
        url: window.location.origin + "/Mostashfa/Create",
        type: "POST",
        async: false,
        data: {
            "MostashfaDetails.FardID": $("#FardDetails-name").val(),
            "MostashfaDetails.Mostashfa": $("#Mostashfa-name").val(),
            "MostashfaDetails.Hala": $("#diagnosis").val(),
            "MostashfaDetails.Tawseya": $("#recommendations").val(),
            "MostashfaDetails.DateFrom": $("#Mostashfa-date").val()
        },
        success: function (result) {
            if (result == -1) {
                alert("يوجد خطأ فى تاريخ دخول المستشفى !!");
            }
            else {
                closePop();
                UpdateMostashfasTable();
                IncreaseMostashfaCounter();
                emptyFormField()
            }
        }
    })
}

function emptyFormField() {
    $("#FardDetails-name").val(null)
    $("#FardDetails-Rotba").val(null)
    $("#Mostashfa-name").val(null)
    $("#Mostashfa-date").val(null)
    $("#diagnosis").val(null)
    $("#recommendations").val(null)
}

function UpdateMostashfasTable() {
    $.ajax({
        url: window.location.origin + "/Mostashfa/GetMostashfa",
        type: "GET",
        async: false,
        success: function (result) {
            fillMostashfaTable(result);
        }
    })
}

function fillMostashfaTable(result) {
    $("#Mostashfa-table").empty();
    var tableHead = `
        <thead>
            <th>م</th>
            <th>الرتبة / الدرجة</th>
            <th>الإسم </th>
            <th>المستشفى</th>
            <th>تاريخ دخول المستشفى</th>
            <th>التشخيص الطبى</th>
            <th>التوصيات الممنوحة</th>
            <th></th>
        </thead>`;
    $("#Mostashfa-table").append(tableHead);

    for (var index in result) {
        var tableItem = `
            <tbody style="font-size:14px;">
                <td>${parseInt(index) + 1}</td>
                <td>${result[index]['MostashfaDetails']['FardDetails']['Rotba']['RotbaName']}</td>
                <td>${result[index]['MostashfaDetails']['FardDetails']['FullName']}</td>
                <td>${result[index]['MostashfaDetails']['Mostashfa']}</td>
                <td>${getDateFormated(result[index]['MostashfaDetails']['DateFrom'])}</td>
                <td>${result[index]['MostashfaDetails']['Hala']}</td>
                <td>${result[index]['MostashfaDetails']['Tawseya']}</td>
                <td>
                    <button class="btn btn-danger" onclick="deleteMostashfa(${result[index]["ID"]})">
                        حذف
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>
                    
                </td>
            </tbody>`;

        $("#Mostashfa-table").append(tableItem);
    }
}

function openMostashfaPopup() {
    document.querySelector(".Mostashfa-popup").classList.add("act");
}

function setMostashfaCouter() {
    var listOfMostashfaNumbers = $("#Mostashfa-counter").text().split("/");
    var currentMostashfaCount = parseInt(listOfMostashfaNumbers[0]);
    var totalMostashfaCount = parseInt(listOfMostashfaNumbers[1]);
    if (totalMostashfaCount === currentMostashfaCount || timeOutCounter) {
        $("#add-Mostashfa-btn").attr('disabled', 'disabled');
    } else {
        $("#add-Mostashfa-btn").removeAttr('disabled');
    }
}

function IncreaseMostashfaCounter() {
    var listOfMostashfaNumbers = $("#Mostashfa-counter").text().split("/");
    var currentMostashfaCount = parseInt(listOfMostashfaNumbers[0]);
    var totalMostashfaCount = parseInt(listOfMostashfaNumbers[1]);
    currentMostashfaCount += 1;
    if (totalMostashfaCount === currentMostashfaCount) {
        $("#add-Mostashfa-btn").attr('disabled', 'disabled');
    }
    var newStr = `${currentMostashfaCount} / ${totalMostashfaCount}`;
    $("#Mostashfa-counter").text(newStr);
}

function DecreaseMostashfaCounter() {
    var listOfMostashfaNumbers = $("#Mostashfa-counter").text().split("/");
    var currentMostashfaCount = parseInt(listOfMostashfaNumbers[0]);
    var totalMostashfaCount = parseInt(listOfMostashfaNumbers[1]);
    currentMostashfaCount -= 1;
    $("#add-Mostashfa-btn").removeAttr('disabled');
    var newStr = `${currentMostashfaCount} / ${totalMostashfaCount}`;
    $("#Mostashfa-counter").text(newStr);
}
function closePop() {
    document.querySelector(".Mostashfa-popup").classList.remove("act");
}

function deleteMostashfa(id) {
    $.ajax({
        url: window.location.origin + "/Mostashfa/Delete",
        type: "POST",
        async: false,
        data: {
            "id": id,

        },
        success: function (result) {
            UpdateMostashfasTable();
            DecreaseMostashfaCounter();
        }
    })
}