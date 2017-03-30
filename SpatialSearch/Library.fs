namespace SpatialSearch

module Library = 
    
    [<Struct>]
    type BoundingBox<'a> = 
        { minX : 'a
          minY : 'a
          minZ : 'a
          maxX : 'a
          maxY : 'a
          maxZ : 'a }
    
    [<Struct>]
    type Location<'a> = 
        { X : 'a
          Y : 'a
          Z : 'a }
    
    let location (x, y, z) = 
        { X = x
          Y = y
          Z = z }
    
    let inOrder (a, b) = 
        if b > a then (a, b)
        else (b, a)

    let toBox loc1 loc2 = 
        
        let x1, x2 = inOrder (loc1.X, loc2.X)
        let y1, y2 = inOrder (loc1.Y, loc2.Y)
        let z1, z2 = inOrder (loc1.Z, loc2.Z)
        { minX = x1
          maxX = x2
          minY = y1
          maxY = y2
          minZ = z1
          maxZ = z2 }

    let inline encloses location box = 
        location.X >= box.minX && 
        location.X <= box.maxX && 
        location.Y >= box.minY && 
        location.Y <= box.maxY && 
        location.Z >= box.minZ && 
        location.Z <= box.maxZ
           
    type Pt = float
    type Location = Pt * Pt
            
    type Bounds = (Location) * (Location)

    type QuadNode = 
        |Leaf of Bounds
        |Node of  (Pt * Pt) * Bounds * Children
    and Children = {
            NE: QuadNode
            NW: QuadNode
            SE: QuadNode
            SW: QuadNode }
     
    let empty () : Bounds = 
        let min = (Pt.MinValue, Pt.MinValue)
        let max = (Pt.MaxValue, Pt.MaxValue)
        (min, max) 

    let inline encloses location (minBound, maxBound) =
        location >= minBound &&
        location < maxBound

    let findEnclosingChild location children = 
        match location with 
        |

    let addLocation (location: Pt * Pt) (node:QuadNode) = 
        let locX, locY = location
        match node with 
        |Leaf (bounds:Bounds) ->
            let minBound, maxBound = bounds
            let children = {
               NE=Leaf((Value locX, Value locY), (fst maxBound, snd maxBound))
               SE=Leaf((Value locX, Value locY), (fst maxBound, snd minBound))
               NW=Leaf((fst minBound, snd maxBound), (Value locX, Value locY))
               SW=Leaf((fst minBound, snd minBound), (Value locX, Value locY)) }
            Node (location, bounds, children)
        |Node (origin, bounds, children) ->
            let enclosingChild = children |> findEnclosingChild location
