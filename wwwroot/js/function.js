// ================================================= Chuc nang nghe nhac ==============================================================



// ================================================= Kiem tra dang nhap ================================================================

$(document).ready(function() {
    let cookie = readCookie('login')
    if (cookie != undefined) {
        $('.va-check-login-hidden').remove()
        cookie = JSON.parse(cookie)
        $('.name-user').text(cookie.fullname)
        $('.ms_pro_name').text(cookie.fullname.slice(0, 2))
    } else {
        $('.va-check-login-open').remove()
    }
})

$(document).on('click', '.btn-submit-register', function() {
    let fullname = $('.va-fullname-register').val()
    let email = $('.va-email-register').val()
    let password = $('.va-password-register').val()
    let confirm = $('.va-confirm-password-register').val()
    if (fullname.length == 0) {
        toastr.error('Họ và tên không được để trống!', 'Xin vui lòng thử lại!');
    } else if (IsEmail(email) == false) {
        toastr.error('Định dạng Email không hợp lệ!', 'Xin vui lòng thử lại!');
    } else if (password.length < 6) {
        toastr.error('Mật khẩu tối thiểu 6 kí tự!', 'Xin vui lòng thử lại!');
    } else if (password != confirm) {
        toastr.error('Mật khẩu nhập lại không đúng!', 'Xin vui lòng thử lại!');
    } else {
        $.ajax({
            method: "POST",
            url: "/Auth/Register",
            data: { fullname: fullname, email: email, password: password },
            dataType: "json",
            cache: false,
            success: function(data) {
                if (data.code == 'error') {
                    toastr.error(data.message_2, data.message_1);
                } else {
                    toastr.success(data.message_2, data.message_1);
                    $('#form-register')[0].reset();
                }
            }
        });
    }

    return false;
})

$(document).on('click', '.btn-change-password', function() {
    let fullname = $('.fullname-user').val()
    let email = $('.email-user').val()
    let id = $('.id-user').val()
    let old = $('.old-password').val()
    let password = $('.new-password').val()
    let confirm = $('.confirm-password').val()
    if (id.length == 0) {
        toastr.error('Có lỗi xảy ra!', 'Xin vui lòng load lại trang và thử lại!');
    } else if (fullname.length == 0) {
        toastr.error('Họ và tên không được để trống!', 'Xin vui lòng thử lại!');
    } else if (IsEmail(email) == false) {
        toastr.error('Định dạng Email không hợp lệ!', 'Xin vui lòng thử lại!');
    } else if (old.length < 6) {
        toastr.error('Bạn phải nhập mật khẩu cũ để tiến hành chỉnh sửa!', 'Xin vui lòng thử lại!');
    } else {
        $.ajax({
            method: "POST",
            url: "/Auth/Update",
            data: { id: id, email: email, fullname: fullname, old: old },
            dataType: "json",
            cache: false,
            success: function(data) {
                if (data.code == 'error') {
                    toastr.error(data.message_2, data.message_1);
                } else {
                    toastr.success(data.message_2, data.message_1);
                }
            }
        });
    }

    if (password == confirm && password.length >= 6) {
        $.ajax({
            method: "POST",
            url: "/Auth/ChangePassword",
            data: { id: id, old: old, email: email, password: password },
            dataType: "json",
            cache: false,
            success: function(data) {
                if (data.code == 'error') {
                    toastr.error(data.message_2, data.message_1);
                } else {
                    toastr.success(data.message_2, data.message_1);
                }
            }
        });
    }

    return false;
})

$(document).on('click', '.btn-logout-by-va', function() {
    eraseCookie('login')
    window.location.reload()
})

$(document).on('click', '.btn-login-by-va', function() {
    let email = $('.va-email-login').val()
    let password = $('.va-password-login').val()
    let check = $('input[name="remember"]:checked').val();
    if (IsEmail(email) == false) {
        toastr.error('Định dạng Email không hợp lệ!', 'Xin vui lòng thử lại!');
    } else if (password.length < 6) {
        toastr.error('Mật khẩu tối thiểu 6 kí tự!', 'Xin vui lòng thử lại!');
    } else {
        $.ajax({
            method: "POST",
            url: "/Auth/Login",
            data: { email: email, password: password },
            dataType: "json",
            cache: false,
            success: function(data) {
                if (data.code == 'error') {
                    toastr.error(data.message_2, data.message_1);
                } else {
                    toastr.success(data.message_2, data.message_1);
                    setTimeout(() => {
                        let time = 1;
                        if (check == 'on') {
                            time = 30;
                        }
                        createCookie('login', JSON.stringify(data.data), time);
                        window.location.reload();
                    }, 2000);
                }
            }
        });
    }

    return false;
})


function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}


function createCookie(name, value, days) {
    var expires;

    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = encodeURIComponent(name) + "=" + encodeURIComponent(value) + expires + "; path=/";
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

function eraseCookie(name) {
    createCookie(name, "", -1);
}