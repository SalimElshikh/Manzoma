window.onload = function () {
    setMo3askrCouter();
    RequestTmamStatus();
    numbersE2A();
}

function IsAllFieldsFilled() {
    var result =
        $("#FardDetails-name").val() !== "" &&
        $("#FardDetails-Rotba").val() !== "" &&
        $("#currentExistance").val() !== "" &&
        $("#reason").val() !== "" &&
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
        url: window.location.origin + "/Mo3askr/Create",
        type: "POST",
        async: false,
        data: {
            "Mo3askrDetails.FardID": $("#FardDetails-name").val(),
            "Mo3askrDetails.Twagod": $("#currentExistance").val(),
            "Mo3askrDetails.Sabab": $("#reason").val(),
            "Mo3askrDetails.DateFrom": $("#date-from").val(),
            "Mo3askrDetails.DateTo": $("#date-to").val()
        },
        success: function (result) {
            if (result == -1) {
                alert("يوجد خطأ فى النواريخ");
            }
            else {
                closePop();
                UpdateMo3askrsTable();
                IncreaseMo3askrCounter()
                emptyFormField()
            }
        }
    })
}


function emptyFormField() {
    $("#FardDetails-name").val(null)
    $("#FardDetails-Rotba").val(null)
    $("#currentExistance").val(null)
    $("#reason").val(null)
    $("#date-from").val(null)
    $("#date-to").val(null)
}

function UpdateMo3askrsTable() {
    $.ajax({
        url: window.location.origin + "/Mo3askr/GetMo3askrs",
        type: "GET",
        async: false,
        success: function (result) {
            fillMo3askrsTable(result);
        }
    })
}

function fillMo3askrsTable(result) {
    $("#Mo3askr-table").empty();

    var tableHead = `
        <thead>
            <th>م</th>
            <th>الرتبة / الدرجة</th>
            <th>الإسم </th>
            <th>مكان التمركز الحالى</th>
            <th>السبب</th>
            <th>المدة من</th>
            <th>المدة إلى</th>
            <th></th>
        </thead>`;
    $("#Mo3askr-table").append(tableHead);

    for (var index in result) {
        var tableItem = `
            <tbody style="font-size:14px;">
                <td>${parseInt(index) + 1}</td>
                <td>${result[index]['Mo3askrDetails']['FardDetails']['Rotba']['RotbaName']}</td>
                <td>${result[index]['Mo3askrDetails']['FardDetails']['FullName']}</td>
                <td>${result[index]['Mo3askrDetails']['Twagod']}</td>
                <td>${result[index]['Mo3askrDetails']['Sabab']}</td>
                <td>${getDateFormated(result[index]['Mo3askrDetails']['DateFrom'])}</td>
                <td>${getDateFormated(result[index]['Mo3askrDetails']['DateTo'])}</td>
                <td>
                    <button class="btn btn-danger" onclick="deleteMo3askr(${result[index]["ID"]})">
                        حذف
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>
                    
                </td>
            </tbody>`;
        $("#Mo3askr-table").append(tableItem);
    }
}

function openMo3askrPopup() {
    document.querySelector(".mo3askr-popup").classList.add("act");
}

function closePop() {
    document.querySelector(".mo3askr-popup").classList.remove("act");
}

function setMo3askrCouter() {
    var listOfMo3askrNumbers = $("#Mo3askr-counter").text().split("/");
    var currentMo3askrCount = parseInt(listOfMo3askrNumbers[0]);
    var totalMo3askrCount = parseInt(listOfMo3askrNumbers[1]);
    if (totalMo3askrCount === currentMo3askrCount || timeOutCounter) {
        $("#add-mo3askr-btn").attr('disabled', 'disabled');
    } else {
        $("#add-mo3askr-btn").removeAttr('disabled');
    }
}

function IncreaseMo3askrCounter() {
    var listOfMo3askrNumbers = $("#Mo3askr-counter").text().split("/");
    var currentMo3askrCount = parseInt(listOfMo3askrNumbers[0]);
    var totalMo3askrCount = parseInt(listOfMo3askrNumbers[1]);
    currentMo3askrCount += 1;
    if (totalMo3askrCount === currentMo3askrCount) {
        $("#add-mo3askr-btn").attr('disabled', 'disabled');
    }
    var newStr = `${currentMo3askrCount} / ${totalMo3askrCount}`;
    $("#Mo3askr-counter").text(newStr);
}

function DecreaseMo3askrCounter() {
    var listOfMo3askrNumbers = $("#Mo3askr-counter").text().split("/");
    var currentMo3askrCount = parseInt(listOfMo3askrNumbers[0]);
    var totalMo3askrCount = parseInt(listOfMo3askrNumbers[1]);
    currentMo3askrCount -= 1;
    $("#add-mo3askr-btn").removeAttr('disabled');
    var newStr = `${currentMo3askrCount} / ${totalMo3askrCount}`;
    $("#Mo3askr-counter").text(newStr);
}



function deleteMo3askr(id) {
    $.ajax({
        url: window.location.origin + "/Mo3askr/Delete",
        type: "POST",
        async: false,
        data: {
            "id": id,
        },
        success: function (result) {
            UpdateMo3askrsTable();
            DecreaseMo3askrCounter();
        }
    })
}