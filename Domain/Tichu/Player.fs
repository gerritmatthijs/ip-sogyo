namespace Tichu

type Player = 
    {name: string; hand: Card list}

module Player = 
    let PlayCards(card: Card)(player: Player): Player = 
        let newHand = player.hand |> Hand.RemoveCards([card])
        {player with hand = newHand}

    let isFinished(player: Player): bool = player.hand.IsEmpty

