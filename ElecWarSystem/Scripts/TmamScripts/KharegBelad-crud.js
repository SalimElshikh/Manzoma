window.onload = function () {
    setOutOfCountryCouter();
    RequestTmamStatus();
    numbersE2A();
    disableBtn(); // Check button state on page load
}

// Function to check if all fields are filled
function IsAllFieldsFilled() {
    return $("#FardDetails-name").val() !== "" &&
        $("#FardDetails-Rotba").val() !== "" &&
        $("#country").val() !== "" &&
        $("#purpose").val() !== "" &&
        $("#date-from").val() !== "" &&
        $("#date-to").val() !== "";
}

// Enable or disable submit button based on form validation
function disableBtn() {
    if (IsAllFieldsFilled()) {
        $(".popup-submit-btn").removeAttr('disabled');
    } else {
        $(".popup-submit-btn").attr('disabled', 'disabled');
    }
}

// Add a new entry for out-of-country details
function Add() {
    $.ajax({
        url: window.location.origin + "/KharegBelad/Create",
        type: "POST",
        async: false,
        data: {
            "outOfCountryDetail.FardID": $("#FardDetails-name").val(),
            "outOfCountryDetail.Balad": $("#country").val(),
            "outOfCountryDetail.Sabab": $("#purpose").val(),
            "outOfCountryDetail.DateFrom": $("#date-from").val(),
            "outOfCountryDetail.DateTo": $("#date-to").val()
        },
        success: function (result) {
            if (result == -1) {
                alert("يوجد خطأ فى التواريخ");
            } else {
                closePop(); // Close the modal
                UpdateOutOfCountriesTable(); // Refresh the table
                IncreaseOutOfCountryCounter(); // Update counter
                emptyFormField(); // Clear form fields
            }
        }
    });
}

// Clear form fields after submission
function emptyFormField() {
    $("#FardDetails-name").val(null);
    $("#FardDetails-Rotba").val(null);
    $("#country").val(null);
    $("#purpose").val(null);
    $("#date-from").val(null);
    $("#date-to").val(null);
}

// Update the table with the list of out-of-country entries
function UpdateOutOfCountriesTable() {
    $.ajax({
        url: window.location.origin + "/KharegBelad/GetOutOfCountries",
        type: "GET",
        async: false,
        success: function (result) {
            fillOutOfCountriesTable(result);
        }
    });
}

// Fill the table with returned data
function fillOutOfCountriesTable(result) {
    $("#KharegBelad-table").empty();

    var tableHead = `
        <thead>
            <tr>
                <th>م</th>
                <th>الرتبة / الدرجة</th>
                <th>الإسم </th>
                <th>جهة السفر</th>
                <th>الغرض من السفر</th>
                <th>المدة من</th>
                <th>المدة إلى</th>
                <th>الإجراءات</th>
            </tr>
        </thead>`;
    $("#KharegBelad-table").append(tableHead);

    for (var index in result) {
        var tableItem = `
            <tr style="font-size:14px;">
                <td>${parseInt(index) + 1}</td>
                <td>${result[index]['KharegBeladDetails']['FardDetails']['Rotba']['RotbaName']}</td>
                <td>${result[index]['KharegBeladDetails']['FardDetails']['FullName']}</td>
                <td>${result[index]['KharegBeladDetails']['Balad']}</td>
                <td>${result[index]['KharegBeladDetails']['Sabab']}</td>
                <td>${getDateFormated(result[index]['KharegBeladDetails']['DateFrom'])}</td>
                <td>${getDateFormated(result[index]['KharegBeladDetails']['DateTo'])}</td>
                <td>
                    <button class="btn btn-danger" onclick="deleteOutOfCountry(${result[index]["ID"]})">
                        حذف <span class="glyphicon glyphicon-remove"></span>
                    </button>
                </td>
            </tr>`;
        $("#KharegBelad-table").append(tableItem);
    }
}

// Open the modal for adding a new out-of-country entry
function openKharegBeladPopup() {
    $('#kharegBeladModal').modal('show');
}

// Close the modal
function closePop() {
    $('#kharegBeladModal').modal('hide');
}

// Handle the out-of-country counter
function setOutOfCountryCouter() {
    var listOfOutOfCountryNumbers = $("#KharegBelad-counter").text().split("/");
    var currentOutOfCountryCount = parseInt(listOfOutOfCountryNumbers[0]);
    var totalOutOfCountryCount = parseInt(listOfOutOfCountryNumbers[1]);
    if (totalOutOfCountryCount === currentOutOfCountryCount) {
        $("#add-KharegBelad-btn").attr('disabled', 'disabled');
    } else {
        $("#add-KharegBelad-btn").removeAttr('disabled');
    }
}

// Increase the out-of-country counter after adding an entry
function IncreaseOutOfCountryCounter() {
    var listOfOutOfCountryNumbers = $("#KharegBelad-counter").text().split("/");
    var currentOutOfCountryCount = parseInt(listOfOutOfCountryNumbers[0]);
    currentOutOfCountryCount += 1;
    $("#KharegBelad-counter").text(`${currentOutOfCountryCount} / ${listOfOutOfCountryNumbers[1]}`);
    if (currentOutOfCountryCount === parseInt(listOfOutOfCountryNumbers[1])) {
        $("#add-KharegBelad-btn").attr('disabled', 'disabled');
    }
}

// Decrease the out-of-country counter after deleting an entry
function DecreaseOutOfCountryCounter() {
    var listOfOutOfCountryNumbers = $("#KharegBelad-counter").text().split("/");
    var currentOutOfCountryCount = parseInt(listOfOutOfCountryNumbers[0]);
    currentOutOfCountryCount -= 1;
    $("#KharegBelad-counter").text(`${currentOutOfCountryCount} / ${listOfOutOfCountryNumbers[1]}`);
    $("#add-KharegBelad-btn").removeAttr('disabled');
}

// Delete an out-of-country entry
function deleteOutOfCountry(id) {
    $.ajax({
        url: window.location.origin + "/KharegBelad/Delete",
        type: "POST",
        async: false,
        data: { "id": id },
        success: function () {
            UpdateOutOfCountriesTable(); // Refresh table
            DecreaseOutOfCountryCounter(); // Update counter
        }
    });
}
