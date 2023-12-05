import { sendError } from "../ws_event_helper.js";
import { clients } from "../state.js";

export function sceneChangeEvent(ws, socketMessage) {
    const client = clients.find(client => client.ws = ws);

    if(client.device == null) {
        sendError(ws, "Client is not authenticated.");
        return;
    }
    const scene = socketMessage.d.scene;

    if(scene == null) {
        sendError(ws, "Missing scene parameter.");
        return;
    }

    if(typeof(scene) != "string") {
        sendError(ws, "scene parameter should be a string.");
        return;
    }

    const msg = {
        "op": 10,
        "d": {
            "scene": scene
        }
    }
    const sendToDevice = client.device === "VR_Headset" ? "Table" : "VR_Headset";
    clients.find(c => c.device === sendToDevice).ws.send(JSON.stringify(msg));
}