module TichuTests

open Xunit
open Tichu

let SetUpGame () = 
    let playerOne = {name = "Gerrit"; hand = "2222333344445" |> Hand.StringToCardList}
    let playerTwo = {name = "Daniel"; hand = "5556666777788" |> Hand.StringToCardList}
    let playerThree = {name = "Wesley"; hand = "889999TTTTJJJ" |> Hand.StringToCardList}
    let playerFour = {name = "Hanneke"; hand = "JQQQQKKKKAAAA" |> Hand.StringToCardList}
    new TichuFacade([playerOne; playerTwo; playerThree; playerFour]) :> ITichu

let SetUpGameEmptyHand() = 
    let playerOne = {name = "Gerrit"; hand = "2222333344445" |> Hand.StringToCardList}
    let playerTwo = {name = "Daniel"; hand = "" |> Hand.StringToCardList}
    let playerThree = {name = "Wesley"; hand = "889999TTTTJJJ" |> Hand.StringToCardList}
    let playerFour = {name = "Hanneke"; hand = "JQQQQKKKKAAAA" |> Hand.StringToCardList}
    new TichuFacade([playerOne; playerTwo; playerThree; playerFour]) :> ITichu

let SetUpGameAlmostEmptyHand() = 
    let playerOne = {name = "Gerrit"; hand = "4" |> Hand.StringToCardList}
    let playerTwo = {name = "Daniel"; hand = "5556666777788" |> Hand.StringToCardList}
    let playerThree = {name = "Wesley"; hand = "889999TTTTJJJ" |> Hand.StringToCardList}
    let playerFour = {name = "Hanneke"; hand = "JQQQQKKKKAAAA" |> Hand.StringToCardList}
    new TichuFacade([playerOne; playerTwo; playerThree; playerFour]) :> ITichu

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
let ``Get current leader`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPlayed = gerritPlayed.DoTurn("Daniel", "6")
    let wesleyPlayed = danielPlayed.DoTurn("Wesley", "T")
    let hannekePlayed = wesleyPlayed.DoTurn("Hanneke", "K")
    Assert.Equal("", tichu.GetCurrentLeader())
    Assert.Equal("Gerrit", gerritPlayed.GetCurrentLeader())
    Assert.Equal("Daniel", danielPlayed.GetCurrentLeader())
    Assert.Equal("Wesley", wesleyPlayed.GetCurrentLeader())
    Assert.Equal("Hanneke", hannekePlayed.GetCurrentLeader())

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
let ``Passing doesn't change last action or player's hand but does change turn`` () =
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPassed = gerritPlayed.DoTurn("Daniel", "pass")
    Assert.Equal(13, danielPassed.GetPlayerHand("Daniel").Length)
    Assert.Equal(2, danielPassed.GetTurn())
    Assert.Equal("Gerrit", danielPassed.GetCurrentLeader())
    Assert.Equal("4", danielPassed.GetLastPlayed())

[<Fact>]
let ``When three people pass, the trick ends`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPassed = gerritPlayed.DoTurn("Daniel", "pass")
    let wesleyPassed = danielPassed.DoTurn("Wesley", "pass")
    let everyonePassed = wesleyPassed.DoTurn("Hanneke", "pass")
    Assert.Equal("", everyonePassed.GetCurrentLeader())
    Assert.Equal("", everyonePassed.GetLastPlayed())
    Assert.Equal(0, everyonePassed.GetTurn());

[<Fact>]
let ``Player with empty hand does not get a turn`` () = 
    let tichu = SetUpGameEmptyHand()

    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    Assert.Equal(2, gerritPlayed.GetTurn())

[<Fact>]
let ``Two consecutive players with empty hands are both skipped`` () = 
    let playerOne = {name = "Gerrit"; hand = "2222333344445" |> Hand.StringToCardList}
    let playerTwo = {name = "Daniel"; hand = "" |> Hand.StringToCardList}
    let playerThree = {name = "Wesley"; hand = "" |> Hand.StringToCardList}
    let playerFour = {name = "Hanneke"; hand = "JQQQQKKKKAAAA" |> Hand.StringToCardList}
    let tichu = new TichuFacade([playerOne; playerTwo; playerThree; playerFour]) :> ITichu

    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    Assert.Equal(3, gerritPlayed.GetTurn())

[<Fact>]
let ``When a player wins the trick with their last cards, the next player starts the next trick`` () = 
    let tichu = SetUpGameAlmostEmptyHand()

    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPassed = gerritPlayed.DoTurn("Daniel", "pass")
    let wesleyPassed = danielPassed.DoTurn("Wesley", "pass")
    let hannekePassed = wesleyPassed.DoTurn("Hanneke", "pass")
    Assert.Equal(1, hannekePassed.GetTurn())
    Assert.Equal("", hannekePassed.GetLastPlayed())
    Assert.Equal("", hannekePassed.GetCurrentLeader())

[<Fact>]
let ``Check situation: a player is out and the next player wins a trick`` () = 
    let tichu = SetUpGameEmptyHand()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let wesleyPlayed = gerritPlayed.DoTurn("Wesley", "T")
    let hannekePassed = wesleyPlayed.DoTurn("Hanneke", "pass")
    let gerritPassed = hannekePassed.DoTurn("Gerrit", "pass")
    Assert.Equal(2, gerritPassed.GetTurn())
    Assert.Equal("", gerritPassed.GetLastPlayed())
    Assert.Equal("", gerritPassed.GetCurrentLeader())

