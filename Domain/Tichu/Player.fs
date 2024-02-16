namespace Tichu

type Player = 
    {name: string; hand: Card list}

module Player = 
    let PlayCards(set: Card list)(player: Player): Player = 
        let newHand = player.hand |> CardList.RemoveCards(set)
        {player with hand = newHand}

    let isFinished(player: Player): bool = player.hand.IsEmpty

