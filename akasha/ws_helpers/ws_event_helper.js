import { identificationEvent } from "./event_handler/intentification_event_handler.js";
import { invokeEvent } from "./event_handler/invoke_event_handler.js";
import { sceneChangeEvent } from "./event_handler/scene_change_event_handler.js";
import { scoreEvent } from "./event_handler/score_event_handler.js";
import { clients, removeClient } from "./state.js";

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
        const socketMessage = JSON.parse(dataString); // Parsing JSON

        switch (socketMessage.op) {
            case 2:
                identificationEvent(ws, socketMessage);
                break;
            
            case 10:
                sceneChangeEvent(ws, socketMessage);
                break;
            
            case 11:
                invokeEvent(ws, socketMessage);
                break;
            
            case 12:
                scoreEvent(ws, socketMessage);
                break;

            default:
                sendError(ws, "Unknown op.")
                break;
        }

    } catch (e) {
        sendError(ws, e.toString());
    }
}

export function sendError(ws, message) {
    const res = {
        "op": 0,
        "d": {
            "message": message
        }
    }
    ws.send(JSON.stringify(res));
}

export function onClose(ws) {
    removeClient(ws);
    console.log(`[-] ${clients.length} client${clients.length > 1 ? "s" : ""} connected.`);
}