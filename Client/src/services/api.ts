import { TichuGameState } from "../types";

export async function playerAction(action: string){
    const response = await fetch("tichu/play", {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            action:action
        })
    });
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

export async function checkAllowed(action: string){
    const response = await fetch("tichu/check", {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            action:action
        })
    });
    if (response.ok){
        const result = await response.json();
        return result as boolean;
    } else {
        return {
            statusCode: response.status,
            statusText: response.statusText
        };
    }
}

export async function createGame(playerNames: string[]){
    const response = await fetch("tichu/newgame", {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            names:playerNames.join(",")
        })
    });
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