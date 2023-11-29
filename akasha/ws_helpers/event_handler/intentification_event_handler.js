import { sendError } from "../ws_event_helper.js";

export function identificationEvent(clients, ws, socketMessage) {
    const clientDevice = socketMessage.d.device;
    if(clientDevice == null) {
        sendError(ws, "Missing device parameter.");
        return;
    }

    if(clientDevice != "Table" && clientDevice != "VR_Headset") {
        sendError("Unknown device.");
        return;
    }

    const client = clients.find(client => client.ws === ws);
    client.device = clientDevice;

    console.log(clientDevice + " is identified.");
    ws.send(JSON.stringify({ "op": 3 }))
}