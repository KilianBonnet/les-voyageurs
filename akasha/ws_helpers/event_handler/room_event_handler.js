import { sendError } from "../ws_event_helper.js";
import { clients } from "../state.js";

export function roomChangeEvent(ws, socketMessage) {
    const client = clients.find(client => client.ws === ws);

    if(client.device == null) {
        sendError(ws, "Client is not authenticated.");
        return;
    }
    const room = socketMessage.d.room;

    if(room == null) {
        sendError(ws, "Missing room parameter.");
        return;
    }

    if(typeof(room) != "number") {
        sendError(ws, "room parameter should be a string.");
        return;
    }

    const msg = {
        "op": 13,
        "d": {
            "room": room
        }
    }

    const sendToDevice = client.device === "VR_Headset" ? "Table" : "VR_Headset";
    clients.find(c => c.device === sendToDevice).ws.send(JSON.stringify(msg));
}