[<Fact>]
let ``Check situation: a player is out and the next player wins a trick with their last cards`` () = 
    let playerOne = {name = "Gerrit"; hand = "2222333344445" |> Hand.StringToCardList}
    let playerTwo = {name = "Daniel"; hand = "" |> Hand.StringToCardList}
    let playerThree = {name = "Wesley"; hand = "T" |> Hand.StringToCardList}
    let playerFour = {name = "Hanneke"; hand = "JQQQQKKKKAAAA" |> Hand.StringToCardList}
    let tichu = new TichuFacade([playerOne; playerTwo; playerThree; playerFour]) :> ITichu

    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let wesleyPlayed = gerritPlayed.DoTurn("Wesley", "T")
    let hannekePassed = wesleyPlayed.DoTurn("Hanneke", "pass")
    let gerritPassed = hannekePassed.DoTurn("Gerrit", "pass")
    Assert.Equal(3, gerritPassed.GetTurn())
    Assert.Equal("", gerritPassed.GetLastPlayed())
    Assert.Equal("", gerritPassed.GetCurrentLeader())

[<Fact>]
let ``Check situation: a player is out and the previous player wins a trick with their last cards`` () = 
    let playerOne = {name = "Gerrit"; hand = "2222333344445" |> Hand.StringToCardList}
    let playerTwo = {name = "Daniel"; hand = "6" |> Hand.StringToCardList}
    let playerThree = {name = "Wesley"; hand = "" |> Hand.StringToCardList}
    let playerFour = {name = "Hanneke"; hand = "JQQQQKKKKAAAA" |> Hand.StringToCardList}
    let tichu = new TichuFacade([playerOne; playerTwo; playerThree; playerFour]) :> ITichu

    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPlayed = gerritPlayed.DoTurn("Daniel", "6")
    let hannekePassed = danielPlayed.DoTurn("Hanneke", "pass")
    let gerritPassed = hannekePassed.DoTurn("Gerrit", "pass")
    Assert.Equal(3, gerritPassed.GetTurn())
    Assert.Equal("", gerritPassed.GetLastPlayed())
    Assert.Equal("", gerritPassed.GetCurrentLeader())

[<Fact>]
let ``No message when nothing special happens`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPassed = gerritPlayed.DoTurn("Daniel", "pass")
    let wesleyPassed = danielPassed.DoTurn("Wesley", "pass")
    Assert.Equal("", tichu.GetMessage())
    Assert.Equal("", gerritPlayed.GetMessage())
    Assert.Equal("", danielPassed.GetMessage())
    Assert.Equal("", wesleyPassed.GetMessage())

[<Fact>]
let ``Get Message upon winning a trick`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPassed = gerritPlayed.DoTurn("Daniel", "pass")
    let wesleyPassed = danielPassed.DoTurn("Wesley", "pass")
    let everyonePassed = wesleyPassed.DoTurn("Hanneke", "pass")
    Assert.Equal("Gerrit has won the trick!", everyonePassed.GetMessage())

[<Fact>]
let ``Get Message upon playing last card`` () = 
    let tichu = SetUpGameAlmostEmptyHand()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    Assert.Equal("Gerrit has played all their cards!", gerritPlayed.GetMessage())

[<Fact>]
let ``Trying to pass when opening a trick gives alert`` () = 
    let tichu = SetUpGame()
    let gerritTriedPassing = tichu.DoTurn("Gerrit", "pass")
    Assert.Equal("You cannot pass when opening a trick.", gerritTriedPassing.GetAlert())

[<Fact>]
let ``No alert when nothing special happens`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("Gerrit", "4")
    let danielPassed = gerritPlayed.DoTurn("Daniel", "6")
    let wesleyPassed = danielPassed.DoTurn("Wesley", "pass")
    Assert.Equal("", tichu.GetAlert())
    Assert.Equal("", gerritPlayed.GetAlert())
    Assert.Equal("", danielPassed.GetAlert())
    Assert.Equal("", wesleyPassed.GetAlert())

[<Fact>]
let ``Get alert when playing a lower or equal card`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("Gerrit", "5")
    let danielTriedPlaying = gerritPlayed.DoTurn("Daniel", "5")
    let danielPlayed = danielTriedPlaying.DoTurn("Daniel", "6")
    let wesleyPlayed = danielPlayed.DoTurn("Wesley", "T")
    let hannekePlayed = wesleyPlayed.DoTurn("Hanneke", "K")
    let gerritPlayedAgain = hannekePlayed.DoTurn("Gerrit", "4")
    
    Assert.Equal("Your card has to be higher than the last played card.", danielTriedPlaying.GetAlert())
    Assert.Equal("Your card has to be higher than the last played card.", gerritPlayedAgain.GetAlert())

[<Fact>]
let ``Get alert upon passing when starting a trick`` () = 
    let tichu = SetUpGame()
    let gerritTriedPassing = tichu.DoTurn("Gerrit", "pass")
    Assert.Equal("You cannot pass when opening a trick.", gerritTriedPassing.GetAlert())