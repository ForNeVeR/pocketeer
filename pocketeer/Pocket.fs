module internal Pocketeer.Pocket

open System

open PocketSharp
open PocketSharp.Models

type PocketSettings =
    { key : string
      token : string }

type Container = 
    { items : PocketItem seq
      lastUpdateDate : DateTime option }

    static member empty = { items = Array.empty; lastUpdateDate = None }

type private ImmediateContainer =
    { items : PocketItem[]
      lastUpdateDate : DateTime option }

let private itemLimit = 500

let private add (container : Container) data : Container =
    let items = Seq.append container.items data.items
    let lastUpdateDate = max container.lastUpdateDate data.lastUpdateDate
    { items = items
      lastUpdateDate = lastUpdateDate }

let private fetchNextBatch (client : PocketClient) offset : Async<ImmediateContainer> =
    async {
        let updateDateTime = DateTime.UtcNow
        printfn "Loading up to %d items…" itemLimit
        let! items = Async.AwaitTask <| client.Get(count = Nullable itemLimit, offset = Nullable offset)
        return { items = Seq.toArray items
                 lastUpdateDate = Some updateDateTime }
    }

let fetchAll (settings : PocketSettings) (container : Container) : Async<Container> =
    async {
        use client = new PocketClient(settings.key, settings.token)
        let fetch = fetchNextBatch client
        
        let mutable result = container
        let mutable count = 0
        let mutable proceed = true
        
        while proceed do
            let! data = fetch count
            let length = data.items.Length
            result <- add result data
            count <- count + length                 
            proceed <- length <> 0
            printfn "Rows loaded in this session: %d" count
            Array.tryLast data.items
            |> Option.map (fun (x : PocketItem) -> x.Title)
            |> Option.map (printfn "Last loaded item: %s")
            |> ignore

        return result
    }
