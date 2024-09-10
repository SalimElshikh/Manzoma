var EditMode, ID, FardDetailsType;

function disableBtn() {
    if ($("#FardDetails-name").val() === "" ||
        $("#FardDetails-Rotba").val() === "" ||
        $("#FardDetails-Raqam3askary").val() === "") {

        $(".popup-submit-btn").attr('disabled', 'disabled');
    } else {
        $(".popup-submit-btn").removeAttr('disabled');
    }
}
function AddFardDetails(unitId, type) {
    $.ajax({
        url: window.location.origin + "/FardDetails/Create",
        type: "POST",
        async: false,
        data: {
            "We7daID": unitId,
            "Raqam3askary": $("#FardDetails-Raqam3askary").val(),
            "RotbaID": $("#FardDetails-Rotba").val(),
            "FullName": $("#FardDetails-name").val(),
            "Type": type
        }
    })
    $("#FardDetails-Raqam3askary").val(null)
    $("#FardDetails-Rotba").val(null)
    $("#FardDetails-name").val(null)
    updateFardDetailsTable(type);
}


function EditFardDetails(id, type) {
    $.ajax({
        url: window.location.origin + "/FardDetails/Edit",
        type: "POST",
        async: false,
        data: {
            "id": id,
            "Raqam3askary": $("#FardDetails-Raqam3askary").val(),
            "RotbaID": $("#FardDetails-Rotba").val(),
            "FullName": $("#FardDetails-name").val(),
            "Type": type
        }
    })
    $("#FardDetails-Raqam3askary").val(null)
    $("#FardDetails-Rotba").val(null)
    $("#FardDetails-name").val(null)
    updateFardDetailsTable(type);
}


function openFardDetailsPopup(id, type, editMode) {
    document.querySelector(".popup").classList.add("act");
    FardDetailsType = type;
    EditMode = editMode;
    ID = id;
    if (EditMode) {
        $.ajax({
            url: window.location.origin + "/FardDetails/GetFardDetails",
            type: "GET",
            async: false,
            data: {
                "id": id
            },
            success: function (result) {
                $("#FardDetails-Raqam3askary").val(result['Raqam3askary'])
                $("#FardDetails-Rotba").val(result['RotbaID'])
                $("#FardDetails-name").val(result['FullName'])
                disableBtn();
            }
        })
    } else {

    }
}
function saveFardDetailsChanges() {
    console.log(ID, FardDetailsType);
    if (EditMode) {
        EditFardDetails(ID, FardDetailsType);
    } else {
        AddFardDetails(ID, FardDetailsType);
    }
    closePop();
}
function closePop() {
    document.querySelector(".popup").classList.remove("act");
    $("#FardDetails-Raqam3askary").val(null)
    $("#FardDetails-Rotba").val(null)
    $("#FardDetails-name").val(null)
}

function updateFardDetailsTable(type) {
    $.ajax({
        url: window.location.origin + "/FardDetails/GetFardDetailss",
        type: "Get",
        async: false,
        data: {
            "type": type,
        },
        success: function (result) {
            FillTable(result, type);
        }
    })
}
function updateAfterDelete(id, type) {
    if (confirm("هل انت متأكد من حذف الشخص ؟")) {
        $.ajax({
            url: window.location.origin + "/FardDetails/Delete",
            type: "POST",
            async: false,
            data: {
                "id": id,
            },
            success: function (result) {
                if (result === "False") {
                    alert("لا يمكنك مسح عنصر من قيادة الوحدة");
                }
                updateFardDetailsTable(type);
            },
            error: function () {
                alert("Failed");
            }
        })
    }
}
function FillTable(result, type) {
    console.log(result);
    $("#FardDetailss-table").empty();
    var Rotba = (type == 1 ? "الرتبة" : "الدرجة")
    var table = `
            <thead>
                <th>م</th>
                <th>الرقم العسكرى</th>
                <th>${Rotba}</th>
                <th>الإسم</th>
                <th></th>
            </thead> `;
    $("#FardDetailss-table").append(table);
    for (var item in result) {
        console.log(result[item]['ID']);
        var tableitem = ` 
                <tbody>
                <tr>
                    <td>${parseInt(item) + 1}</td >
                    <td>${result[item]['Raqam3askary']}</td>
                    <td>${result[item]['Rotba']['RotbaName']}</td>
                    <td>${result[item]['FullName']}</td>
                    <td>
                        <button class="btn btn-danger" onclick="updateAfterDelete(${result[item]['ID']}, ${type})">
                            حذف
                            <span class="glyphicon glyphicon-remove"></span>
                        </button>
                        |
                        <button class="btn btn-success"  onclick="openFardDetailsPopup(${result[item]['ID']}, ${type}, ${true})">
                            تعديل
                            <span class="glyphicon glyphicon-edit"></span>
                        </button>
                    </td>
                </tr></tbody>`;
        $("#FardDetailss-table").append(tableitem);
    }
}

