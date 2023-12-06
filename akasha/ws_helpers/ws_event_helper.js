import { bonusEvent } from "./event_handler/bonus_event_handler.js";
import { identificationEvent } from "./event_handler/intentification_event_handler.js";
import { invokeEvent } from "./event_handler/invoke_event_handler.js";
import { roomChangeEvent } from "./event_handler/room_event_handler.js";
import { sceneChangeEvent } from "./event_handler/scene_change_event_handler.js";
import { scoreEvent } from "./event_handler/score_event_handler.js";
import { addClient, clients, removeClient } from "./state.js";

export function onConnection(ws) {
    // Pushing client on the list
    addClient(ws);
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
            
            case 13:
                roomChangeEvent(ws, socketMessage);
                break;

            case 14:
                bonusEvent(ws, socketMessage);
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