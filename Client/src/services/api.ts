export async function sendGreetings(){
    const response = await fetch("tichu/api/play");
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