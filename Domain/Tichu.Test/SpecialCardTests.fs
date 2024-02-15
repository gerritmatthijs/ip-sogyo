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
let ``Player with the mahjong starts the game`` () =
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["2222333344445D"; "5556666777788D"; "1889999TTTTJJJ"; "JQQQQKKKKAAAAD"]
    let tichu = new TichuFacade(names, hands) :> ITichuFacade
    Assert.Equal(2, tichu.GetTurn())

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
let ``Phoenix is recognised as part of a multiple`` () =
    let two = "2" |> Card.StringToCardList
    let pairOfTens = "TT" |> Card.StringToCardList
    Assert.Equal(Some(Card.Card('2')), CardSet.GetPhoenixValue(two))
    Assert.Equal(Some(Card.Card('T')), CardSet.GetPhoenixValue(pairOfTens))

[<Fact>]
let ``Phoenix is not allowed as part of a 4 of a kind`` () =
    let jackTriple = "JJJ" |> Card.StringToCardList
    Assert.Equal(None, CardSet.GetPhoenixValue(jackTriple))

[<Fact>]
let ``Phoenix is recognised as part of a straight`` () = 
    let straightWithGap = "5689TJ" |> Card.StringToCardList
    Assert.Equal(Some(Card.Card('7')), CardSet.GetPhoenixValue(straightWithGap))

[<Fact>]
let ``Phoenix is recognised as the higher alternative of a Full House`` () =
    let twoPair = "66TT" |> Card.StringToCardList
    Assert.Equal(Some(Card.Card('T')), CardSet.GetPhoenixValue(twoPair))

[<Fact>]
let ``Phoenix is not recognised when no valid set can be made`` () =
    let gibberish = "56" |> Card.StringToCardList
    Assert.Equal(None, CardSet.GetPhoenixValue(gibberish))