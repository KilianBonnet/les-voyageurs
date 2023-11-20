import WebSocket, { WebSocketServer } from 'ws';
import { onClose, onMessage, onConnection } from './ws_helpers/ws_event_helper.js';

const wss = new WebSocketServer({ port: 8080 });


wss.on('listening', () => console.log('Server listening on port 8080'));

wss.on('connection', (ws) => {
    onConnection(ws);
    ws.on('message', (data) => onMessage(ws, data));
    ws.on('close', () => onClose(ws));
});
