module UnitTests

open System
open FSharp.PropertyTesting
open Xunit

[<Fact>]
let ``Deterministic`` () =
    // Arrange
    let date1 = DateTimeOffset.Now.AddYears(-2)
    let date2 = DateTimeOffset.Now
    let data: MyEntity list = [
        { Id = "data1"; CreatedOn = date1 }
        { Id = "data2"; CreatedOn = date2 }
    ]
    let cutoff = DateTimeOffset.Now.AddYears(-1).Year
    let sut = MyServiceWorking({ new IRepository with member this.List() = data })

    // Act
    let result1 = sut.AllAfterYear(cutoff)
    let result2 = sut.AllAfterYear(cutoff)
    
    // Assert
    Assert.Equal<MyEntity>(result1, result2)

[<Fact>]
let ``Removes all dates past cutoff`` () =
    // Arrange
    let date1 = DateTimeOffset.Now.AddYears(-2)
    let date2 = DateTimeOffset.Now
    let data: MyEntity list = [
        { Id = "data1"; CreatedOn = date1 }
        { Id = "data2"; CreatedOn = date2 }
    ]
    let cutoff = DateTimeOffset.Now.AddYears(-1).Year
    let sut = MyServiceWorking({ new IRepository with member this.List() = data })

    // Act
    let result = sut.AllAfterYear(cutoff)
    
    // Assert
    let remaining = Assert.Single(result)
    Assert.Equal(date2, remaining.CreatedOn)
