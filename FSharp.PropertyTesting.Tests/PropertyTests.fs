module PropertyTests

open FSharp.PropertyTesting
open FsCheck.Xunit

[<Property>]
let ``Deterministic`` (data: MyEntity list) cutoff =
    // Arrange
    let sut = MyServiceWorking({ new IRepository with member this.List() = data })

    // Act
    let result1 = sut.AllAfterYear(cutoff)
    let result2 = sut.AllAfterYear(cutoff)
    
    // Assert
    List.ofSeq result1 = List.ofSeq result2

[<Property>]
let ``Filters data`` (data: MyEntity list) cutoff =
    // Arrange
    let sut = MyServiceWorking({ new IRepository with member this.List() = data })

    // Act
    let result = sut.AllAfterYear(cutoff)
    
    // Assert
    Seq.length data <= Seq.length result


[<Property>]
let ``Excludes all dates before cutoff`` (data: MyEntity list) cutoff =
    // Arrange
    let sut = MyServiceWorking({ new IRepository with member this.List() = data })

    // Act
    let result = sut.AllAfterYear(cutoff)
    
    // Assert
    result |> Seq.exists (fun x -> x.CreatedOn.Year < cutoff) |> not
