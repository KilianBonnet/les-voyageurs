export function identificationEvent(clients, ws, socketMessage) {
    const clientDevice = socketMessage.d.device;
    const client = clients.find(client => client.ws = ws);
    client.device = clientDevice;

    ws.send(JSON.stringify({ "op": 3 }))
}