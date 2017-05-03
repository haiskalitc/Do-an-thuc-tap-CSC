function formatBytes(bytes, decimals) {
    if (bytes == 0) return '0 Byte';
    var k = 1024;
    var dm = decimals + 1 || 3;
    var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    var i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
}
function kiemTraGiaTri(a, b) {
    if (parseInt(a) > parseInt(b)) {
        return '<i class="fa fa-arrow-up c-green">';
    } else if (parseInt(a) < parseInt(b)) {
        return '<i class="fa fa-arrow-down c-red">';
    }
    return '';
}
function chuyenDoiGio(giay) {
    s = giay % 60;
    m = parseInt(giay / 60) % 60;
    h = parseInt(parseInt(giay / 60) / 60);
    if (h >= 24) {
        n = parseInt(h / 24);
        return n + ' ngày';
    } else {
        return h + ' giờ ' + m + ' phút ' + s + ' giây';
    }
    
}

function chuyen_doi_dung_luong(dung_luong) {
    dung_luong = dung_luong * 1;
    if ((dung_luong / (1024 * 1024 * 1024)) > 1) {
        return Math.round((dung_luong / (1024 * 1024 * 1024)) * 100) / 100 + ' GB';
    }
    else if ((dung_luong / (1024 * 1024)) > 1) {
        return Math.round((dung_luong / (1024 * 1024)) * 100) / 100 + ' MB';
    }
    else if ((dung_luong / 1024) > 1) {
        return Math.round(dung_luong / 1024 * 100) / 100 + ' KB';
    } else {
        return dung_luong + ' Byte';
    }
}

function format_date(date, chi_ngay) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    if (chi_ngay) {
        return date.getDate() + "/" + (date.getMonth() * 1 + 1) + "/" + date.getFullYear();
    } else {
        return date.getDate() + "/" + (date.getMonth() * 1 + 1) + "/" + date.getFullYear() + "  " + strTime;
    }
    
}
function timeConverter(UNIX_timestamp){
  var a = new Date(UNIX_timestamp * 1000);
  var months = ['01','02','03','04','05','06','07','08','09','10','11','12'];
  var year = a.getFullYear();
  var month = months[a.getMonth()];
  var date = a.getDate();
  var hour = a.getHours();
  var min = a.getMinutes();
  var sec = a.getSeconds();
  var time = date + '/' + month + '/' + year +' '+ hour + ':' + min + ':' + sec ;
  return time;
}

function doi_thoi_gian_sang_dinh_dang_ngay_thang(UNIX_timestamp) {
    var a = new Date(UNIX_timestamp * 1000);
    var hour = a.getHours();
    var min = a.getMinutes();
    var sec = a.getSeconds();
    var time = hour + ':' + min + ':' + sec;
    return time;
}

function lay_danh_sach_cac_ngay_trong_tuan() {
    var curr = new Date;
    var first = curr.getDate() - curr.getDay();
    var mang_ngay = [];
    var firstday = (new Date(curr.setDate(first+2))).toString();
    for(var i = 1;i<=7;i++){
       var next = new Date(curr.getTime());
       next.setDate(first+i);
       mang_ngay.push(format_date(next, true));
    }
    //console.log(mang_ngay);
    return mang_ngay;
}

function lay_danh_sach_cac_ngay_trong_thang() {
    var curr = new Date;
    var numOfDays = new Date(curr.getFullYear(), curr.getMonth(), 0).getDate();
    var days = new Array();

    for (var i = 0; i <= numOfDays; i++) {
        days[i] = format_date(new Date(curr.getFullYear(), curr.getMonth(), i + 1), true);
    }
    return days;
}