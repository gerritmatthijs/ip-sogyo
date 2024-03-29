module TichuTests

open Xunit
open Tichu

let SetUpGame () = 
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2222333344445"; "5556666777788"; "889999TTTTJJJ"; "JQQQQKKKKAAAA"]
    new TichuFacade(names, hands, "", "", 0) :> ITichuFacade

let SetUpGameEmptyHand() = 
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2222333344445"; ""; "889999TTTTJJJ"; "JQQQQKKKKAAAA"]
    new TichuFacade(names, hands, "", "", 0) :> ITichuFacade

let SetUpGameAlmostEmptyHand() = 
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["4"; "5556666777788"; "889999TTTTJJJ"; "JQQQQKKKKAAAA"]
    new TichuFacade(names, hands, "", "", 0) :> ITichuFacade

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
    Assert.Equal("2222333344445", tichu.GetPlayerHand(0))
    Assert.Equal("5556666777788", tichu.GetPlayerHand(1))
    Assert.Equal("889999TTTTJJJ", tichu.GetPlayerHand(2))
    Assert.Equal("JQQQQKKKKAAAA", tichu.GetPlayerHand(3))

[<Fact>]
let ``Get last played card before and after first play`` () = 
    let tichu = SetUpGame()
    let newTichu = tichu.DoTurn("3")
    Assert.Equal("", tichu.GetLastPlayed())
    Assert.Equal("3", newTichu.GetLastPlayed())

[<Fact>]
let ``Get current leader`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("4")
    let danielPlayed = gerritPlayed.DoTurn("6")
    let wesleyPlayed = danielPlayed.DoTurn("T")
    let hannekePlayed = wesleyPlayed.DoTurn("K")
    Assert.Equal("", tichu.GetCurrentLeader())
    Assert.Equal("Gerrit", gerritPlayed.GetCurrentLeader())
    Assert.Equal("Daniel", danielPlayed.GetCurrentLeader())
    Assert.Equal("Wesley", wesleyPlayed.GetCurrentLeader())
    Assert.Equal("Hanneke", hannekePlayed.GetCurrentLeader())

[<Fact>]
let ``Get index of player whose turn it is`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("4")
    let danielPlayed = gerritPlayed.DoTurn("6")
    let wesleyPlayed = danielPlayed.DoTurn("T")
    let hannekePlayed = wesleyPlayed.DoTurn("K")
    Assert.Equal(0, tichu.GetTurn())
    Assert.Equal(1, gerritPlayed.GetTurn())
    Assert.Equal(2, danielPlayed.GetTurn())
    Assert.Equal(3, wesleyPlayed.GetTurn())
    Assert.Equal(0, hannekePlayed.GetTurn())

[<Fact>]
let ``Throw when attempting to play a card that is not in hand`` () =
    let tichu = SetUpGame()
    Assert.Throws<System.Exception>(fun () -> tichu.DoTurn("6") :> obj)

[<Fact>]
let ``Play single card removes it from hand`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("4")
    let danielPlayed = gerritPlayed.DoTurn("6")
    Assert.Equal("222233334445", danielPlayed.GetPlayerHand(0))
    Assert.Equal("555666777788", danielPlayed.GetPlayerHand(1))

[<Fact>]
let ``Play sets removes all  cards from hand`` () =
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("444")
    let danielPlayed = gerritPlayed.DoTurn("666")
    Assert.Equal("2222333345", danielPlayed.GetPlayerHand(0))
    Assert.Equal("5556777788", danielPlayed.GetPlayerHand(1))

[<Fact>]
let ``Playing Full House removes all cards from hand`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("33444")
    let danielPlayed = gerritPlayed.DoTurn("66677")
    Assert.Equal("22223345", gerritPlayed.GetPlayerHand(0))
    Assert.Equal("55567788", danielPlayed.GetPlayerHand(1))

[<Fact>]
let ``Playing straight removes all cards from hand`` () =
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2222333444567"; "3455566677788"; "889999TTTTJJJ"; "JQQQQKKKKAAAA"]
    let tichu = new TichuFacade(names, hands, "", "", 0) :> ITichuFacade

    let gerritPlayed = tichu.DoTurn("234567")
    let danielPlayed = gerritPlayed.DoTurn("345678")
    Assert.Equal("2223344", gerritPlayed.GetPlayerHand(0))
    Assert.Equal("5566778", danielPlayed.GetPlayerHand(1))

