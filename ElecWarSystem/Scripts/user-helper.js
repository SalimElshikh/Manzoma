function UpdateUnitComboBox() {
    $.ajax({
        url: window.location.origin + "/We7daRa2eeseya/GetUnitsByZone",
        type: "GET",
        async: true,
        data: {
            "Taba3eyaID": $("#zone-name").val()
        }, success: function (result) {
            $("#unit-name").empty();
            $("#unit-name").append("<option></option>");
            for (var index in result) {
                var item = `<option value="${result[index]['ID']}">${result[index]['We7daName']}</option>`;
                $("#unit-name").append(item);
            }
        }
    })
}

function SignUp() {
    $.ajax({
        url: window.location.origin + "/User/CreateAccount",
        type: "POST",
        async: false,
        data: {
            "UserName": $("#username").val(),
            "Password": $("#password").val(),
            "confirmPassword": $("#confirmPassword").val(),
            "We7daID": parseInt($("#unit-name").val())
        }
    })
}