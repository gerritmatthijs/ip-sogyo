module CardSetTests

open Xunit
open Tichu

[<Fact>]
let ``CardSet creation`` () =
    let jackPair = {card = {value = 'J'}; number = 2}
    Assert.Equal('J', jackPair.card.value)
    Assert.Equal(2, jackPair.number)

[<Fact>]
let ``Transform CardSet to string`` () = 
    let jackTriple = {card = {value = 'J'}; number = 3}
    let setstring = jackTriple |> CardSet.CardSetToString
    Assert.Equal("JJJ", setstring)

[<Fact>]
let ``Transform string to CardSet`` () = 
    let setstring = "TTTT"
    let TenQuadruple = setstring |> CardSet.StringToCardSet
    Assert.Equal("TTTT", TenQuadruple |> CardSet.CardSetToString)

[<Fact>]
let ``CardSets of the same type are recognised`` () = 
    let jackTriple = "JJJ" |> CardSet.StringToCardSet
    let twoTriple = "222" |> CardSet.StringToCardSet
    Assert.True(jackTriple |> CardSet.IsSameTypeAs(twoTriple))

[<Fact>]
let ``CardSets of different types are told apart`` () = 
    let jackDouble = "JJ" |> CardSet.StringToCardSet
    let twoTriple = "222" |> CardSet.StringToCardSet
    Assert.False(jackDouble |> CardSet.IsSameTypeAs(twoTriple))

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