[<Fact>]
let ``Playing a set that is not allowed does not change the hand`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("5")
    let danielPlayed = gerritPlayed.DoTurn("5")
    Assert.Equal(13, danielPlayed.GetPlayerHand(1).Length)

[<Fact>]
let ``Playing the wrong type of set does not change the hand`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("444")
    let danielPlayed = gerritPlayed.DoTurn("66")
    Assert.Equal(13, danielPlayed.GetPlayerHand(1).Length)

[<Fact>]
let ``Passing doesn't change last action or player's hand but does change turn`` () =
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("4")
    let danielPassed = gerritPlayed.DoTurn("pass")
    Assert.Equal(13, danielPassed.GetPlayerHand(1).Length)
    Assert.Equal(2, danielPassed.GetTurn())
    Assert.Equal("Gerrit", danielPassed.GetCurrentLeader())
    Assert.Equal("4", danielPassed.GetLastPlayed())

[<Fact>]
let ``When three people pass, the trick ends`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("4")
    let danielPassed = gerritPlayed.DoTurn("pass")
    let wesleyPassed = danielPassed.DoTurn("pass")
    let everyonePassed = wesleyPassed.DoTurn("pass")
    Assert.Equal("", everyonePassed.GetCurrentLeader())
    Assert.Equal("", everyonePassed.GetLastPlayed())
    Assert.Equal(0, everyonePassed.GetTurn());

[<Fact>]
let ``Player with empty hand does not get a turn`` () = 
    let tichu = SetUpGameEmptyHand()

    let gerritPlayed = tichu.DoTurn("4")
    Assert.Equal(2, gerritPlayed.GetTurn())

[<Fact>]
let ``Two consecutive players with empty hands are both skipped`` () = 
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2222333344445"; ""; ""; "JQQQQKKKKAAAA"]
    let tichu = new TichuFacade(names, hands, "", "", 0) :> ITichuFacade

    let gerritPlayed = tichu.DoTurn("4")
    Assert.Equal(3, gerritPlayed.GetTurn())

[<Fact>]
let ``When a player wins the trick with their last cards, the next player starts the next trick`` () = 
    let tichu = SetUpGameAlmostEmptyHand()

    let gerritPlayed = tichu.DoTurn("4")
    let danielPassed = gerritPlayed.DoTurn("pass")
    let wesleyPassed = danielPassed.DoTurn("pass")
    let hannekePassed = wesleyPassed.DoTurn("pass")
    Assert.Equal(1, hannekePassed.GetTurn())
    Assert.Equal("", hannekePassed.GetLastPlayed())
    Assert.Equal("", hannekePassed.GetCurrentLeader())

[<Fact>]
let ``Check situation: a player is out and the next player wins a trick`` () = 
    let tichu = SetUpGameEmptyHand()
    let gerritPlayed = tichu.DoTurn("4")
    let wesleyPlayed = gerritPlayed.DoTurn("T")
    let hannekePassed = wesleyPlayed.DoTurn("pass")
    let gerritPassed = hannekePassed.DoTurn("pass")
    Assert.Equal(2, gerritPassed.GetTurn())
    Assert.Equal("", gerritPassed.GetLastPlayed())
    Assert.Equal("", gerritPassed.GetCurrentLeader())

[<Fact>]
let ``Check situation: a player is out and the next player wins a trick with their last cards`` () = 
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2222333344445"; ""; "T"; "JQQQQKKKKAAAA"]
    let tichu = new TichuFacade(names, hands, "", "", 0) :> ITichuFacade

    let gerritPlayed = tichu.DoTurn("4")
    let wesleyPlayed = gerritPlayed.DoTurn("T")
    let hannekePassed = wesleyPlayed.DoTurn("pass")
    let gerritPassed = hannekePassed.DoTurn("pass")
    Assert.Equal(3, gerritPassed.GetTurn())
    Assert.Equal("", gerritPassed.GetLastPlayed())
    Assert.Equal("", gerritPassed.GetCurrentLeader())

[<Fact>]
let ``Check situation: a player is out and the previous player wins a trick with their last cards`` () = 
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2222333344445"; "6"; ""; "JQQQQKKKKAAAA"]
    let tichu = new TichuFacade(names, hands, "", "", 0) :> ITichuFacade

    let gerritPlayed = tichu.DoTurn("4")
    let danielPlayed = gerritPlayed.DoTurn("6")
    let hannekePassed = danielPlayed.DoTurn("pass")
    let gerritPassed = hannekePassed.DoTurn("pass")
    Assert.Equal(3, gerritPassed.GetTurn())
    Assert.Equal("", gerritPassed.GetLastPlayed())
    Assert.Equal("", gerritPassed.GetCurrentLeader())

[<Fact>]
let ``No message when nothing special happens`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("4")
    let danielPassed = gerritPlayed.DoTurn("pass")
    let wesleyPassed = danielPassed.DoTurn("pass")
    Assert.Equal("", tichu.GetMessage())
    Assert.Equal("", gerritPlayed.GetMessage())
    Assert.Equal("", danielPassed.GetMessage())
    Assert.Equal("", wesleyPassed.GetMessage())

