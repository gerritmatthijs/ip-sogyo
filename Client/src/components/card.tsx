// import { useState } from 'react';

// type Props = {
//     value: string
// };


// export const Card = (props: Props) => {
//     const {value} = props;
//     // const [clicked, setClicked] = useState(false);
//     // function cardClicked(){
//     //     if (clicked){
//     //         setClicked(false)
//     //     }
//     //     else{
//     //         setClicked(true)
//     //     }
//     // }

//     return <button className="card" style={{'backgroundPosition': getPicture(value)}} onClick={() => playCard(value)}>
//     </button>
// }

export const getPicture = (card: string) => {
    switch(card) {
        case "T": return "-900% 0%";
        case "J": return "-1000% 0%";
        case "Q": return "-1100% 0%";
        case "K": return "-1200% 0%";
        case "A": return "-1300% 0%";
        case "D": return "0% 0%";
        case "1": return "0% -300%";
        case "H": return "0% -200%";
        default: 
            const numberValue = Number(card);
            const horizontalPosition = (numberValue - 1) * -100;
            return horizontalPosition + "% 0%";
    }
}