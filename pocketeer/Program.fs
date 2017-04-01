module Pocketeer.Program

open System
open System.IO
open System.Reflection

open Pocketeer.Pocket

let private run key token =
    let settings = { key = key; token = token }
    async {        
        let! data = fetchAll settings Container.empty
        printfn "%d items loaded" <| Seq.length data.items
        return 0
    }

let private executableName =
    let path = Assembly.GetExecutingAssembly().Location
    Path.GetFileName(path)

[<EntryPoint>]
let main = function
    | [| key; token |] -> Async.RunSynchronously <| run key token
    | _ -> printfn "Usage: %s <key> <token>" executableName; -1