[<Fact>]
let ``Get Message upon winning a trick`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("4")
    let danielPassed = gerritPlayed.DoTurn("pass")
    let wesleyPassed = danielPassed.DoTurn("pass")
    let everyonePassed = wesleyPassed.DoTurn("pass")
    Assert.Equal("Gerrit has won the trick!", everyonePassed.GetMessage())

[<Fact>]
let ``Get Message upon playing last card`` () = 
    let tichu = SetUpGameAlmostEmptyHand()
    let gerritPlayed = tichu.DoTurn("4")
    Assert.Equal("Gerrit has played all their cards!", gerritPlayed.GetMessage())

[<Fact>]
let ``No alert when nothing special happens`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("4")
    let danielPassed = gerritPlayed.DoTurn("6")
    let wesleyPassed = danielPassed.DoTurn("pass")
    Assert.Equal("", tichu.GetAlert())
    Assert.Equal("", gerritPlayed.GetAlert())
    Assert.Equal("", danielPassed.GetAlert())
    Assert.Equal("", wesleyPassed.GetAlert())

[<Fact>]
let ``Get alert when playing a lower or equal card`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("5")
    let danielTriedPlaying = gerritPlayed.DoTurn("5")
    let peoplePlayed = danielTriedPlaying.DoTurn("6").DoTurn("T").DoTurn("K")
    let gerritPlayedAgain = peoplePlayed.DoTurn("4")
    
    Assert.Equal("Your card set has to be higher than the last played card set.", danielTriedPlaying.GetAlert())
    Assert.Equal("Your card set has to be higher than the last played card set.", gerritPlayedAgain.GetAlert())

[<Fact>]
let ``Get alert when playing lower or equal full house`` () =
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["22333344445KK"; "5556666777788"; "889999TTTTJJJ"; "22JQQQQKKAAAA"]
    let tichu = new TichuFacade(names, hands, "", "", 3) :> ITichuFacade

    let hannekePlayed = tichu.DoTurn("22QQQ")
    let gerritTriedPlaying = hannekePlayed.DoTurn("44433")
    Assert.Equal("Your card set has to be higher than the last played card set.", gerritTriedPlaying.GetAlert())


[<Fact>]
let ``Get alert upon passing when starting a trick`` () = 
    let tichu = SetUpGame()
    let gerritTriedPassing = tichu.DoTurn("pass")
    Assert.Equal("You cannot pass when opening a trick.", gerritTriedPassing.GetAlert())

[<Fact>]
let ``Get alert upon playing the wrong type of set`` () = 
    let tichu = SetUpGame()
    let gerritPlayed = tichu.DoTurn("444")
    let danielTriedPlaying = gerritPlayed.DoTurn("5")
    Assert.Equal("You can only play sets of the same type as the leading set.", danielTriedPlaying.GetAlert())

[<Fact>]
let ``Get alert upon playing invalid set`` () =
    let tichu = SetUpGame()
    let playInvalidSet = tichu.DoTurn("56")
    Assert.Equal("Invalid card set.", playInvalidSet.GetAlert())

[<Fact>]
let ``Game ends when 3 players play all their cards`` () = 
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["5"; ""; ""; "JQQQQKKKKAAAA"]
    let tichu = new TichuFacade(names, hands, "", "", 0) :> ITichuFacade

    let gerritFinished = tichu.DoTurn("5")
    Assert.False(tichu.IsEndOfGame())
    Assert.True(gerritFinished.IsEndOfGame())