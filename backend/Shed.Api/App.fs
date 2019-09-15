open Suave.Web
open Suave
open Suave.Filters
open Suave.Successful
open Suave.Operators
open Suave.RequestErrors
open Suave.Json
open System.Runtime.Serialization
open Shed.Api.Rest
open Shed.Domain

let browse =
    request (fun r ->
        match r.queryParam "genre" with
        | Choice1Of2 genre -> OK (sprintf "Genre: %s" genre)
        | Choice2Of2 msg -> BAD_REQUEST msg)

let createPost = (mapJson (fun (a:Foo) -> { bar = a.foo }))

let webPart = 
    choose [
        path "/" >=> (OK "Home")
        path "/posts" >=> choose [
            GET >=> warbler (fun _ -> Db.getPosts () |> JSON)
            POST >=> createPost
        ]
        pathScan "/posts/%d" 
            (fun id -> OK (sprintf "Post details: %d" id))
        path "/store/browse" >=> browse
    ]

startWebServer defaultConfig webPart
