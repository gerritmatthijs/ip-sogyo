module CardTests

open Xunit
open Tichu

[<Fact>]
let ``Card creation`` () =
    let jack = Card.Card('J')
    Assert.Equal('J', jack.value)

[<Fact>]
let ``Card has correct integer Value`` () = 
    let jack = Card.Card('J')
    let two = Card.Card('2')
    Assert.Equal(11, jack.IntValue())
    Assert.Equal(2, two.IntValue())