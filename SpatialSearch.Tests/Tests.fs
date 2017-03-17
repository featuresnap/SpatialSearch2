namespace SpatialSearch.Tests

module LibraryTests = 
    open Xunit
    open System
    open SpatialSearch.Library
    open FsUnit


    [<Fact>]
    let ``location created correctly`` () =
        let aLocation = location(1,2,3)
        let expected = {X=1; Y=2; Z=3}
        Assert.Equal(expected, aLocation)

    [<Theory>]
    [<InlineData(1,2)>]
    [<InlineData(2,1)>]
    let ``inOrder places items in order from lowest to highest`` (value1:int, value2:int) =
        let expectedOrder =  (Math.Min(value1, value2), Math.Max(value1, value2))
        let actualOrder = inOrder(value1, value2)
        Assert.Equal(expectedOrder, actualOrder)
    

    [<Fact>]
    let ``toBox creates box correctly out of 2 locations``() =
        let location1 = location(4,5,6)
        let location2 = location(1,2,3)
        let expected = 
            {
                minX = 1
                minY = 2
                minZ = 3
                maxX = 4
                maxY = 5
                maxZ = 6
            }
        let actual = toBox location1 location2
        Assert.Equal(expected, actual)
    
    [<Theory>]
    [<InlineData(1,1,1)>]
    [<InlineData(4,4,4)>]
    [<InlineData(4,1,1)>]
    [<InlineData(4,1,4)>]
    [<InlineData(2,1,2)>]
    [<InlineData(2,2,1)>]
    let ``bounding box does not enclose point outside it`` (x,y,z) =
        let boundingBox =  (location(2,2,2), location(3,3,3)) ||> toBox
        let location = location(x,y,z)
        Assert.False(boundingBox |> encloses location)
        


