import { ChangeEventHandler } from "react";
import { sendGreetings } from '../services/api.ts'

type Props = {
    card: String
};

export const Card = (props: Props) => {
    const {card} = props;
    const backgroundPos = getPosition(card);
    console.log(backgroundPos);
    return <button className="card" style={{'backgroundPosition': getPosition(card)}} onClick={() => sendGreetings()}>
    </button>
}

function getPosition(card: String) {
    switch(card) {
        case "T": return "-900% 0%";
        case "J": return "-1000% 0%";
        case "Q": return "-1100% 0%";
        case "K": return "-1200% 0%";
        case "A": return "-1300% 0%";
        default: 
            const numberValue = Number(card);
            const horizontalPosition = (numberValue - 1) * -100;
            return horizontalPosition + "% 0%";
    }
}