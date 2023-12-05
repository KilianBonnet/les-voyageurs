import { sendError } from "../ws_event_helper.js";
import { clients } from "../state.js";

export function invokeEvent(ws, socketMessage) {
    const client = clients.find(client => client.ws === ws);

    if(client.device == null) {
        sendError(ws, "Client is not authenticated.");
        return;
    }
    const networkObjectId = socketMessage.d.networkObjectId;

    if(networkObjectId == null) {
        sendError(ws, "Missing networkObjectId parameter.");
        return;
    }

    if(typeof(networkObjectId) != "number") {
        sendError(ws, "networkObjectId parameter should be a string.")
    }

    const msg = {
        "op": 11,
        "d": {
            "networkObjectId": networkObjectId
        }
    }

    const sendToDevice = client.device === "VR_Headset" ? "Table" : "VR_Headset";
    clients.find(c => c.device === sendToDevice).ws.send(JSON.stringify(msg));
}