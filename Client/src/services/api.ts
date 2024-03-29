import { GameStatus, TichuGameState } from "../types";

async function parseResponseGameState(response: Response){
    if (response.ok){
        const result = await response.json();
        return result as TichuGameState;
    } else {
        return {
            statusCode: response.status,
            statusText: response.statusText
        };
    }
}

async function parseResponseGameStatus(response: Response){
    if (response.ok){
        const result = await response.json();
        return result as GameStatus;
    } else {
        return {
            statusCode: response.status,
            statusText: response.statusText
        };
    }
}

async function sendServerRequest(address: string, body: BodyInit | null | undefined){
    return await fetch(address, {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: body
    });
}

export async function playerAction(action: string, gameID: string){
    const response = await sendServerRequest(
        "tichu/play", 
        JSON.stringify({ action:action, gameID:gameID })
        );
    return parseResponseGameState(response);
}

export async function parseCardSelection(action: string, gameID: string){
    const response = await sendServerRequest(
        "tichu/check", 
        JSON.stringify({ action:action, gameID:gameID })
        );
    return parseResponseGameStatus(response);
}

export async function getGame(gameID: string){
    const response = await sendServerRequest(
        "tichu/getgame", 
        JSON.stringify({ gameID:gameID })
        );
    return parseResponseGameState(response);
}

export async function createGame(playerNames: string[]){
    const response = await sendServerRequest(
        "tichu/newgame", 
        JSON.stringify({ names:playerNames.join(",") })
        );
    return parseResponseGameState(response);
}