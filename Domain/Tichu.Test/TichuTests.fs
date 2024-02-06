module TichuTests

open Xunit
open Tichu

let SetUpGame () = 
    let playerOne = {name = "Gerrit"; hand = "2222333344445" |> Hand.StringToCardList}
    let playerTwo = {name = "Daniel"; hand = "5556666777788" |> Hand.StringToCardList}
    let playerThree = {name = "Wesley"; hand = "889999TTTTJJJ" |> Hand.StringToCardList}
    let playerFour = {name = "Hanneke"; hand = "JQQQQKKKKAAAA" |> Hand.StringToCardList}
    new TichuGame([playerOne; playerTwo; playerThree; playerFour], None) :> ITichu

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
let ``Play single card removes it from hand`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPlayed = tichu.DoTurn("Daniel", "6")
    // let hand = 
    //     match tryNewTichu with 
    //     | Ok newTichu -> newTichu.GetPlayerHand("Gerrit")
    //     | Error e -> handstring
    // Assert.True(Result.isOk tryNewTichu)
    Assert.Equal("222233334445", gerritPlayed.GetPlayerHand("Gerrit"))
    Assert.Equal("555666777788", danielPlayed.GetPlayerHand("Daniel"))

[<Fact>]
let ``Get last played card before and after first play`` () = 
    let tichu = SetUpGame()
    let newTichu = tichu.DoTurn("Gerrit", "3")
    Assert.Equal("", tichu.GetLastPlayed())
    Assert.Equal("3", newTichu.GetLastPlayed())

[<Fact>]
let ``Play higher card is allowed`` () = 
    let tichu = SetUpGame()
    let tichu1 = tichu.DoTurn("Wesley", "T")
    Assert.Equal("OK", tichu1.CheckAllowed "J")

[<Fact>]
let ``Play lower or equal card is not allowed`` () = 
    let tichu = SetUpGame()
    let tichu1 = tichu.DoTurn("Wesley", "T")
    Assert.Equal("Your card has to be higher than the last played card.", tichu1.CheckAllowed("9"))
    Assert.Equal("Your card has to be higher than the last played card.", tichu1.CheckAllowed("T"))

[<Fact>]
let ``DoTurn throws exception if move is not allowed`` () = 
    let tichu = SetUpGame()
    let tichu1 = tichu.DoTurn("Gerrit", "4")
    Assert.Throws<System.Exception>(fun () -> tichu1.DoTurn("Gerrit", "3") :> obj)