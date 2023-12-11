import { WebSocketServer } from 'ws';
import { onClose, onMessage, onConnection } from './ws_helpers/ws_event_helper.js';
import os from 'os';

const wss = new WebSocketServer({ port: 8080, host: '0.0.0.0' });

wss.on('listening', () => {
    const address = wss.address();
    const localIP = getLocalIpAddress();
    console.log(`Server is listening at ws://${localIP}:${address.port}`);
});

wss.on('connection', (ws) => {
    onConnection(ws);
    ws.on('message', (data) => onMessage(ws, data));
    ws.on('close', () => onClose(ws));
});

function getLocalIpAddress() {
    const ifaces = os.networkInterfaces();
    for (const iface in ifaces) {
        for (const alias of ifaces[iface]) {
            if (alias.family === 'IPv4' && !alias.internal) {
                return alias.address;
            }
        }
    }
    return '127.0.0.1';
}