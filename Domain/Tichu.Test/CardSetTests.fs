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
    Assert.Equal("TTTT", TenQuadruple.Value |> CardSet.CardSetToString)

[<Fact>]
let ``CardSets of the same type are recognised`` () = 
    let jackTriple = "JJJ" |> CardSet.StringToCardSet
    let twoTriple = "222" |> CardSet.StringToCardSet
    Assert.True(jackTriple.Value |> CardSet.IsSameTypeAs(twoTriple.Value))

[<Fact>]
let ``CardSets of different types are told apart`` () = 
    let jackDouble = "JJ" |> CardSet.StringToCardSet
    let twoTriple = "222" |> CardSet.StringToCardSet
    Assert.False(jackDouble.Value |> CardSet.IsSameTypeAs(twoTriple.Value))

[<Fact>]
let ``Higher CardSet is recognised as higher, but not conversely`` () = 
    let kingTriple = "KKK" |> CardSet.StringToCardSet
    let tenTriple = "TTT" |> CardSet.StringToCardSet
    Assert.True(kingTriple.Value |> CardSet.IsHigherThen(tenTriple.Value))
    Assert.False(tenTriple.Value |> CardSet.IsHigherThen(kingTriple.Value))

[<Fact>]
let ``Equal height CardSets are not recognised as higher`` () =
    let jackPair = "JJ" |> CardSet.StringToCardSet
    let anotherJackPair = "JJ" |> CardSet.StringToCardSet
    Assert.False(jackPair.Value |> CardSet.IsHigherThen(anotherJackPair.Value))

[<Fact>]
let ``Trying to compare different set types throws`` () = 
    let jackPair = "JJ" |> CardSet.StringToCardSet
    let twoTriple = "222" |> CardSet.StringToCardSet
    Assert.Throws<System.Exception>(fun () -> jackPair.Value |> CardSet.IsHigherThen(twoTriple.Value) :> obj)

[<Fact>]
let ``Convert CardSet to Card list`` () = 
    let sixPair = "66" |> CardSet.StringToCardSet
    let sixPairList = sixPair.Value |> CardSet.CardSetToCardList
    Assert.Equal(2, sixPairList.Length)
    Assert.Equal('6', sixPairList[0].value)

[<Fact>]
let ``Detect valid set`` () = 
    let setstring = "66"
    Assert.True(setstring |> CardSet.IsValidSet)

[<Fact>]
let ``Detect invalid set`` () = 
    let setstring = "56"
    Assert.False(setstring |> CardSet.IsValidSet)

[<Fact>]
let ``Return None when trying to convert invalid set`` () = 
    let setstring = "56"
    Assert.Equal(None, setstring |> CardSet.StringToCardSet)