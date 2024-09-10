window.onload = function () {
    setAgazaCouter();
    RequestTmamStatus();
    numbersE2A();
}

function IsAllFieldsFilled() {
    var result =
        $("#FardDetails-name").val() !== "" &&
        $("#FardDetails-Rotba").val() !== "" &&
        $("#Agaza-type").val() !== "" &&
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
        url: window.location.origin + "/Agaza/Create",
        type: "POST",
        async: false,
        data: {
            "AgazaDetails.FardID": $("#FardDetails-name").val(),
            "AgazaDetails.AgazaType": $("#Agaza-type").val(),
            "AgazaDetails.DateFrom": $("#date-from").val(),
            "AgazaDetails.DateTo": $("#date-to").val()
        },
        success: function (result) {
            if (result == -1) {
                alert("يوجد خطأ فى تاريخ الأجازة");
            }
            else {
                closePop();
                UpdateAgazasTable();
                IncreaseAgazaCounter()
                emptyFormField()
            }
        }
    })
}

function emptyFormField() {
    $("#FardDetails-name").val(null)
    $("#FardDetails-Rotba").val(null)
    $("#Agaza-type").val(null)
    $("#date-from").val(null)
    $("#date-to").val(null)
}

function UpdateAgazasTable() {
    $.ajax({
        url: window.location.origin + "/Agaza/GetAgaza",
        type: "GET",
        async: false,
        success: function (result) {
            fillAgazaTable(result);
        }
    })
}

function fillAgazaTable(result) {
    $("#Agazas-table").empty();

    var tableHead = `
        <thead>
            <th>م</th>
            <th>الرتبة / الدرجة</th>
            <th>الإسم </th>
            <th>نوع الأجازة</th>
            <th>بدء الأجازة</th>
            <th>عودة الأجازة</th>
            <th></th>
        </thead>`;
    $("#Agazas-table").append(tableHead);

    for (var index in result) {
        var tableItem = `
            <tbody style="font-size:14px;">
                <td>${parseInt(index) + 1}</td>
                <td>${result[index]['AgazaDetails']['FardDetails']['Rotba']['RotbaName']}</td>
                <td>${result[index]['AgazaDetails']['FardDetails']['FullName']}</td>
                <td>${result[index]['AgazaDetails']['AgazaType']}</td>
                <td>${getDateFormated(result[index]['AgazaDetails']['DateFrom'])}</td>
                <td>${getDateFormated(result[index]['AgazaDetails']['DateTo'])}</td>
                <td>
                    <button class="btn btn-danger" onclick="deleteAgaza(${result[index]["ID"]})">
                        حذف
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>
                    
                </td>
            </tbody>`;
        $("#Agazas-table").append(tableItem);
    }
}

function openAgazaPopup() {
    document.querySelector(".Agaza-popup").classList.add("act");
}


function setAgazaCouter() {
    var listOfAgazaNumbers = $("#Agaza-counter").text().split("/");
    var currentAgazaCount = parseInt(listOfAgazaNumbers[0]);
    var totalAgazaCount = parseInt(listOfAgazaNumbers[1]);
    if (totalAgazaCount === currentAgazaCount || timeOutCounter) {
        $("#add-Agaza-btn").attr('disabled', 'disabled');
    } else {
        $("#add-Agaza-btn").removeAttr('disabled');
    }
}

function IncreaseAgazaCounter() {
    var listOfAgazaNumbers = $("#Agaza-counter").text().split("/");
    var currentAgazaCount = parseInt(listOfAgazaNumbers[0]);
    var totalAgazaCount = parseInt(listOfAgazaNumbers[1]);
    currentAgazaCount += 1;
    if (totalAgazaCount === currentAgazaCount) {
        $("#add-Agaza-btn").attr('disabled', 'disabled');
    }
    var newStr = `${currentAgazaCount} / ${totalAgazaCount}`;
    $("#Agaza-counter").text(newStr);
}

function DecreaseAgazaCounter() {
    var listOfAgazaNumbers = $("#Agaza-counter").text().split("/");
    var currentAgazaCount = parseInt(listOfAgazaNumbers[0]);
    var totalAgazaCount = parseInt(listOfAgazaNumbers[1]);
    currentAgazaCount -= 1;
    $("#add-Agaza-btn").removeAttr('disabled');
    var newStr = `${currentAgazaCount} / ${totalAgazaCount}`;
    $("#Agaza-counter").text(newStr);
}

function closePop() {
    document.querySelector(".Agaza-popup").classList.remove("act");
}

function deleteAgaza(id) {
    $.ajax({
        url: window.location.origin + "/Agaza/Delete",
        type: "POST",
        async: false,
        data: {
            "AgazaID": id,
        },
        success: function (result) {
            UpdateAgazasTable();
            DecreaseAgazaCounter();
        }
    })
    console.log(id);
}