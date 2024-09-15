const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
let exports = await getAssemblyExports("scoreboard2.dll");
let ReplicatorService = exports.scoreboard2.RemoteControl.ReplicatorService;

// console.log(exports.scoreboard2.RemoteControl.ReplicatorService.GetStringFromDotnet());

let socket = null;

export function SetupWebsocket(url) {
    if (typeof url != "string") throw new TypeError("url needs to be a string");
    if (socket instanceof WebSocket) Close();

    socket = new WebSocket(url);
    socket.addEventListener("message", (data) => {
        ReplicatorService.SocketOnMessage(data.data.toString());
    });

    socket.addEventListener("open", (_) => {
        ReplicatorService.SocketOnOpen();
    });

    socket.addEventListener("close", (_) => {
        ReplicatorService.SocketOnClose();
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