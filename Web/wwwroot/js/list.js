$("#modal-url").on('input', getAndVerifyInputs);
$("#modal-submit").click(submit);

function submit() {
    const data = getAndVerifyInputs();
    if (!data.Url) {
        return;
    }

    const btn = $(this);
    const span = $("span", btn);

    span.empty();
    span.addClass("buffering");
    btn.prop("disabled", true);
    
    const request = $.ajax({
        // should be replaced with a bit more dynamic solution
        url: "https://localhost:5001/List/Add",
        data: data,
        type: "POST",
        async: true,
        dataTypes: "json"
    });

    request.done(function(response) {
        span.removeClass();
        span.addClass("text-success");
        span.html(response.responseText);
    });

    request.fail(function(response) {
        console.log(response);
        span.removeClass();
        span.addClass("text-danger");
        span.html(response.responseText);
        btn.prop("disabled", false);
    });
}

function getAndVerifyInputs() {
    const urlPattern = /^(https?:\/\/)?m\w{9}t\.net\/\w{5}\/\d+\/.+$/;
    
    return {
        Url: getAndVerify("#modal-url", urlPattern)
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
