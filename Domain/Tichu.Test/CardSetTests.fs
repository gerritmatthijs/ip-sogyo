module CardSetTests

open Xunit
open Tichu

let StringToCardSet(str: string) = 
    str |> Card.StringToCardList |> CardSet.ToCardSet

[<Fact>]
let ``Create Multiple CardSet`` () = 
    let TenQuadruple = "TTTT" |> StringToCardSet
    Assert.Equal(Multiple(Card.Card('T'), 4), TenQuadruple)

[<Fact>]
let `` Create Full House CardSet`` () = 
    let fullHouseOne = "333QQ" |> StringToCardSet
    let fullHouseTwo = "66KKK" |> StringToCardSet
    Assert.Equal(FullHouse(Card.Card('3'), Card.Card('Q')), fullHouseOne)
    Assert.Equal(FullHouse(Card.Card('K'), Card.Card('6')), fullHouseTwo)

[<Fact>]
let ``Invalid sets yield a NonExistant type set`` () = 
    let invalidCardSetOne = "56" |> StringToCardSet
    let invalidCardSetTwo = "22225" |> StringToCardSet
    Assert.Equal(Invalid, invalidCardSetOne)
    Assert.Equal(Invalid, invalidCardSetTwo)

[<Fact>]
let ``Multiples are recognised as the same type`` () = 
    let jackTriple = "JJJ" |> StringToCardSet
    let twoTriple = "222" |> StringToCardSet
    Assert.True(jackTriple |> CardSet.IsSameTypeAs(twoTriple))

[<Fact>]
let ``Full Houses are recognised as the same type`` () =
    let fullHouseOne = "22255" |> StringToCardSet
    let fullHouseTwo = "88TTT" |> StringToCardSet
    Assert.True(fullHouseTwo |> CardSet.IsSameTypeAs(fullHouseOne))

[<Fact>]
let ``CardSets of different types are told apart`` () = 
    let jackDouble = "JJ" |> StringToCardSet
    let twoTriple = "222" |> StringToCardSet
    let fullHouse = "222JJ" |> StringToCardSet
    Assert.False(jackDouble |> CardSet.IsSameTypeAs(twoTriple))
    Assert.False(fullHouse |> CardSet.IsSameTypeAs(twoTriple))

[<Fact>]
let ``Higher Multiple is recognised as higher, but not conversely`` () = 
    let kingTriple = "KKK" |> StringToCardSet
    let tenTriple = "TTT" |> StringToCardSet
    Assert.True(kingTriple |> CardSet.IsHigherThen(tenTriple))
    Assert.False(tenTriple |> CardSet.IsHigherThen(kingTriple))

[<Fact>]
let ``Higher Full House is recognised`` () =
    let fullHouseHigh = "44KKK" |> StringToCardSet
    let fullHouseLow = "TTTAA" |> StringToCardSet
    Assert.True(fullHouseHigh |> CardSet.IsHigherThen(fullHouseLow))
    Assert.False(fullHouseLow |> CardSet.IsHigherThen(fullHouseHigh))

[<Fact>]
let ``Equal height CardSets are not recognised as higher`` () =
    let jackPair = "JJ" |> StringToCardSet
    let anotherJackPair = "JJ" |> StringToCardSet
    Assert.False(jackPair |> CardSet.IsHigherThen(anotherJackPair))

[<Fact>]
let ``Trying to compare different set types throws`` () = 
    let jackPair = "JJ" |> StringToCardSet
    let twoTriple = "222" |> StringToCardSet
    Assert.Throws<System.Exception>(fun () -> jackPair |> CardSet.IsHigherThen(twoTriple) :> obj)