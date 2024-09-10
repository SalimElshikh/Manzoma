window.onload = function () {
    RequestTmamStatus();
}
function RequestTmamStatus() {
    $.ajax({
        url: window.location.origin + "/Tmam/GetTmamStatus",
        type: "GET",
        async: true,
        success: function (result) {
            ShowAlertAccordingToStatus(result);
        }
    })
}
function ShowAlertAccordingToStatus(status) {
    $("#alertBox").empty();
    var alertHtml = "";
    $(".hide-after-send").attr('hidden', 'hidden');
    if (!status["Submitted"] && !status["Recieved"]) {
        $(".hide-after-send").removeAttr('hidden');
        alertHtml = `<div class="alert alert-danger" role="alert">
                            إحترس ! لم يتم إرسال التمام حتى الأن
                        </div>`;
    } else if (status["Recieved"] && status["Submitted"]) {
        alertHtml = `<div class="alert alert-success" role="alert">
                        تم إرسال التمام إلى مركز عمليات 250 و تم تأكيد الإستلام بنجاح !
                    </div>`;
    }
    else if (!status["Recieved"] && status["Submitted"]) {
        alertHtml = `<div class="alert alert-info" role="alert">
                        تم إرسال التمام إلى مركز عمليات 250 و منتَظَر تأكيد الإستلام
                    </div>`;
    }


    $("#alertBox").append(alertHtml);
}
const timeFrom = new Date(1, 1, 1, 22, 0, 0);
const timeTo = new Date(1, 1, 1, 23, 59, 59);
var timeOutCounter = false

function getDateFormated(date) {
    date = new Date(parseInt(date.substr(6)));
    var yyyy = date.getFullYear();
    var mm = date.getMonth() + 1;
    var dd = date.getDate();
    if (mm < 10)
        mm = '0' + mm;
    if (dd < 10)
        dd = '0' + dd;
    return numbersEn2Ar(`${yyyy}/${mm}/${dd}`);
}

function calOut(power) {
    if ($("#existing").val() > power) {
        $("#existing").val(power);
    }
    $("#outdoor").val(power - $("#existing").val());
}

function getTimeInSeconds(date) {
    return (date.getHours() * 60 * 60) + (date.getMinutes() * 60) + date.getSeconds();
}

function timeInRange(timeInSeconds) {
    return (timeInSeconds > getTimeInSeconds(timeFrom) && timeInSeconds < getTimeInSeconds(timeTo));
}

function getTimeRemain(timeInSeconds) {
    var timeRamain = 0;
    var hours = Math.floor(timeInSeconds / (60 * 60));
    timeRamain = timeInSeconds % (60 * 60);
    hours = (hours >= 10) ? hours.toString() : `0${hours}`;
    var minutes = Math.floor(timeRamain / 60);
    minutes = (minutes >= 10) ? minutes.toString() : `0${minutes}`;
    var seconds = timeRamain % 60;
    seconds = (seconds >= 10) ? seconds.toString() : `0${seconds}`;
    return `${hours}:${minutes}:${seconds}`;
}
function numbersEn2Ar(number) {
    const en2ArDigits = { '0': '٠', '1': '١', '2': '٢', '3': '٣', '4': '٤', '5': '٥', '6': '٦', '7': '٧', '8': '٨', '9': '٩' };
    return number.toString().replace(/\d/g, (x) => en2ArDigits[x]);
}
setInterval(
    function () {
        const d = new Date();
        if (timeInRange(getTimeInSeconds(d))) {
            $("#time").text("00:00:00");
            $(".timeout").attr('readonly', 'readonly');
            $(".timeout-btn").attr('disabled', 'disabled');
            $(".hide-after-send").removeAttr('hidden');
            timeOutCounter = true;
        } else {
            var time = numbersEn2Ar(getTimeRemain(getTimeInSeconds(timeFrom) - getTimeInSeconds(d)));
            $("#time").text(time);
            $(".timeout").removeAttr('readonly');
            $(".timeout-btn").removeAttr('disabled');
            timeOutCounter = false;
        }
    }, 1000);

function calOut(power) {
    var power = parseInt($("#power").val());
    var exsting = parseInt($("#existing").val());
    var outing = power - exsting;
    $("#outdoor").val(outing);
}

function toTmamDetails(pg) {
    var sum = parseInt($("#Ma2moreya").val()) +
            parseInt($("#Agaza").val()) +
            parseInt($("#Marady").val()) +
            parseInt($("#Segn").val()) +
            parseInt($("#Gheyab").val()) +
            parseInt($("#Mostashfa").val()) +
            parseInt($("#KharegBelad").val()) +
            parseInt($("#Mo3askr").val()) +
            parseInt($("#Fer2a").val());

    if (parseInt($("#outdoor").val()) !== sum) {
        alert("خطأ فى تجميع التمام!!!" );
    }
    else {
        $.ajax({
            url: window.location.origin + "/Tmam/AddTmamDetail",
            type: "POST",
            async: false,
            data: {
                "IsOfficers": (pg == 1) ? true : false,
                "Qowwa": parseInt($("#power").val()),
                "Ma2moreya": parseInt($("#Ma2moreya").val()),
                "Agaza": parseInt($("#Agaza").val()),
                "Marady": parseInt($("#Marady").val()),
                "Segn": parseInt($("#Segn").val()),
                "Gheyab": parseInt($("#Gheyab").val()),
                "Mostashfa": parseInt($("#Mostashfa").val()),
                "KharegBelad": parseInt($("#KharegBelad").val()),
                "Mo3askar": parseInt($("#Mo3askr").val()),
                "Fer2a": parseInt($("#Fer2a").val()),
                "Tmam.Qa2edManoobID": parseInt($("#FardDetails-name").val())
            },
            success: function () {
                if (pg == 2) {
                    window.location.href = window.location.origin + "/Agaza/Index";
                }
                else {
                    window.location.href = window.location.origin + "/Tmam/Index?pg=" + (pg + 1);
                }
            }
        })
    }
}
function UpdateFardDetailsComboBox() {
    $.ajax({
        url: window.location.origin + "/FardDetails/GetFardDetailssOfRotba",
        type: "GET",
        async: false,
        data: {
            "RotbaID": $("#FardDetails-Rotba").val()
        }, success: function (result) {
            $("#FardDetails-name").empty();
            $("#FardDetails-name").append("<option></option>");
            for (var index in result) {
                var item = `<option value="${result[index]['ID']}">${result[index]['FullName']}</option>`;
                $("#FardDetails-name").append(item);
            }
        }
    })
}
function SubmitTmam() {
    var warning = confirm("إحذر عندما ترسل التمام إلى 250 لا يمكنك تعديله مرة أخرى طبقا لتعليمات إدارة الحرب الإلكترونية برجاء التأكد من صحة التمام قبل إرساله\n هل أنت متأكد من إرسال التمام؟");
    if (warning) {
        $.ajax({
            url: window.location.origin + "/Tmam/SubmiitTmam",
            type: "POST",
            async: false,
            success: function (result) {
                if (result == "") {
                    window.location.href = window.location.origin + "/Tmam/Review"
                } else {
                    alert(`يوجد الأخطاء التالية فى التمام!!! ${result}`);
                }
            }
        })
    }
}




