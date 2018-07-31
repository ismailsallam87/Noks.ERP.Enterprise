var loading_img = '<p style="text-align:center;"> <img src="../../assets/img/loading.gif"/> </p>';
var error_ajax_loading = '<p style="text-align:center;color:red">An Error occurred while trying to load the required information.</p>';
function Task_Select(url, data) {
    var cont = $('#Task_Tbl_body');
    cont.empty();
    cont.append(loading_img);
    var ajaxoptions = {};
    ajaxoptions.data = data;
    ajaxoptions.url = url;
    ajaxoptions.success = function (result) {
        cont.empty();
        cont.append(result);
    };
    ajaxoptions.error = function () {
        cont.append(error_ajax_loading);
    };
    $.ajax(ajaxoptions);
}
function Runner_Select(url, data) {
    var cont = $('#Runners_Tbl_body');
    cont.empty();
    cont.append(loading_img);
    var ajaxoptions = {};
    ajaxoptions.data = data;
    ajaxoptions.url = url;
    ajaxoptions.success = function (result) {
        cont.empty();
        cont.append(result);
    };
    ajaxoptions.error = function () {
        cont.append(error_ajax_loading);
    };
    $.ajax(ajaxoptions);
}