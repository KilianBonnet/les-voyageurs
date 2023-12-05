import { sendError } from "../ws_event_helper.js";
import { clients, increaseScore } from "../state.js";

export function scoreEvent(ws, socketMessage) {
    const client = clients.find(client => client.ws === ws);

    if(client.device == null) {
        sendError(ws, "Client is not authenticated.");
        return;
    }
    const scoreType = socketMessage.d.type;
    const scoreIncrement = socketMessage.d.score;

    if(scoreType == null) {
        sendError(ws, "Missing type parameter.");
        return;
    }

    if(typeof(scoreType) != "string" && scoreType === "increase") {
        sendError(ws, "type parameter should be set as 'increase'.")
        return;
    }

    if(typeof(scoreIncrement) != "number") {
        sendError(ws, "score parameter should be a string.")
        return;
    }

    const score = increaseScore(scoreIncrement);
    const msg = {
        "op": 12,
        "d": {
            "type": "info",
            "score": score
        }
    }

    console.log(`Score is now ${score}`);
    clients.forEach(c => c.ws.send(JSON.stringify(msg)));
}