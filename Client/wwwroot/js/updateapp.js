const bc = new BroadcastChannel('update-channel');
bc.onmessage = function (message) {
    if (message && message.data == "new-version-found") {
        bc.postMessage("skip-waiting");
    }
    if (message && message.data == "reload") {
        notifyNewVersion();
    }
}
let dotnetObj;
window.Updater = {
    Initialize: function (interop) {
        dotnetObj = interop;
    },
    Dispose: function () {

        if (dotnetObj != null) {
            dotnetObj.dispose();
        }
    }
}
function notifyNewVersion() {
    localStorage.setItem("new-version", true);
    window.location.reload();
    dotnetObj.invokeMethodAsync("Updater.NotifyUpdateAvailable", true);
}
function restartApp() {
    window.location.reload();
    localStorage.setItem("new-version", false);
}