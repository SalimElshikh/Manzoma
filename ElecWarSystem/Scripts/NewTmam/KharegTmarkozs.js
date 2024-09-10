let mo3edatData = [];
let asl7aData = [];
let za5iraData = [];
let markbatData = [];

let selectedAsl7aNameId;
let selectedMo3edatNameId;
let selectedMarkbatNameId;
let selectedZa5iraNameId;
let A8radTa7arokID;
let selectedValue;
let selectedText;
let NameQa2ed;
let Rotba;

function openModal(modalId) {
    $(modalId).modal('show');
}

function openMo3edatPopup() {
    openModal('#mo3edatModal');
}

function openAsl7aPopup() {
    openModal('#asl7aModal');
}

function openZa5iraPopup() {
    openModal('#za5iraModal');
}

function openMarkbatPopup() {
    openModal('#markbatModal');
}

$(document).ready(function () {
    $('#A8radTmarkozDropdown').change(function () {
        selectedValue = $(this).val();
        selectedText = $("#A8radTmarkozDropdown option:selected").text();
    });
    $('#RotbaQa2edTmarkoz').change(function () {
        RotbaID = $(this).val();
        Rotba = $("#RotbaQa2edTmarkoz option:selected").text();
    });
    $('#Qa2edTmarkoz').change(function () {
        NameQa2edID = $(this).val();
        NameQa2ed = $("#Qa2edTmarkoz option:selected").text();
    });
    // Attach event listeners to the form submit buttons
    ["Mo3edat", "Asl7a", "Za5ira", "Markbat"].forEach(type => {
        $(`#addNew${type}Form`).submit(function (event) {
            event.preventDefault();
            addNewData(type);
        });
    });

    $("#createForm").submit(function (event) {
        event.preventDefault();
        submitAllData();
    });

    // Initialize dropdown change event listeners
    initializeDropdownChange('#DropDownListAsl7aName', selectedAsl7aNameId);
    initializeDropdownChange('#DropDownListMo3edatName', selectedMo3edatNameId);
    initializeDropdownChange('#DropDownListMarkbatName', selectedMarkbatNameId);
    initializeDropdownChange('#DropDownListZa5iraName', selectedZa5iraNameId);
    initializeDropdownChange('#A8radTa7arokID', A8radTa7arokID);
});

function initializeDropdownChange(dropdownId, selectedVariable) {
    let dropdown = $(dropdownId);
    if (dropdown.length > 0) {
        dropdown.change(function () {
            selectedVariable = $(this).val();
            console.log(`Selected value in Modal: ${selectedVariable}`);
        });
    }
}

function addNewData(type) {
    let name = $(`#${type.toLowerCase()}Name`).val();
    let num = $(`#${type.toLowerCase()}Num`).val();
    let selectedName;

    switch (type) {
        case "Asl7a":
            selectedName = $('#DropDownListAsl7aName option:selected').text();
            asl7aData.push({ Name: selectedName, Num: num });
            appendRowToTable(type, { Name: selectedName, Num: num });
            break;
        case "Mo3edat":
            selectedName = $('#DropDownListMo3edatName option:selected').text();
            mo3edatData.push({ Name: selectedName, Num: num });
            appendRowToTable(type, { Name: selectedName, Num: num });
            break;
        case "Markbat":
            selectedName = $('#DropDownListMarkbatName option:selected').text();
            markbatData.push({ Name: selectedName, Num: num });
            appendRowToTable(type, { Name: selectedName, Num: num });
            break;
        case "Za5ira":
            selectedName = $('#DropDownListZa5iraName option:selected').text();
            za5iraData.push({ Name: selectedName, Num: num });
            appendRowToTable(type, { Name: selectedName, Num: num });
            break;
    }

    emptyForm(type);
    $(`#${type.toLowerCase()}Modal`).modal('hide');
}

function emptyForm(type) {
    $(`#${type.toLowerCase()}Name`).val('');
    $(`#${type.toLowerCase()}Num`).val('');
}

function appendRowToTable(type, item) {
    let tableId = `${type.toLowerCase()}sTable`;

    var table = $(`#${tableId} tbody`);
    if (table.length === 0) {
        $(`#${tableId}`).append('<tbody></tbody>');
        table = $(`#${tableId} tbody`);
    }

    var row = `
        <tr>
            <td>${item.Num}</td>
            <td>${item.Name}</td>
            <td>
                <button onclick="deleteRow('${type}', ${table.children().length})"><i class="fa-regular fa-trash-can"></i></button>
            </td>
        </tr>`;

    table.append(row);
}

function deleteRow(type, index) {
    switch (type) {
        case "Mo3edat":
            mo3edatData.splice(index, 1);
            break;
        case "Asl7a":
            asl7aData.splice(index, 1);
            break;
        case "Za5ira":
            za5iraData.splice(index, 1);
            break;
        case "Markbat":
            markbatData.splice(index, 1);
            break;
    }

    removeTableRow(type, index);
}
function updateFardDetails() {
    var rotbaID = $('#RotbaQa2edTmarkoz').val();
    var we7daID = 3;

    $.ajax({
        url: 'GetFardDetailsByRotbaAndWe7da',
        type: 'POST',
        data: { rotbaID: rotbaID, we7daID: we7daID },
        success: function (data) {
            var fardDetailsDropdown = $('#Qa2edTmarkoz');
            fardDetailsDropdown.empty();
            fardDetailsDropdown.append($('<option></option>').val('').html('اختار القائد'));
            $.each(data, function (i, fardDetail) {
                fardDetailsDropdown.append($('<option></option>').val(fardDetail.ID).html(fardDetail.FullName));
            });
        }
    });
}

function removeTableRow(type, index) {
    let tableId = `${type.toLowerCase()}sTable`;

    var table = $(`#${tableId} tbody`);
    $(table.children()[index]).remove();
}

function getAntiForgeryToken() {
    return $('input[name="__RequestVerificationToken"]').val();
}


function submitAllData() {
    let allData = {
        KharegTmarkoz: {
            Qa2edTmarkoz: NameQa2ed,
            MakanTmarkoz7ali: $('#MakanTmarkoz7ali').val(),
            DobatNum: $('#DobatNum').val(),
            DargatNum: $('#DargatNum').val(),
            DateFrom: $('#DateFrom').val(),
            DateTo: $('#DateTo').val(),
            RotbaQa2edTmarkoz: Rotba
        },
        Mo3edats: mo3edatData,
        Asl7as: asl7aData,
        Za5iras: za5iraData,
        Markbats: markbatData
    };

    console.log("Submitting all data:", allData);

    $.ajax({
        url: 'GetData',
        type: "GET",
        data: JSON.stringify(allData),
        contentType: "application/json",
        headers: {
            "RequestVerificationToken": getAntiForgeryToken()
        },
        success: function (result) {
            if (result.success) {
                alert("Data saved successfully!");
                clearAllTables();
            } else {
                alert(result.message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error occurred:", status, error);
            alert("An error occurred: " + error);
        }
    });
}

function clearAllTables() {
    ["Mo3edat", "Asl7a", "Za5ira", "Markbat"].forEach(type => {
        $(`#${type.toLowerCase()}sTable tbody`).empty();
    });
}
