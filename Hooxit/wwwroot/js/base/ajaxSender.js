var ajaxRequest = {
    sendAjax: function (url, data, method, type, headers, success, error) {
        $.ajax({
            url: url,
            data: JSON.stringify(data),
            type: method,
            headers: headers,
            contentType: type,
            success: success,
            error: error
        });
    },
    getData: function (url, successCallback) {
        $.get(url, successCallback);
    }
}