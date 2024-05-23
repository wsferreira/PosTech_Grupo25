function RequestAjax(url,type,inputData,failMessage,modal) {
    var result = null;
    $.ajax({
        url: url,
        type: type,
        async: false,
        data: inputData,
        cache: false
    }).done(function (data) {
        result = data;
    }).fail(function (jqXHR, textStatus) {
        alert(failMessage);
        $(modal).modal('toggle');
    });
    return result;
}