import { sendError } from "../ws_event_helper.js";
import { clients } from "../state.js";

export function env_selectPuzzleComplete(ws) {
    const client = clients.find(client => client.ws === ws);

    if(client.device == null) {
        sendError(ws, "Client is not authenticated.");
        return;
    }
    const msg = {
        "op": 16,
    }

    const sendToDevice = "VR_Headset";
    clients.filter(c => c.device === sendToDevice).forEach(c => c.ws.send(JSON.stringify(msg)));
}

