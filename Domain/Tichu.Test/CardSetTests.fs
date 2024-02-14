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
    Assert.Equal(FullHouse(Card.Card('3')), fullHouseOne)
    Assert.Equal(FullHouse(Card.Card('K')), fullHouseTwo)

[<Fact>]
let ``Create straight CardSet`` () = 
    let straight = "89TJQ" |> StringToCardSet
    Assert.Equal(Straight(Card.Card('8'), 5), straight)

[<Fact>]
let ``Create subsequent pairs CardSet`` () =
    let doublePair = "99TT" |> StringToCardSet
    let quadruplePair = "55667788" |> StringToCardSet
    Assert.Equal(SubsequentPairs(Card.Card('9'), 2), doublePair)
    Assert.Equal(SubsequentPairs(Card.Card('5'), 4), quadruplePair)

[<Fact>]
let ``Invalid sets yield a NonExistant type set`` () = 
    let invalidPair = "56" |> StringToCardSet
    let invalidFullHouse = "22225" |> StringToCardSet
    let invalidStraight = "5689T" |> StringToCardSet
    let invalidSubsequentPairsOne = "556688" |> StringToCardSet
    let invalidSubsequentPairsTwo = "566" |> StringToCardSet
    Assert.Equal(Invalid, invalidPair)
    Assert.Equal(Invalid, invalidFullHouse)
    Assert.Equal(Invalid, invalidStraight)
    Assert.Equal(Invalid, invalidSubsequentPairsOne)
    Assert.Equal(Invalid, invalidSubsequentPairsTwo)

[<Fact>]
let ``Multiples are recognised as the same type`` () = 
    let jackTriple = "JJJ" |> StringToCardSet
    let twoTriple = "222" |> StringToCardSet
    Assert.True(jackTriple |> CardSet.IsSameTypeAs(twoTriple))

let ``Multipiles of different length are told apart`` () =
    let jackDouble = "JJ" |> StringToCardSet
    let twoTriple = "222" |> StringToCardSet
    Assert.False(jackDouble |> CardSet.IsSameTypeAs(twoTriple))

[<Fact>]
let ``Full Houses are recognised as the same type`` () =
    let fullHouseOne = "22255" |> StringToCardSet
    let fullHouseTwo = "88TTT" |> StringToCardSet
    Assert.True(fullHouseTwo |> CardSet.IsSameTypeAs(fullHouseOne))

[<Fact>]
let ``Straights of the same length are recognised as the same type`` () = 
    let straightOne = "345678" |> StringToCardSet
    let straightTwo = "56789T" |> StringToCardSet
    Assert.True(straightOne |> CardSet.IsSameTypeAs(straightTwo))

[<Fact>]
let ``Straights of different length are told apart`` () =
    let straightOne = "34567" |> StringToCardSet
    let straightTwo = "56789T" |> StringToCardSet
    Assert.False(straightOne |> CardSet.IsSameTypeAs(straightTwo))

[<Fact>]
let ``Subsequent pairs of the same length are recognised as the same type`` () =
    let SubsequentPairsOne = "223344" |> StringToCardSet
    let SubsequentPairsTwo = "TTJJQQ" |> StringToCardSet
    Assert.True(SubsequentPairsOne |> CardSet.IsSameTypeAs(SubsequentPairsTwo))

[<Fact>]
let ``Subsequent pairs of different lengths are told apart`` () = 
    let subsequentPairsOne = "4455" |> StringToCardSet
    let subsequentPairsTwo = "778899" |> StringToCardSet
    Assert.False(subsequentPairsOne |> CardSet.IsSameTypeAs(subsequentPairsTwo))

[<Fact>]
let ``CardSets of different types are told apart`` () = 
    let twoTriple = "222" |> StringToCardSet
    let fullHouse = "222JJ" |> StringToCardSet
    let straight = "45678" |> StringToCardSet
    let subsequentPairs = "445566" |> StringToCardSet
    Assert.False(fullHouse |> CardSet.IsSameTypeAs(twoTriple))
    Assert.False(straight |> CardSet.IsSameTypeAs(fullHouse))
    Assert.False(subsequentPairs |> CardSet.IsSameTypeAs(straight))
    Assert.False(twoTriple |> CardSet.IsSameTypeAs(subsequentPairs))

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
let ``Higher straight is recognised`` () = 
    let straightLow = "345678" |> StringToCardSet
    let straightHigh = "56789T" |> StringToCardSet
    Assert.True(straightHigh |> CardSet.IsHigherThen(straightLow))
    Assert.False(straightLow |> CardSet.IsHigherThen(straightHigh))

[<Fact>]
let ``Higher subsequent pairs is recognised`` () =
    let subsequentPairsLow = "445566" |> StringToCardSet
    let subsequentPairsHigh = "TTJJQQ" |> StringToCardSet
    Assert.True(subsequentPairsHigh |> CardSet.IsHigherThen(subsequentPairsLow))
    Assert.False(subsequentPairsLow |> CardSet.IsHigherThen(subsequentPairsHigh))

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