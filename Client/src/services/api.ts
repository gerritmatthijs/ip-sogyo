import { TichuGameState, isTichuGameState } from "../types";

export async function playCard(name: string, cardPlayed: string){
    const response = await fetch("tichu/play", {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            name:name,
            card:cardPlayed
        })
    });
    if (response.ok){
        const result = await response.json();
        if (isTichuGameState(result)){
            return result as TichuGameState;
        }
        else {
            return result as string;
        }
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