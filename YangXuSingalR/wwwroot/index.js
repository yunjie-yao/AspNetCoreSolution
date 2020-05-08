let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/counthub")
        .build();

    connection.on("ReceiveUpdate",
        (update) => {
            const resultDiv = document.getElementById("result");
            resultDiv.innerHTML = update;
        });

    connection.on("someFunc",
        (obj) => {
            const resultDiv = document.getElementById("result");
            resultDiv.innerHTML = "Someone called,parameters:"+obj.random;
        });

    connection.on("finished",
        function() {
            connection.stop();
            const resultDiv = document.getElementById("result");
            resultDiv.innerHTML = "Finished";
        });

    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    fetch("api/count",
            {
                method: "POST",
                headers: {
                    'content-type': 'application/json'
                }
            })
        .then(response => response.text())
        .then(id => connection.invoke("GetLatestCount", id));
})