function checkSelect() {
    var check = false;
    var cf = true;
    var rs = true;
    $(".cbSelect input[type=checkbox]").each(function () {
        if ($(this).attr("checked") == "checked") {
            check = true;
        }
    });
    if (check == true) {
        if (confirm("Nếu xoá, dữ liệu sẽ không thể phục hồi\nBạn có thực sự muốn xoá không?") == true) {
            $("#alr").remove();
        } else {
            cf = false;
        }
        if (cf == false) {
            $("#alr").remove();
            rs = cf;
        } else {
            if (check == false && $("#alr").length <= 1) {
                $("#alr").remove();
                $("#infor").append("<div id='alr' class='alert alert-danger'>Bạn phải chọn 1 hoặc nhiều mục cần xoá</div>");
                rs = false;
            }
        }
    } else {
        $("#alr").remove();
        $("#infor").append("<div id='alr' class='alert alert-danger'>Bạn phải chọn 1 hoặc nhiều mục cần xoá</div>");
        rs = false;
    }
    
    return rs;
}


$(function () {

    
    // $(document).ready(function(){
    if ($('.getURL').length > 0) {
        $('.getURL').each(function () {
            var base = $(this);
            base.blur(function () {
                getStrip(this, '#' + base.attr('rel'));
            });
        });
    }

    if ($('.ckfinder').length > 0) {
        $('.ckfinder').each(function () {
            var base = $(this);
            var textbox = base.children('input[type="text"]');
            var textboxID = textbox.attr('id');

            var s = '';
            s += '<input type="button" class="btn btn-default" value="Chọn file" />';
            s += '<div class="input-group-addon">';
            s += '<img id="img_' + textboxID + '" for="' + textboxID + '" src="">';
            s += '<img id="imgorg_' + textboxID + '" class="org" for="' + textboxID + '" src="">';
            s += '</div>';
            base.append(s);

            var btn = base.children('input[type="button"]');
            btn.attr('rel', textboxID);

            btn.click(function () {
                selectFile(textboxID);
            });
            textbox.blur(function () {
                previewFile(textboxID, textbox.val());
            });
            previewFile(textboxID, textbox.val());
        });
    }
    

    if ($('.datePicker').length > 0) {
        var s = '<span class="input-group-addon"><i class="icon-calendar"></i></span>';
        $('.datePicker').each(function () {
            if ($(this)[0].tagName == 'INPUT') {
                $(this).after(s);
                $(this).parent().addClass('input-group');
                $(this).parent().addClass('date');
            }
            else if ($(this)[0].tagName == 'DIV') {
                $(this).append(s);
                $(this).addClass('input-group');
                $(this).addClass('date');
            }
            $(this).datepicker();
        });
    }
    
    $(".input-group-addon img:first-child").mouseover(function () {
        $(this).next().addClass("imgpreview");
    });
    $(".input-group-addon img:first-child").mouseout(function () {
        $(this).next().removeClass("imgpreview");
    });
});

function previewFile(id, fileUrl) {
    $('#img_' + id).attr('src', fileUrl);
    $('#imgorg_' + id).attr('src', fileUrl);
}

function selectFile(textboxID) {
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl) {
        $('#' + textboxID).val(fileUrl);
        previewFile(textboxID, $('#' + textboxID).val());
    };
    finder.popup();
}

function loadResource(filename, filetype) {
    if (filetype == "js") { //if filename is a external JavaScript file
        var fileref = document.createElement('script');
        fileref.setAttribute("type", "text/javascript");
        fileref.setAttribute("src", filename);
    }
    else if (filetype == "css") { //if filename is an external CSS file
        var fileref = document.createElement("link");
        fileref.setAttribute("rel", "stylesheet");
        fileref.setAttribute("type", "text/css");
        fileref.setAttribute("href", filename);
    }
    if (typeof fileref != "undefined")
        document.getElementsByTagName("head")[0].appendChild(fileref)
}

