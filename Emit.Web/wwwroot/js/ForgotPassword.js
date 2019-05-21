
document.getElementById("forgotPasswordBtn").addEventListener("click", function (e) {
    e.preventDefault();
    email = document.getElementById("forgotPasswordEmail").value;
    data = {
        Email:email
    }
    options = {
        method: 'Post',
        headers: {
            'Content-Type': 'application/json',
            // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        body: JSON.stringify(data)
    }
    fetch("/api/identity",options)
        .then(x => x.json())
        .then(displayForgotPasswordResult)
    .catch(err=>err.toString())
});
function displayForgotPasswordResult(forgotPasswordResult) {
    console.log(forgotPasswordResult)
    const forgotPasswordContent = document.getElementById("forgotPasswordContent")
    let styleClass;
    forgotPasswordResult.success ? styleClass = "alert-success" : styleClass = "alert-danger";
    forgotPasswordContent.innerHTML = `<h2 class="${styleClass} alert">${forgotPasswordResult.message}</h2>`;
    setTimeout(() => {
        forgotPasswordContent.className = "";
        forgotPasswordContent.innerHTML = "";
    },2000);
}