namespace FSharp.PropertyTesting

open System

type MyEntity = { Id: string; CreatedOn: DateTimeOffset }

type IRepository =
    abstract member List: unit -> MyEntity seq

type MyService(repository: IRepository) =
    member this.AllAfterYear(year: int): MyEntity seq = failwith "TODO"
