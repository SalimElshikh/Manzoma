var EditMode, ID, FardDetailsType;

function disableBtn() {
    var name = $("#FardDetails-name").val().trim();
    var raqam3askary = $("#FardDetails-Raqam3askary").val().trim();
    var rotba = $("#FardDetails-Rotba").val();

    // Check if all fields have valid values
    if (name !== "" && raqam3askary !== "" && rotba !== "") {
        $(".popup-submit-btn").removeAttr('disabled'); // Enable the button
    } else {
        $(".popup-submit-btn").attr('disabled', 'disabled'); // Disable the button
    }
}
// Attach 'input' event listener for text inputs and 'change' event for select dropdown
$(document).ready(function () {
    // Attach 'input' event listener for text inputs
    $("#FardDetails-name, #FardDetails-Raqam3askary").on('input', disableBtn);

    // Attach 'change' event listener for the select dropdown
    $("#FardDetails-Rotba").on('change', disableBtn);

    // Initial call to disableBtn() in case fields are pre-filled
    disableBtn();
});
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
        },
        success: function () {
            // Clear fields after adding
            $("#FardDetails-Raqam3askary").val(null);
            $("#FardDetails-Rotba").val(null);
            $("#FardDetails-name").val(null);
            updateFardDetailsTable(type);
        },
        error: function () {
            alert('Failed to add data');
        }
    });
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
        },
        success: function () {
            // Clear fields after editing
            $("#FardDetails-Raqam3askary").val(null);
            $("#FardDetails-Rotba").val(null);
            $("#FardDetails-name").val(null);
            updateFardDetailsTable(type);
        },
        error: function () {
            alert('Failed to edit data');
        }
    });
}

function openFardDetailsPopup(id, type, editMode) {
    // Show the modal (Bootstrap version)
    $("#draggablePopup").modal('show');

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
                $("#FardDetails-Raqam3askary").val(result['Raqam3askary']);
                $("#FardDetails-Rotba").val(result['RotbaID']);
                $("#FardDetails-name").val(result['FullName']);
                disableBtn(); // Enable/disable button based on input
            },
            error: function () {
                alert('Failed to retrieve data');
            }
        });
    } else {
        // Clear form if it's in Add Mode
        $("#FardDetails-Raqam3askary").val(null);
        $("#FardDetails-Rotba").val(null);
        $("#FardDetails-name").val(null);
    }
}

function saveFardDetailsChanges() {
    if (EditMode) {
        EditFardDetails(ID, FardDetailsType);
    } else {
        AddFardDetails(ID, FardDetailsType);
    }
    // Hide the modal (Bootstrap version)
    $("#draggablePopup").modal('hide');
}

function closePop() {
    $("#draggablePopup").modal('hide');
    $("#FardDetails-Raqam3askary").val(null);
    $("#FardDetails-Rotba").val(null);
    $("#FardDetails-name").val(null);
}

function updateFardDetailsTable(type) {
    $.ajax({
        url: window.location.origin + "/FardDetails/GetFardDetailss",
        type: "GET",
        async: false,
        data: {
            "type": type
        },
        success: function (result) {
            FillTable(result, type);
        },
        error: function () {
            alert('Failed to update table');
        }
    });
}

function updateAfterDelete(id, type) {
    if (confirm("هل انت متأكد من حذف الشخص ؟")) {
        $.ajax({
            url: window.location.origin + "/FardDetails/Delete",
            type: "POST",
            async: false,
            data: {
                "id": id
            },
            success: function (result) {
                if (result === "False") {
                    alert("لا يمكنك مسح عنصر من قيادة الوحدة");
                }
                updateFardDetailsTable(type);
            },
            error: function () {
                alert("Failed to delete data");
            }
        });
    }
}

function FillTable(result, type) {
    $("#FardDetailss-table").empty();
    var Rotba = (type == 1 ? "الرتبة" : "الدرجة");
    var table = `
        <thead>
            <tr>
                <th>م</th>
                <th>الرقم العسكرى</th>
                <th>${Rotba}</th>
                <th>الإسم</th>
                <th></th>
            </tr>
        </thead>
        <tbody></tbody>`;

    $("#FardDetailss-table").append(table);

    result.forEach((item, index) => {
        var tableItem = `
            <tr>
                <td>${index + 1}</td>
                <td>${item.Raqam3askary}</td>
                <td>${item.Rotba.RotbaName}</td>
                <td>${item.FullName}</td>
                <td>
                    <button class="btn btn-danger" onclick="updateAfterDelete(${item.ID}, ${type})">حذف</button> |
                    <button class="btn btn-info" onclick="openFardDetailsPopup(${item.ID}, ${type}, true)">تعديل</button>
                </td>
            </tr>`;
        $("#FardDetailss-table tbody").append(tableItem);
    });
}
