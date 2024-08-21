window.onload = function () {
    setMaradyCouter();
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
        url: window.location.origin + "/Marady/Create",
        type: "POST",
        async: false,
        data: {
            "MaradyDetails.FardID": $("#FardDetails-name").val(),
            "MaradyDetails.Mostashfa": $("#Mostashfa-name").val(),
            "MaradyDetails.MostashfaDate": $("#Mostashfa-date").val(),
            "MaradyDetails.Hala": $("#diagnosis").val(),
            "MaradyDetails.DateFrom": $("#date-from").val(),
            "MaradyDetails.DateTo": $("#date-to").val()
        },
        success: function (result) {
            if (result == -1) {
                alert("يوجد خطأ فى تاريخ الأجازة المرضية");
            }
            else {
                closePop();
                UpdateMa2moreyasTable();
                IncreaseMaradyCounter()
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
    $("#date-from").val(null)
    $("#date-to").val(null)
}

function UpdateMa2moreyasTable() {
    $.ajax({
        url: window.location.origin + "/Marady/GetMarady",
        type: "GET",
        async: false,
        success: function (result) {
            fillMaradyTable(result);
        }
    })
}

function fillMaradyTable(result) {
    $("#Marady-table").empty();

    var tableHead = `
        <thead>
            <th>م</th>
            <th>الرتبة / الدرجة</th>
            <th>الإسم </th>
            <th>المستشفى</th>
            <th>تاريخ دخول المستشفى</th>
            <th>التشخيص</th>
            <th>بدء الأجازة</th>
            <th>عودة الأجازة</th>
            <th></th>
        </thead>`;
    $("#Marady-table").append(tableHead);

    for (var index in result) {
        var tableItem = `
            <tbody style="font-size:14px;">
                <td>${parseInt(index) + 1}</td>
                <td>${result[index]['MaradyDetails']['FardDetails']['Rotba']['RotbaName']}</td>
                <td>${result[index]['MaradyDetails']['FardDetails']['FullName']}</td>
                <td>${result[index]['MaradyDetails']['Mostashfa']}</td>
                <td>${getDateFormated(result[index]['MaradyDetails']['MostashfaDate'])}</td>
                <td>${result[index]['MaradyDetails']['Hala']}</td>
                <td>${getDateFormated(result[index]['MaradyDetails']['DateFrom'])}</td>
                <td>${getDateFormated(result[index]['MaradyDetails']['DateTo'])}</td>
                <td>
                    <button class="btn btn-danger" onclick="deleteMarady(${result[index]["ID"]})">
                        حذف
                        <span class="glyphicon glyphicon-remove"></span>
                    </button>
                    
                </td>
            </tbody>`;
        $("#Marady-table").append(tableItem);
    }
}

function openMaradyPopup()
{
    document.querySelector(".Marady-popup").classList.add("act");
}


function setMaradyCouter() {
    var listOfMaradyNumbers = $("#Marady-counter").text().split("/");
    var currentMaradyCount = parseInt(listOfMaradyNumbers[0]);
    var totalMaradyCount = parseInt(listOfMaradyNumbers[1]);
    if (totalMaradyCount === currentMaradyCount || timeOutCounter) {
        $("#add-Marady-btn").attr('disabled', 'disabled');
    } else {
        $("#add-Marady-btn").removeAttr('disabled');
    }
}

function IncreaseMaradyCounter() {
    var listOfMaradyNumbers = $("#Marady-counter").text().split("/");
    var currentMaradyCount = parseInt(listOfMaradyNumbers[0]);
    var totalMaradyCount = parseInt(listOfMaradyNumbers[1]);
    currentMaradyCount += 1;
    if (totalMaradyCount === currentMaradyCount) {
        $("#add-Marady-btn").attr('disabled', 'disabled');
    }
    var newStr = `${currentMaradyCount} / ${totalMaradyCount}`;
    $("#Marady-counter").text(newStr);
}

function DecreaseMaradyCounter() {
    var listOfMaradyNumbers = $("#Marady-counter").text().split("/");
    var currentMaradyCount = parseInt(listOfMaradyNumbers[0]);
    var totalMaradyCount = parseInt(listOfMaradyNumbers[1]);
    currentMaradyCount -= 1;
    $("#add-Marady-btn").removeAttr('disabled');
    var newStr = `${currentMaradyCount} / ${totalMaradyCount}`;
    $("#Marady-counter").text(newStr);
}

function closePop() {
    document.querySelector(".Marady-popup").classList.remove("act");
}

function deleteMarady(id) {
    $.ajax({
        url: window.location.origin + "/Marady/Delete",
        type: "POST",
        async: false,
        data: {
            "id": id,
        },
        success: function (result) {
            UpdateMa2moreyasTable();
            DecreaseMaradyCounter();
        }
    })
    console.log(id);
}