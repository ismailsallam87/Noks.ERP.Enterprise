var menu_config_url = '';

function Intial_Menu(userId) {
  var cont = $('#sidebar_menu');
  cont.empty();
  var ajaxoptions = {};
  ajaxoptions.data = { UserId: userId };
  ajaxoptions.url = '../../Settings/Intial_Menu';
  ajaxoptions.success = function (result) {
      cont.append(result);
      App.init();
  };
  ajaxoptions.error = function () {
  };
  $.ajax(ajaxoptions);
}