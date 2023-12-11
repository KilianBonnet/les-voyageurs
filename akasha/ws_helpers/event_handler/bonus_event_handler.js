import { sendError } from "../ws_event_helper.js";
import { clients } from "../state.js";

export function bonusEvent(ws, socketMessage) {
    const client = clients.find(client => client.ws === ws);

    if(client.device == null) {
        sendError(ws, "Client is not authenticated.");
        return;
    }
    const bonus = socketMessage.d.bonus;

    if(bonus == null) {
        sendError(ws, "Missing bonus parameter.");
        return;
    }

    if(typeof(bonus) != "number") {
        sendError(ws, "bonus parameter should be a string.");
        return;
    }

    const msg = {
        "op": 14,
        "d": {
            "bonus": bonus
        }
    }

    const sendToDevice = client.device === "VR_Headset" ? "Table" : "VR_Headset";
    clients.find(c => c.device === sendToDevice).ws.send(JSON.stringify(msg));
}