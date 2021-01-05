
$(document).ready(function () {

    $('.PlaceDetails .direction span').click(function () {
        ClosePlaceDetails();
    });

    LoadSearchPlace();

});

function LoadSearchPlace() {
    $.ajax({
        url: '/Compo/SearchPlace/Search',
        type: 'get'
    }).done(function (res) {
        $('.SideBar').html(res);
    });
}

function Login() {
    $.ajax({
        url: '/Compo/Login',
        type: 'get',
        data: {}
    }).done(function (result) {
        $('body').append(result);
        $('#LoginModal').modal();
    });
}

function Register() {
    $.ajax({
        url: '/Compo/Register',
        type: 'get',
        data: {}
    }).done(function (result) {
        $('body').append(result);
        $('#RegisterModal').modal();
    });
}

function CloseModals(selector, aferClose) {
    $(selector).modal('hide');
    setTimeout(function () {
        aferClose();
    }, 400);
}

function SignOut() {
    $.ajax({
        url: '/Compo/SignOut',
        type: 'get',
        data: {}
    }).done(function (result) {
        location.reload();
    });
}

function SendForm(idOfForm, _url) {
    var form = $('#' + idOfForm)[0];
    var _formdata = new FormData(form);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: _url,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        error: function (xhr, status, error) {
            var errorMessage = xhr.status + ': ' + xhr.statusText
            alert(errorMessage);
        },
        data: _formdata,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 600000,
    });
}

function OpenSideBar() {
    $('.SideBar').show();
    $('.SideBar').animate({ width: 380 }, 500);
    $('.SideBarBtn').animate({ left: 380 }, 500);
    $('.SideBarBtn').css('transform', 'rotate(360deg)');
}

function CloseSideBar() {
    $('.SideBar').animate({ width: 0 }, 500);
    $('.SideBarBtn').animate({ left: 0 }, 500);
    $('.SideBarBtn').css('transform', 'rotate(180deg)');
}

$('.SideBarBtn').click(function () {
    if ($('.SideBar').width() == 380) {
        CloseSideBar();
    } else {
        OpenSideBar();
    }
});

function OpenPlaceDetails() {

}

function ClosePlaceDetails() {
    $('.PlaceDetails').animate({ bottom: -200 }, 500);
    map.removeLayer(ClickedMarker);
    ClickedMarker = undefined;
}