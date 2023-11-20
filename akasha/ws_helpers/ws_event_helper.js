const clients = [];

export function onConnection(ws) {
    clients.push(ws); // Pushing client on the list
    console.log(`[+] ${clients.length} clients are connected.`);
}

export function onMessage(ws, data) {
    // Convert buffer in string
    const jsonString = data.toString('utf-8');

    // Parsing JSON
    const jsonObject = JSON.parse(jsonString);
    console.log('Parsed JSON object:', jsonObject);

    // Sending back json
    ws.send(jsonString);
}

export function onClose(ws) {
    // Removing client from the list
    const index = clients.indexOf(ws);
    if (index !== -1) clients.splice(index, 1);

    console.log(`[-] ${clients.length} clients are connected.`);
}