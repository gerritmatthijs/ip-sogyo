module TichuFactoryTests

open Xunit
open Tichu

[<Fact>]
let ``Create game through factory`` () = 
    let factory = new TichuFactory() :> ITichuFactory
    let tichuGame = factory.createNewGame(["Gerrit"; "Daniel"])
    Assert.Equal(13, tichuGame.GetPlayerHand("Gerrit").Length)
    Assert.Equal(13, tichuGame.GetPlayerHand("Daniel").Length)
    Assert.NotEqual<string>(tichuGame.GetPlayerHand("Gerrit"), tichuGame.GetPlayerHand("Daniel"))
