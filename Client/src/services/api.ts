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
        console.log(result);
    } else {
        return {
            statusCode: response.status,
            statusText: response.statusText
        };
    }

}