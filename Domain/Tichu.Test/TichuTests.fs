module TichuTests

open Xunit
open Tichu

let SetUpGame () = 
    let playerOne = {name = "Gerrit"; hand = "2222333344445" |> Hand.StringToCardList}
    let playerTwo = {name = "Daniel"; hand = "5556666777788" |> Hand.StringToCardList}
    let playerThree = {name = "Wesley"; hand = "889999TTTTJJJ" |> Hand.StringToCardList}
    let playerFour = {name = "Hanneke"; hand = "JQQQQKKKKAAAA" |> Hand.StringToCardList}
    new TichuGame([playerOne; playerTwo; playerThree; playerFour], None, 0) :> ITichu

[<Fact>]
let ``Get player name`` () = 
    let tichu = SetUpGame()
    Assert.Equal("Gerrit", tichu.GetPlayerName(0))
    Assert.Equal("Daniel", tichu.GetPlayerName(1))
    Assert.Equal("Wesley", tichu.GetPlayerName(2))
    Assert.Equal("Hanneke", tichu.GetPlayerName(3))

[<Fact>]
let ``Get player hand`` () = 
    let tichu = SetUpGame()
    Assert.Equal("2222333344445", tichu.GetPlayerHand("Gerrit"))
    Assert.Equal("5556666777788", tichu.GetPlayerHand("Daniel"))
    Assert.Equal("889999TTTTJJJ", tichu.GetPlayerHand("Wesley"))
    Assert.Equal("JQQQQKKKKAAAA", tichu.GetPlayerHand("Hanneke"))

[<Fact>]
let ``Get last played card before and after first play`` () = 
    let tichu = SetUpGame()
    let newTichu = tichu.DoTurn("Gerrit", "3")
    Assert.Equal("", tichu.GetLastPlayed())
    Assert.Equal("3", newTichu.GetLastPlayed())

[<Fact>]
let ``Get index of player whose turn it is`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPlayed = gerritPlayed.DoTurn("Daniel", "6")
    let wesleyPlayed = danielPlayed.DoTurn("Wesley", "T")
    let hannekePlayed = wesleyPlayed.DoTurn("Hanneke", "K")
    Assert.Equal(0, tichu.GetTurn())
    Assert.Equal(1, gerritPlayed.GetTurn())
    Assert.Equal(2, danielPlayed.GetTurn())
    Assert.Equal(3, wesleyPlayed.GetTurn())
    Assert.Equal(0, hannekePlayed.GetTurn())

[<Fact>]
let ``Throw when attempting to play out of turn`` () =
    let tichu = SetUpGame()
    Assert.Throws<System.Exception>(fun () -> tichu.DoTurn("Daniel", "6") :> obj)

[<Fact>]
let ``Play single card removes it from hand`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPlayed = gerritPlayed.DoTurn("Daniel", "6")
    // let hand = 
    //     match tryNewTichu with 
    //     | Ok newTichu -> newTichu.GetPlayerHand("Gerrit")
    //     | Error e -> handstring
    // Assert.True(Result.isOk tryNewTichu)
    Assert.Equal("222233334445", danielPlayed.GetPlayerHand("Gerrit"))
    Assert.Equal("555666777788", danielPlayed.GetPlayerHand("Daniel"))

[<Fact>]
let ``Play higher card is allowed`` () = 
    let tichu = SetUpGame()
    let tichu1 = tichu.DoTurn("Gerrit", "3")
    Assert.Equal("OK", tichu1.CheckAllowed "4")

[<Fact>]
let ``Play lower or equal card is not allowed`` () = 
    let tichu = SetUpGame()
    let tichu1 = tichu.DoTurn("Gerrit", "4")
    Assert.Equal("Your card has to be higher than the last played card.", tichu1.CheckAllowed("3"))
    Assert.Equal("Your card has to be higher than the last played card.", tichu1.CheckAllowed("4"))

[<Fact>]
let ``DoTurn throws exception if move is not allowed`` () = 
    let tichu = SetUpGame()
    let tichu1 = tichu.DoTurn("Gerrit", "4")
    Assert.Throws<System.Exception>(fun () -> tichu1.DoTurn("Gerrit", "3") :> obj)