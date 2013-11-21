$(function () {
    $("#actionLink").click(function () {
        var subject = $('input[name=radio]:checked').val();
        var body = $("#txtBody").val();
        var emailFromName = $("#txtName").val();
        var emailFromAddress = $("#txtEmail").val();
        if ((emailFromName.length > 0 && emailFromName.length <= 100) && (emailFromAddress.length > 0 && emailFromAddress.length <= 100)) {
            support(subject, body,  emailFromAddress, emailFromName)
        }
        else {
            $("#errorMessageDiv").attr("class", "alertmessage");
            window.setTimeout(function () { $("#errorMessageDiv").attr("class", "alertmessage hidden"); }, 3000);
        }
    });
    $("#actionLink").click(support);
});

function support(subject, body, emailFromAddress, emailFromName) {
    $.ajax({
        url: window.location.protocol + '//' + window.location.host + '/Handler/SupportEmailHandler.ashx',
        cache: false,
        type: 'Post',
        async: true,
        data: { subject: subject, body: body, emailFromAddress: emailFromAddress, emailFromName: emailFromName },
        success: function (data) {
            if (data == "success") {
                $("#successMessageDiv").attr("class", "confirmationmessage");
                window.setTimeout(function () { $("#successMessageDiv").attr("class", "confirmationmessage hidden"); }, 3000);
                $("#radio").attr("checked", "checked");
                $("#txtName").val("");
                $("#txtEmail").val("");
                $("#txtBody").val("");
            }
            else {
                $("#errorMessageDiv").attr("class", "alertmessage");
                window.setTimeout(function () { $("#errorMessageDiv").attr("class", "alertmessage hidden"); }, 3000);

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ", " + thrownError);
        }
    });
}