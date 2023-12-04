export let clients = [];

let score = 0;
export function increaseScore(amount){
    score += amount;
    return score;
}