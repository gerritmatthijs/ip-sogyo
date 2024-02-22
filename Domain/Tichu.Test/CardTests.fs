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
    Assert.Equal(11., jack.NumericValue())
    Assert.Equal(2., two.NumericValue())
    Assert.Equal(100., dragon.NumericValue())

[<Fact>]
let ``Creating nonexistant card throws`` () =
    Assert.Throws<System.Exception>(fun () -> Card.Card('Y') :> obj)