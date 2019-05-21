var connection = new signalR.HubConnectionBuilder()
                                .withUrl("/notification")
                                .build();

connection.on("Login", function (test) {
    console.log(test)
    document.getElementById("test").innerHTML = test;
});
connection.start()
    .catch(err => console.log(err.toString()));

