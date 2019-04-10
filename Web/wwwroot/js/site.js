// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#modal-url").change(getAndVerifyInputs);
$("#modal-username").change(getAndVerifyInputs);
$("#modal-submit").click(submit);

function submit() {
    const data = getAndVerifyInputs();
    if (!data.PostUrl || !data.PostName) {
        return;
    }

    const btn = $(this);
    const span = $("span", btn);

    span.empty();
    span.addClass("buffering");
    btn.prop("disabled", true);
    
    const request = $.ajax({
        url: "https://localhost:5001/List/Add",
        data: data,
        type: "POST",
        async: true,
        dataTypes: "json"
    });

    request.done(function(response) {
        span.removeClass();
        span.addClass("text-success");
        span.html("Successfully added");
    });

    request.fail(function(response) {
        span.removeClass();
        span.addClass("text-danger");
        span.html("Error: " + response.responseText);
    });
}

function getAndVerifyInputs() {
    const urlPattern = /^(https?:\/\/)?m\w{9}t\.net\/\w{5}\/\d+\/.+$/;
    const namePattern = /^[a-zA-Z_]{3,32}$/;
    
    return {
        PostUrl: getAndVerify("#modal-url", urlPattern),
        PostName: getAndVerify("#modal-username", namePattern)
    };
}

function getAndVerify(selector, pattern) {
    const form = $(selector);
    const input = form.val().trim();

    form.removeClass();
    form.addClass("form-control");

    if (pattern.test(input)) {
        form.addClass( "is-valid" );
        return input;
    } else {
        form.addClass("is-invalid");
        return null;
    }
}
