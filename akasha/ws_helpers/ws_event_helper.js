let clients = [];

export function onConnection(ws) {
    // Pushing client on the list
    clients.push({
        "ws": ws,
        "device": undefined
    })

    console.log(`[+] ${clients.length} client${clients.length > 1 ? "s" : ""} connected.`);
    ws.send(JSON.stringify({ "op": 1 }))
}

export function onMessage(ws, data) {

    try {
        const dataString = data.toString('utf-8'); // Convert buffer in string
        const dataObject = JSON.parse(dataString); // Parsing JSON
        const clientDevice = dataObject.d.device;

        if(dataObject.op === 2 && (clientDevice === "table" || clientDevice === "VR_Headset")) {
            const client = clients.find(client => client.ws = ws);
            client.device = clientDevice;
            ws.send(JSON.stringify({ "op": 3 }))
        }

    } catch (e) {
        const res = {
            "op": 0,
            "d": {
                "message": e.toString()
            }
        }
        ws.send(JSON.stringify(res));
    }
}

export function onClose(ws) {
    clients = clients.filter(client => client.ws !== ws); // Removing client from the list
    console.log(`[-] ${clients.length} client${clients.length > 1 ? "s" : ""} connected.`);
}