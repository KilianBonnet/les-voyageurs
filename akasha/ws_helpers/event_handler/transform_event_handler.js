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

    if(!checkParameters(ws, "position", position) || !checkParameters(ws, "position", position))
        return;


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

    
}