module SpecialCardTests

open Xunit
open Tichu

let SetUpGame () = 
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["1222233334444P"; "55556666777788"; "889999TTTTJJJH"; "JQQQQKKKKAAAAD"]
    new TichuFacade(names, hands) :> ITichuFacade

let HoundSetUp() = 
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2222333344445"; "5556666777788D"; "889999TTTTJJH"; "JQQQQKKKKAAAAD"]
    new TichuFacade(names, hands, "", "", 2) :> ITichuFacade

[<Fact>]
let ``Special cards are correctly converted to char`` () =
    let tichu = SetUpGame()
    Assert.Equal("1222233334444P", tichu.GetPlayerHand(0))
    Assert.Equal("55556666777788", tichu.GetPlayerHand(1))
    Assert.Equal("889999TTTTJJJH", tichu.GetPlayerHand(2))
    Assert.Equal("JQQQQKKKKAAAAD", tichu.GetPlayerHand(3))

[<Fact>]
let ``Player with the mahjong starts the game`` () =
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2222333344445D"; "5556666777788D"; "1889999TTTTJJJ"; "JQQQQKKKKAAAAD"]
    let tichu = new TichuFacade(names, hands) :> ITichuFacade
    Assert.Equal(2, tichu.GetTurn())

[<Fact>]
let ``Mahjong can be played as a 1`` () =
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2222333344445D"; "5556666777788D"; "1889999TTTTJJJ"; "JQQQQKKKKAAAAD"]
    let tichu = new TichuFacade(names, hands) :> ITichuFacade

    let mahjongPlayed = tichu.DoTurn("1")
    let twoPlayed = mahjongPlayed.DoTurn("pass").DoTurn("2")
    Assert.Equal("", twoPlayed.GetAlert())

[<Fact>]
let ``Mahjong can be played in a straight`` () =
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["12222333344445"; "5556666777788P"; "889999TTTTJJJH"; "JQQQQKKKKAAAAD"]
    let tichu = new TichuFacade(names, hands) :> ITichuFacade
    let gerritPlayedStraight = tichu.DoTurn("12345")
    Assert.Equal("", gerritPlayedStraight.GetAlert())

[<Fact>]
let ``Playing hound gives turn to opposite player`` () =
    let tichu = HoundSetUp()
    let houndPlayed = tichu.DoTurn("H")
    Assert.Equal(0, houndPlayed.GetTurn())

[<Fact>]
let ``Any set can be played on hound`` () =
    let tichu = HoundSetUp()
    let houndPlayed = tichu.DoTurn("H")
    let pairOfTwosPlayed = houndPlayed.DoTurn("22")
    Assert.Equal("", pairOfTwosPlayed.GetAlert())

[<Fact>]
let ``Passing after hound gives alert`` () =
    let tichu = HoundSetUp()
    let houndPlayed = tichu.DoTurn("H")
    let gerritTriedPassing = houndPlayed.DoTurn("pass")
    Assert.Equal("You cannot pass when opening a trick.", gerritTriedPassing.GetAlert())

[<Fact>]
let ``Phoenix can be played over any single card other than dragon`` () =
    let tichu = SetUpGame()
    let peoplePlayed = tichu.DoTurn("4").DoTurn("7").DoTurn("J").DoTurn("A")
    let phoenixPlayed = peoplePlayed.DoTurn("P")
    Assert.Equal(1, phoenixPlayed.GetTurn())
    Assert.Equal("P", phoenixPlayed.GetLastPlayed())

[<Fact>]
let ``Trying to play phoenix over dragon gives alert`` () = 
    let tichu = SetUpGame()
    let peoplePlayed = tichu.DoTurn("4").DoTurn("7").DoTurn("J").DoTurn("D")
    let triedToPlayPhoenix = peoplePlayed.DoTurn("P")
    Assert.Equal(0, triedToPlayPhoenix.GetTurn())
    Assert.Equal("Phoenix cannot be played over the dragon.", triedToPlayPhoenix.GetAlert())

