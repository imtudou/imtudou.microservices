function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);

var config = {
    authority: "https://localhost:5000",
    client_id: "JavaScript.Client",
    client_secret:"secret",
    redirect_uri: "https://localhost:5002/callback.html",
    response_type: "code",
    scope: "api",
    post_logout_redirect_uri: "https://localhost:5002/index.html",
};
var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    debugger
    if (user) {
        log("User logged in", user);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    debugger
    mgr.signinRedirect();
}

function api() {
    debugger
    mgr.getUser().then(function (user) {
        var url = "https://localhost:5001/identity";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    debugger
    mgr.signoutRedirect();
}