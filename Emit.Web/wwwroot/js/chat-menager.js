const chatConnection = new signalR.HubConnectionBuilder()
                        .withUrl('/chathub')
                        .build();

chatConnection.on("chatMessage", displayMessage);
chatConnection.start()
    .catch(err => console.log(err.toString()));
document.querySelector(".chat-form").addEventListener("submit", function (e) {
    e.preventDefault()
    sendMessage()
});
function sendMessage() {
    const chatInput = document.getElementById("chat-input");
    const message = chatInput.value;
    chatInput.value = "";
    const user = localStorage.getItem("user") || "Annonomys";
    chatConnection.invoke("SendMessage", message, user);
    displayMessage({ message, user }, true);
}
function displayMessage(response,active) {
    const chatHistory = document.getElementById("chat-history");
    let className = "chat-message ";
    if (active) {
        className += "chat-message-active";
    }
    else {
        className += "chat-message-passive";
    }
    const msg = `<br/><p class="${className}">${response.message} <sub>${response.user}</sub></p><br/>`
    chatHistory.innerHTML += msg;
};
