$(function () {
    setProfile()
    $("#profile").on("click", function () {
        setProfile()
    });
    $("#change-password").on("click", function () {
        setChangePasword()
    });
})
function setProfile() {
    var content = $("#content");
    fetch('/user/details')
        .then(x => x.text())
        .then(function (html) {
            content.html(html);
        })
}
function setChangePasword() {
    var content = $("#content");
    fetch('/user/ChangePassword')
        .then(x => x.text())
        .then(function (html) {
            content.html(html);
        })
}
