// Reusable AJAX wrapper with auto refresh token
function callApi(options) {
    var token = localStorage.getItem("AccessToken");

    if (!options.headers) options.headers = {};
    options.headers["Authorization"] = "Bearer " + token;

    // Save original callbacks
    var originalSuccess = options.success;
    var originalError = options.error;

    // Override success/error to handle 401
    $.ajax({
        ...options,
        success: function (data, textStatus, xhr) {
            if (originalSuccess) originalSuccess(data, textStatus, xhr);
        },
        error: function (xhr, textStatus, errorThrown) {
            if (xhr.status === 401 && !options._retry) {
                // 401 → refresh token silently
                options._retry = true; // prevent infinite loop
                console.log("401 detected → refreshing token...");

                refreshToken(function () {
                    callApi(options); // retry original request
                });
            } else {
                // Only show other errors
                if (originalError) originalError(xhr, textStatus, errorThrown);
            }
        }
    });
}

// Refresh token function
function refreshToken(callback) {
    var refreshToken = localStorage.getItem("RefreshToken");

    $.ajax({
        type: "POST",
        url: baseUrl + "/api/auth/refresh-token",
        contentType: "application/json",
        data: JSON.stringify({
            refreshToken: refreshToken
        }),
        beforeSend: function () {
            $(".loading").show();
        },
        complete: function () {
            $(".loading").hide();
        },
        success: function (data) {
            console.log("Token refreshed");

            // Save new tokens
            localStorage.setItem("AccessToken", data.AccessToken);
            localStorage.setItem("RefreshToken", data.RefreshToken);

            if (callback) callback();
        },
        error: function () {
            console.log("Refresh token expired → redirect login");
            localStorage.clear();
            window.location.href = baseUrl+"/dms/login";
        }
    });
}