// WASM doesn't support System.Net.Sockets (which is what WebSocketSharp relies on) and i don't want to go through the
// work of using System.Net.WebSockets which is much too verbose for something as simple as an echo chamber

const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
const exports = await getAssemblyExports("scoreboard2.dll");
const Shim = exports.scoreboard2.RemoteControl.WebSocketPlatform.WebSocketShimJS;

let socket = null;

export function SetupWebsocket(url) {
    if (typeof url != "string") throw new TypeError("url needs to be a string");
    if (socket instanceof WebSocket) Close();

    socket = new WebSocket(url);
    socket.addEventListener("message", (data) => {
        Shim.SocketOnMessage(data.data.toString());
    });

    socket.addEventListener("open", (_) => {
        Shim.SocketOnOpen();
    });

    socket.addEventListener("close", (_) => {
        Shim.SocketOnClose();
    });
}

export function Send(data) {
    console.log("send data");
    if (socket === null) return;
    socket.send(data);
}

export function Close() {
    console.log("close")
    if (socket === null) throw new ReferenceError("WebSocket not initialized");
    socket.close()
}