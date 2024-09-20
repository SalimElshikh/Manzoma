// يتم تشغيل هذه الدوال عند تحميل الصفحة
window.onload = function () {
    setMaradyCouter();      // تعيين عدد المأموريات
    RequestTmamStatus();    // طلب حالة الإتمام (قد تحتاج إلى إضافة هذه الدالة لاحقًا)
    numbersE2A();           // تحويل الأرقام (قد تحتاج إلى إضافة هذه الدالة لاحقًا)
    disableBtn();           // التحقق من تفعيل زر الحفظ بناءً على الحقول
}

// التحقق من أن جميع الحقول قد تم تعبئتها
function IsAllFieldsFilled() {
    var result =
        $("#FardDetails-name").val() !== "" &&
        $("#FardDetails-Rotba").val() !== "" &&
        $("#Ma2moreya-place").val() !== "" &&
        $("#Ma2moreya-commandor").val() !== "" &&
        $("#date-from").val() !== "" &&
        $("#date-to").val() !== "";
    return result;
}

// تفعيل أو تعطيل زر الحفظ بناءً على تعبئة الحقول
function disableBtn() {
    if (IsAllFieldsFilled()) {
        $(".popup-submit-btn").removeAttr('disabled');
    } else {
        $(".popup-submit-btn").attr('disabled', 'disabled');
    }
}

// إضافة مأمورية جديدة
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
            } else {
                closePop();                 // إغلاق النافذة المنبثقة
                UpdateMa2moreyasTable();    // تحديث جدول المأموريات
                IncreaseMa2moreyaCounter(); // زيادة العداد
                emptyFormField();           // إفراغ الحقول بعد الإضافة
            }
        }
    });
}

// إفراغ الحقول في النموذج
function emptyFormField() {
    $("#FardDetails-name").val(null);
    $("#FardDetails-Rotba").val(null);
    $("#Ma2moreya-place").val(null);
    $("#Ma2moreya-commandor").val(null);
    $("#date-from").val(null);
    $("#date-to").val(null);
}

// تحديث جدول المأموريات
function UpdateMa2moreyasTable() {
    $.ajax({
        url: window.location.origin + "/Ma2moreya/GetMa2moreyas",
        type: "GET",
        async: false,
        success: function (result) {
            fillMaradyTable(result); // تعبئة الجدول
        }
    });
}

// تعبئة جدول المأموريات
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

// فتح النافذة المنبثقة
function openMa2moreyaPopup() {
    $('#Ma2moreyaPopup').modal('show'); // استخدام المودال الخاص بـBootstrap
}

// إغلاق النافذة المنبثقة
function closePop() {
    $('#Ma2moreyaPopup').modal('hide'); // إخفاء المودال
}

// زيادة العداد عند إضافة مأمورية جديدة
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

// تقليل العداد عند حذف مأمورية
function DecreaseMaradyCounter() {
    var listOfMa2moreyaNumbers = $("#Ma2moreya-counter").text().split("/");
    var currentMa2moreyaCount = parseInt(listOfMa2moreyaNumbers[0]);
    var totalMa2moreyaCount = parseInt(listOfMa2moreyaNumbers[1]);
    currentMa2moreyaCount -= 1;
    $("#add-Ma2moreya-btn").removeAttr('disabled');
    var newStr = `${currentMa2moreyaCount} / ${totalMa2moreyaCount}`;
    $("#Ma2moreya-counter").text(newStr);
}

// حذف مأمورية
function deleteMa2moreya(id) {
    $.ajax({
        url: window.location.origin + "/Ma2moreya/Delete",
        type: "POST",
        async: false,
        data: {
            "Ma2moreyaID": id,
        },
        success: function (result) {
            UpdateMa2moreyasTable(); // تحديث الجدول بعد الحذف
            DecreaseMaradyCounter(); // تقليل العداد
        }
    });
}
