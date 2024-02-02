module TichuFactoryTests

open Xunit
open Tichu

[<Fact>]
let ``Create game through factory`` () = 
    let factory = new TichuFactory() :> ITichuFactory
    let tichuGame = factory.createNewGame(["Gerrit"])
    Assert.Equal(14, tichuGame.GetPlayerHand("Gerrit").Length)
