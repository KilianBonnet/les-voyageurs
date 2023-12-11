export let clients = [];
export function removeClient(ws) {
    clients = clients.filter(client => client.ws !== ws);
}
export function addClient(ws) {
    clients.push({
        "ws": ws,
        "device": undefined
    })
}

let score = 0;
export function increaseScore(amount){
    score += amount;
    if(score < 0) score = 0;

    return score;
}