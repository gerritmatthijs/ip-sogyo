module TichuFactoryTests

open Xunit
open Tichu

[<Fact>]
let ``Create game through factory`` () = 
    let factory = new TichuFactory() :> ITichuFactory
    let tichuGame = factory.CreateNewGame(["Gerrit"; "Daniel"])
    Assert.Equal(13, tichuGame.GetPlayerHand("Gerrit").Length)
    Assert.Equal(13, tichuGame.GetPlayerHand("Daniel").Length)
    Assert.NotEqual<string>(tichuGame.GetPlayerHand("Gerrit"), tichuGame.GetPlayerHand("Daniel"))
    Assert.NotEqual<string>("2222333344445", tichuGame.GetPlayerHand("Gerrit"))

