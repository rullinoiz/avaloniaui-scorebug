Replicator.cs is a dedicated server for hosting a remote control instance for your scoreboard so that other peers using the scoreboard2 software to share the same scorebug.

# Scoreboard2 Replicator Standards
If you would like to create your own Replicator in a service like AWS or Cloudflare Workers, here are the things to look out for:

* ### WebSockets
The Replicator is 100% based on WebSockets, powered by [websocket-sharp](https://github.com/sta/websocket-sharp), which utilizes System.Net.Sockets. This, however, is incompatible with .NET WASM which prevents you from building applications using low level networking for the web. I solved this by creating a platform specific implementation that uses JavaScript's native WebSocket implementation through .NET WASM's JavaScript interop.

* ### Unique Peers
Any service you use that uses WebSockets should be able to differentiate between each connected peer so that the server can send any message received to all other peers. This isn't a simple echo, because the sender should not receive the same message!

---

That's basically it. Any service you decide to use should use WebSockets and allow you to identify who sent data and should allow you to send to individual peers rather than broadcast it. I plan to add an authentication feature later on, but as of now, the server is only responsible for sending whatever data it receives to everyone else.