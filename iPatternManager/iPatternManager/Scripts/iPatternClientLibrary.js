$(function()
{
   $("#errorMessagePopup").dialog({ autoOpen: false, width: "800", height: "500"
   });
   var words = [
			"ActionScript",
			"AppleScript",
			"Asp"];

   $("#tbGlobalSearch").autocomplete({
      source: words,
      select: function(event, ui) { Search(); }
   });

   $("#tbGlobalSearch").keypress(function(e) { if (e.keyCode == 13) Search(); });
});

function Search()
{
   $.ajax({
      type: "POST",
      url: "/Search/Search/",
      data: "phrase=" + $("#tbGlobalSearch").val(),
      beforeSend: function(jqXHR, settings) { $("#searchResultArea").html('<div style="font-size: large; padding: 20px;">Loading....</div>'); },
      error: function(xhr, status, error)
      {
         $("#errorMessagePopup").html(xhr.responseText);
         $("#errorMessagePopup").dialog("open");
      },
      success: function(response)
      {
         $("#searchResultArea").html(response);
      }
   });
}

function ShowError(html)
{
   $("#errorMessagePopup").html(html);
   $("#errorMessagePopup").dialog("open");
}


function CollapseExpand(id) {
    $.ajax({
        type: "POST",
        url: "/Message/CollapseExpand/" + id,
        data: null,
        error: function(xhr, status, error) {
            alert(error);
        },
        success: function(response) {
            $("#informationTypeListDiv").html(response);
        }
    });

    return false; // if it's a link to prevent post
}

function MoveMessage(btnClicked) {
    var form = $("#inputListForm");
    $.ajax({
        type: "POST",
        url: "/Input/Move",
        data: form.serialize(),
        error: function(xhr, status, error) {
            alert(error);
        },
        success: function(response) {
            $("#mybody").html(response);
        }
    });

    return false; // if it's a link to prevent post
}

function ChangeArea(areaDropDown) {
    window.location.href = '/Area/ChangeArea/' + areaDropDown.value;
}