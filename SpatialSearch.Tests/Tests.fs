

module SpatialSearch.Tests
open Xunit
open System
open SpatialSearch
open SpatialSearch.Library

[<Fact>]
let ``location created correctly`` () =
    let aLocation = location(1,2,3)
    let expected = {X=1; Y=2; Z=3}
    Assert.Equal(expected, aLocation)

[<Theory>]
[<InlineData(1,2)>]
[<InlineData(2,1)>]
let ``inOrder places items in order from lowest to highest`` (value1:int, value2:int) =
    let highest =  Math.Max(value1, value2)
    let lowest = Math.Min(value1, value2)
    let ordered = inOrder(value1, value2)
    Assert.Equal((lowest, highest), ordered)
    