[<Fact>]
let ``Phoenix when played single is 'transparent'`` () =
    let tichu = SetUpGame()
    let peoplePlayed = tichu.DoTurn("4").DoTurn("7").DoTurn("J").DoTurn("Q")
    let phoenixPlayed = peoplePlayed.DoTurn("P")
    let kingPlayedOverPhoenix = phoenixPlayed.DoTurn("pass").DoTurn("pass").DoTurn("K")
    Assert.Equal(1, phoenixPlayed.GetTurn())
    Assert.Equal(0, kingPlayedOverPhoenix.GetTurn())
    Assert.Equal("", kingPlayedOverPhoenix.GetAlert())

[<Fact>]
let ``Phoenix when played single at the start of the trick is higher than Mahjong but lower than 2`` () =
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2223333444455P"; "12556666777788"; "889999TTTTJJJH"; "JQQQQKKKKAAAAD"]
    let tichu = new TichuFacade(names, hands, "", "", 0) :> ITichuFacade
    
    let phoenixPlayed = tichu.DoTurn("P")
    let triedPlayMahjong = phoenixPlayed.DoTurn("1")
    let twoPlayed = phoenixPlayed.DoTurn("2")
    Assert.Equal(1, triedPlayMahjong.GetTurn())
    Assert.Equal(2, twoPlayed.GetTurn())

[<Fact>]
let ``Phoenix can be played as a pair`` () =
    let tichu = SetUpGame()
    let pairWithPhoenixPlayed = tichu.DoTurn("4P")
    Assert.Equal(1, pairWithPhoenixPlayed.GetTurn())

[<Fact>]
let ``Pair can be played over pair with phoenix`` () =
    let tichu = SetUpGame()
    let gerritPlayedPhoenixPair = tichu.DoTurn("4P")
    let danielPlayedPair = gerritPlayedPhoenixPair.DoTurn("77")
    Assert.Equal(2, danielPlayedPair.GetTurn())

[<Fact>]
let ``Pair with phoenix can be played over another pair`` () =
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["12222333344445"; "5556666777788P"; "889999TTTTJJJH"; "JQQQQKKKKAAAAD"]
    let tichu = new TichuFacade(names, hands) :> ITichuFacade

    let pairPlayed = tichu.DoTurn("44")
    let phoenixPairPlayed = pairPlayed.DoTurn("6P")
    Assert.Equal("", phoenixPairPlayed.GetAlert())

[<Fact>]
let ``Phoenix is not allowed as part of a 4 of a kind`` () =
    let tichu = SetUpGame()
    let gerritTriedBombWithPhoenix = tichu.DoTurn("333P")
    Assert.Equal("Invalid card set.", gerritTriedBombWithPhoenix.GetAlert())

[<Fact>]
let ``Phoenix can be played as part of a straight`` () =
    let tichu = SetUpGame()
    let gerritPlaysStraight = tichu.DoTurn("1234P")
    Assert.Equal("", gerritPlaysStraight.GetAlert())

[<Fact>]
let ``Phoenix can be played as part of a full house`` () =
    let tichu = SetUpGame()
    let gerritPlaysFullHouse = tichu.DoTurn("3334P")
    Assert.Equal("", gerritPlaysFullHouse.GetAlert())

[<Fact>]
let ``Phoenix is played as higher option in a full house`` () =
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["1222233334477P"; "44555566667788"; "889999TTTTJJJH"; "JQQQQKKKKAAAAD"]
    let tichu = new TichuFacade(names, hands) :> ITichuFacade

    let fullHouseSevensPlayed = tichu.DoTurn("2277P")
    let danielTriedFullHouseFives = fullHouseSevensPlayed.DoTurn("55566")
    Assert.Equal("Your card set has to be higher than the last played card set.", danielTriedFullHouseFives.GetAlert())

[<Fact>]
let ``Set with phoenix can be invalid`` () =
    let tichu = SetUpGame()
    let gerritPlaysGibberish = tichu.DoTurn("34P")
    Assert.Equal("Invalid card set.", gerritPlaysGibberish.GetAlert())