module CardTests

open Xunit
open Tichu

[<Fact>]
let ``Card creation`` () =
    let jack = Card.Card('J')
    let dragon = Card.Card('D')
    Assert.Equal('J', jack.CharValue())
    Assert.Equal('D', dragon.CharValue())

[<Fact>]
let ``Card has correct integer Value`` () = 
    let jack = Card.Card('J')
    let two = Card.Card('2')
    let dragon = Card.Card('D')
    Assert.Equal(11, jack.IntValue())
    Assert.Equal(2, two.IntValue())
    Assert.Equal(100, dragon.IntValue())

[<Fact>]
let ``Convert string to card list and back`` () = 
    let handstring = "24TD"
    let convertedHand = Card.StringToCardList(handstring)
    let doubleConvertedHand = Card.CardListToString(convertedHand)
    Assert.Equal<Card list>([Card.Card('2'); Card.Card('4'); Card.Card('T'); Card.Card('D')], convertedHand)
    Assert.Equal(handstring, doubleConvertedHand)