var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build();

connectionUserCount.on("UpdateTotalViews", (value) => {

    var newCountSpan = document.getElementById("totalViewsCounter");
    console.log({ newCountSpan }, {value});
    newCountSpan.innerText = value;
});

connectionUserCount.on("UpdateTotalUsers", (value) => {
    var newUserSpan = document.getElementById("totalUsersCounter");
    newUserSpan.innerText = value;
});


function newWindowLoadedOnCleint() {
    connectionUserCount.send("NewWindowLoaded");
}

function fulFilled() {
    console.log("Connection To User Hub Successfully.");
    newWindowLoadedOnCleint();
}

function rejected() {
    console.log("Connection Failed");
}

connectionUserCount.start().then(fulFilled, rejected);