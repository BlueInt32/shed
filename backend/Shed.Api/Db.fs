module Db

open System
open System.Collections.Generic
open System.Data.SQLite
open Shed.Domain
open System.Data

// https://github.com/mausch/FsSql  //
let databaseFilename = __SOURCE_DIRECTORY__ + @"\..\shed.db"
let connectionStringFile = sprintf "Data Source=%s;Version=3;New=False;Compress=True;" databaseFilename  

// type Person = { Name : string; Age : int }
let openConnection() = 
    let conn = new System.Data.SQLite.SQLiteConnection(connectionStringFile) 
    conn.Open() 
    conn :> IDbConnection 

let connMgr = Sql.withNewConnection openConnection 
let execScalar sql = Sql.execScalar connMgr sql 
let execReader sql = Sql.execReader connMgr sql 
let execReaderf sql = Sql.execReaderF connMgr sql 
let execNonQueryf sql = Sql.execNonQueryF connMgr sql 
let execNonQuery sql p = Sql.execNonQuery connMgr sql p |> ignore 
let exec sql = execNonQuery sql []

let getPostsNumber (): int64 = 
    // let filteredSql = "select * From Posts " 
    execScalar "select count(*) from posts" [] |> Option.get 

let getPosts () = 
    execReader "select * from posts" [] 
    |> Seq.ofDataReader
    |> Seq.map (fun dr -> 
    // |> Seq.iter (fun dr -> 
        let id = (dr?Id).Value 
        let title = (dr?Title).Value 
        let creationDate = 
            match dr?CreationDate with 
            | None -> DateTime.MinValue 
            | Some x -> DateTime.Parse(x) 
        // printfn "Id: ?; Name: %s; Address: %s" name creationDate)
        { Id=id; Title= Some title; CreationDate= creationDate; }
        )
