import { sendError } from "../ws_event_helper.js";
import { clients } from "../state.js";

export function transformEventHandler(ws, socketMessage) {
    const client = clients.find(client => client.ws === ws);

    if(client.device == null) {
        sendError(ws, "Client is not authenticated.");
        return;
    }

    const position = socketMessage.d.position;
    const rotation = socketMessage.d.rotation;

    if(!checkParameters(ws, "position", position) || !checkParameters(ws, "rotation", rotation))
        return;

    const msg = {
        "op": 15,
        "d": {
            position,
            rotation
        }
    }

    const sendToDevice = client.device === "VR_Headset" ? "Table" : "VR_Headset";
    clients.find(c => c.device === sendToDevice).ws.send(JSON.stringify(msg))
}

function checkParameters(ws, paramName, param){
    if(param === undefined) {
        sendError(ws, `Missing ${paramName} parameter.`);
        return false;
    }

    if(typeof(param) !== "object") {
        sendError(ws, `Wrong ${paramName} format (excepted Object, got ${typeof(param)}).`);
        return false;
    }

    return checkCoordinate(ws, `${paramName}.x`, param.x) &&
        checkCoordinate(ws, `${paramName}.y`, param.z) &&
        checkCoordinate(ws, `${paramName}.z`, param.y);
}

function checkCoordinate(ws, coordinateName, param) {
    if(param === undefined) {
        sendError(ws, `Missing ${coordinateName} parameter.`);
        return false;
    }

    return typeof(param) === 'number';
}