namespace SpatialSearch

module Library = 
    type BoundingBox<'a> = 
        { minX : 'a
          minY : 'a
          minZ : 'a
          maxX : 'a
          maxY : 'a
          maxZ : 'a }
    
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
    
    type Node<'a> = 
        | Leaf of Location<'a>
        | Parent of BoundingBox<'a> * Node<'a> list
    
    let inline encloses location box = 
        location.X >= box.minX && location.X <= box.maxX && location.Y >= box.minY && location.Y <= box.maxY 
        && location.Z >= box.minZ && location.Z <= box.maxZ