function BindDropDownListsByDictionarycls(MethodURL, json, DropDownID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8?",
        data: '{}',
        url: MethodURL,
        dataType: "json",
        success: function (data) {
            var select = $(DropDownID);
            select.children().remove();
            select.append($("<option>").val(0).text('– Todos –'));
            if (data.d) {
                $(data.d).each(function (index, item) {
                    select.append($("<option>").val(item.DataValueField).text(item.DataTextField));
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
            alert(errorThrown);
        }
    });
};