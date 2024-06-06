namespace FSharp.PropertyTesting

type MyServiceWorking(repository: IRepository) =
    member this.AllAfterYear(year: int): MyEntity seq =
        repository.List() |> Seq.filter(fun x -> x.CreatedOn.Year > year)
