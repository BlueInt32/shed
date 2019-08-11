namespace Shed.Api.Rest

open Newtonsoft.Json
open Newtonsoft.Json.Serialization
open Suave
open Suave.Operators
open Suave.Http
open Suave.Successful

[<AutoOpen>]
module RestFul =
    type RestResource<'a> = {
    GetAll : unit -> 'a seq
    }


    // 'a -> WebPart
    let JSON v =
        let jsonSerializerSettings = new JsonSerializerSettings()
        jsonSerializerSettings.ContractResolver <- new CamelCasePropertyNamesContractResolver()

        JsonConvert.SerializeObject(v, jsonSerializerSettings)
        |> OK
        >=> Writers.setMimeType "application/json; charset=utf-8"


    //let rest resourceName resource =
    //    let resourcePath = "/" + resourceName
    //    let gellAll = resource.GetAll () |> JSON
    //    path resourcePath >=> GET >=> getAll
