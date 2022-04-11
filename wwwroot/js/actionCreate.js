$(document).on('click', '.ms_btn_create_album', function() {
    let form = $('#CreateAlbum').serializeArray()
    let error = false;
    let data = {};

    let image = $('#CreateAlbum').find('.image').val().replace(/C:\\fakepath\\/i, '');
    for (var i = 0; i < form.length; i++) {
        if (form[i].value == '') {
            error = true;
            break;
        }
        data[form[i].name] = form[i].value;
    }
    if (image == '') {
        error = true;
    }
    if (error == true) {
        toastr.error('Xin vui lòng điền đầy đủ thông tin các trường!', 'Xin vui lòng thử lại!');
    } else {
        let cookie = readCookie('login');
        cookie = JSON.parse(cookie)
        $.ajax({
            method: "POST",
            url: "/Action/CreateAlbum",
            data: { name: data.name, url: data.url, image: image, publish: data.publish, id: cookie.id },
            dataType: "json",
            cache: false,
            success: function(data) {
                if (data.code == 'error') {
                    toastr.error(data.message_2, data.message_1);
                } else {
                    toastr.success(data.message_2, data.message_1);
                    setTimeout(() => {
                        $('#CreateAlbum').submit();
                    }, 2000);
                }
            }
        });
    }

    return false;
})

$(document).on('click', '.create_music', function() {
    let form = $('#UploadFile').serializeArray()
    let error = false;
    let data = {};
    let image = $('#UploadFile').find('.image').val().replace(/C:\\fakepath\\/i, '');
    let music = $('#UploadFile').find('.music').val().replace(/C:\\fakepath\\/i, '');
    for (var i = 0; i < form.length; i++) {
        if (form[i].value == '') {
            error = true;
            break;
        }
        data[form[i].name] = form[i].value;
    }
    if (image == '' || music == '') {
        error = true;
    }
    if (error == true) {
        toastr.error('Xin vui lòng điền đầy đủ thông tin các trường!', 'Xin vui lòng thử lại!');
    } else {
        let cookie = readCookie('login');
        cookie = JSON.parse(cookie)
        $.ajax({
            method: "POST",
            url: "/Action/CreateMusic",
            data: { name: data.name, url: data.url, image: image, music: music, publish: data.publish, id: cookie.id, artist: data.artist, album: data.album, theloai: data.theloai },
            dataType: "json",
            cache: false,
            success: function(data) {
                if (data.code == 'error') {
                    toastr.error(data.message_2, data.message_1);
                } else {
                    toastr.success(data.message_2, data.message_1);
                    setTimeout(() => {
                        $('#UploadFile').submit();
                    }, 2000);
                }
            }
        });
    }

    return false;
})

$(document).on('keyup', '.title', function() {
    let _this = $(this);
    let text = _this.val();
    _this.parents('form').find('.canonical').val(slug(text))

});


function slug(title) {
    title = cnvVi(title);
    return title;
}


function cnvVi(str) {
    str = str.toLowerCase(); // chuyen ve ki tu biet thuong
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|,|\.|\:|\;|\'|\–| |\"|\&|\#|\[|\]|\\|\/|~|$|_/g, "-");
    str = str.replace(/-+-/g, "-");
    str = str.replace(/^\-+|\-+$/g, "");
    return str;
}

function replace(Str = '') {
    if (Str == '') {
        return '';
    } else {
        Str = Str.replace(/\./gi, "");
        return Str;
    }
}

function readCookie(name) {
    var nameEQ = encodeURIComponent(name) + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ')
            c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0)
            return decodeURIComponent(c.substring(nameEQ.length, c.length));
    }
    return null;
}