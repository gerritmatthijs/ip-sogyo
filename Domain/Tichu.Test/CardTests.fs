module CardTests

open Xunit
open Tichu

[<Fact>]
let CardCreation() =
    let jack = {value = 'J'}
    Assert.Equal('J', jack.value)

let CardIntValue() = 
    let jack = {value = 'J'}
    let two = {value = '2'}
    Assert.Equal(11, jack.intValue())
    Assert.Equal(2, two.intValue())