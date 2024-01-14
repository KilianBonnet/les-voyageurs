import { bonusEvent } from "./event_handler/bonus_event_handler.js";
import { identificationEvent } from "./event_handler/intentification_event_handler.js";
import { invokeEvent } from "./event_handler/invoke_event_handler.js";
import { roomChangeEvent } from "./event_handler/room_event_handler.js";
import { sceneChangeEvent } from "./event_handler/scene_change_event_handler.js";
import { scoreEvent } from "./event_handler/score_event_handler.js";
import { transformEventHandler } from "./event_handler/transform_event_handler.js";
import { addClient, clients, removeClient } from "./state.js";

export function onConnection(ws) {
    // Pushing client on the list
    addClient(ws);
    console.log(`[+] ${clients.length} client${clients.length > 1 ? "s" : ""} connected.`);
    ws.send(JSON.stringify({ "op": 1 }))
}

const eventMapper = [
    { op: 2, handler: identificationEvent },
    { op: 10, handler: sceneChangeEvent },
    { op: 11, handler: invokeEvent },
    { op: 12, handler: scoreEvent },
    { op: 13, handler: roomChangeEvent },
    { op: 14, handler: bonusEvent },
    { op: 15, handler: transformEventHandler }
]
export function onMessage(ws, data) {

    try {
        const dataString = data.toString('utf-8'); // Convert buffer in string
        const socketMessage = JSON.parse(dataString); // Parsing JSON

        const event = eventMapper.find(event => event.op === socketMessage.op);
        if(event === undefined) {
            sendError(ws, "Unknown op.");
            return;
        }
        event.handler(ws, socketMessage);

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