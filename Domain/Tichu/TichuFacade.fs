namespace Tichu

type TichuFacade(tichu: TichuGame) = 

    interface ITichu with
        member x.GetPlayerName(playerNumber: int): string = 
            tichu.players[playerNumber].name

        member x.GetPlayerHand(name: string): string = 
            Hand.CardListToString(GetPlayer(name).hand)

        member x.GetLastPlayed(): string = 
            match lastPlay with 
            | None -> ""
            | Some(card, _) -> card.value.ToString()

        member x.GetCurrentLeader(): string = 
            match lastPlay with
            | None -> ""
            | Some(_, player) -> player.name

        member x.GetTurn(): int = turn

        member x.CheckAllowed(action: string): string = 
            match lastPlay, action with
            | None, "pass" -> "You cannot pass when opening a trick."
            | None, _ -> "OK"
            | _, "pass" -> "OK"
            | Some(card, _), setstring -> 
                let cardPlayed = {value = setstring[0]};
                if cardPlayed.IntValue() > card.IntValue() then "OK" else "Your card has to be higher than the last played card."

        member this.DoTurn(name: string, action: string): ITichu = 
            this.checkErrors(name, action)

            match action with 
            | "pass" -> Pass(name)
            | setstring -> PlaySet(name, setstring)


        member x.IsEndOfGame(): bool = 
            failwith "Not Implemented"