module CardSetTests

open Xunit
open Tichu

[<Fact>]
let ``Transform Multiple to string`` () = 
    let jackTriple = Multiple(Card.Card('J'), 3)|> CardSet.CardSetToString
    Assert.Equal("JJJ", jackTriple)

[<Fact>]
let ``Transform Full House to string`` () = 
    let fullHouse1 = FullHouse(Card.Card('2'), Card.Card('J')) |> CardSet.CardSetToString
    let fullHouse2 = FullHouse(Card.Card('T'), Card.Card('3')) |> CardSet.CardSetToString
    Assert.Equal("222JJ", fullHouse1)
    Assert.Equal("33TTT", fullHouse2)

[<Fact>]
let ``Transform string to Multiple CardSet`` () = 
    let TenQuadruple = "TTTT" |> CardSet.StringToCardSet
    Assert.Equal("TTTT", TenQuadruple |> CardSet.CardSetToString)

[<Fact>]
let ``Transform string to Full House CardSet`` () = 
    let fullHouseOne = "333QQ" |> CardSet.StringToCardSet
    let fullHouseTwo = "66KKK" |> CardSet.StringToCardSet
    Assert.Equal("333QQ", fullHouseOne |> CardSet.CardSetToString)
    Assert.Equal("66KKK", fullHouseTwo |> CardSet.CardSetToString)

[<Fact>]
let ``Transform string to NonExistant set`` () = 
    let invalidCardSetOne = "56" |> CardSet.StringToCardSet
    let invalidCardSetTwo = "22225" |> CardSet.StringToCardSet
    Assert.Equal(NonExistant, invalidCardSetOne)
    Assert.Equal(NonExistant, invalidCardSetTwo)

[<Fact>]
let ``Multiples are recognised as the same type`` () = 
    let jackTriple = "JJJ" |> CardSet.StringToCardSet
    let twoTriple = "222" |> CardSet.StringToCardSet
    Assert.True(jackTriple |> CardSet.IsSameTypeAs(twoTriple))

[<Fact>]
let ``Full Houses are recognised as the same type`` () =
    let fullHouseOne = "22255" |> CardSet.StringToCardSet
    let fullHouseTwo = "88TTT" |> CardSet.StringToCardSet
    Assert.True(fullHouseTwo |> CardSet.IsSameTypeAs(fullHouseOne))

[<Fact>]
let ``CardSets of different types are told apart`` () = 
    let jackDouble = "JJ" |> CardSet.StringToCardSet
    let twoTriple = "222" |> CardSet.StringToCardSet
    let fullHouse = "222JJ" |> CardSet.StringToCardSet
    Assert.False(jackDouble |> CardSet.IsSameTypeAs(twoTriple))
    Assert.False(fullHouse |> CardSet.IsSameTypeAs(twoTriple))

[<Fact>]
let ``Higher CardSet is recognised as higher, but not conversely`` () = 
    let kingTriple = "KKK" |> CardSet.StringToCardSet
    let tenTriple = "TTT" |> CardSet.StringToCardSet
    Assert.True(kingTriple |> CardSet.IsHigherThen(tenTriple))
    Assert.False(tenTriple |> CardSet.IsHigherThen(kingTriple))

[<Fact>]
let ``Equal height CardSets are not recognised as higher`` () =
    let jackPair = "JJ" |> CardSet.StringToCardSet
    let anotherJackPair = "JJ" |> CardSet.StringToCardSet
    Assert.False(jackPair |> CardSet.IsHigherThen(anotherJackPair))

[<Fact>]
let ``Trying to compare different set types throws`` () = 
    let jackPair = "JJ" |> CardSet.StringToCardSet
    let twoTriple = "222" |> CardSet.StringToCardSet
    Assert.Throws<System.Exception>(fun () -> jackPair |> CardSet.IsHigherThen(twoTriple) :> obj)

[<Fact>]
let ``Convert CardSet to Card list`` () = 
    let sixPair = "66" |> CardSet.StringToCardSet
    let sixPairList = sixPair |> CardSet.CardSetToCardList
    Assert.Equal(2, sixPairList.Length)
    Assert.Equal('6', sixPairList[0].value)

[<Fact>]
let ``Return NonExistant when trying to convert invalid set`` () = 
    let setstring = "56"
    Assert.Equal(NonExistant, setstring |> CardSet.StringToCardSet)