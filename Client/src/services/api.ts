export async function playCard(cardPlayed: String){
    const response = await fetch("tichu/play", {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            card:cardPlayed
        })
    });
    if (response.ok){
        const result = await response.json();
        return result as string
    } else {
        return {
            statusCode: response.status,
            statusText: response.statusText
        };
    }
}

export async function createGame(playerName: String){
    const response = await fetch("tichu/newgame", {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            name:playerName
        })
    });
    if (response.ok){
        const result = await response.json();
        return result as string
    } else {
        return {
            statusCode: response.status,
            statusText: response.statusText
        };
    }
}