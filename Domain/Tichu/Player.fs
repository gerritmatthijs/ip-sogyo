namespace Tichu

type Player = 
    {name: string; hand: Card list}

module Player = 
    let PlayCards(set: Card list)(player: Player): Player = 
        let newHand = player.hand |> Hand.RemoveCards(set)
        {player with hand = newHand}

    let isFinished(player: Player): bool = player.hand.IsEmpty

