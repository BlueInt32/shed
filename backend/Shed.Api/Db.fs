module Db

open System
open System.Collections.Generic
open System.Data.SQLite
open Dapper
open Shed.Domain

type Person = {
  Id : int
  Name : string
  Age : int
  Email : string
}
let private peopleStorage = new Dictionary<int, Post>()
let getPeople () =
    let onePost = {
        Id = 1; 
        Title = Some "Billy-Joe"; 
        FileType = Image; 
        FileData = { Extension = ".jpg"; ThumbHeight = 200; ThumbWidth = 300;}
        CreationDate = DateTime.Now;
        LastUpdateDate = DateTime.Now;
        Tags = None; }
    peopleStorage.Add(1, onePost)
    peopleStorage.Values |> Seq.map (fun p -> p)

// Interacting with F# data

// http://www.codesuji.com/2017/07/29/F-and-Dapper/

// Initialize connectionstring
let databaseFilename = __SOURCE_DIRECTORY__ + @"..\shed.db"
let connectionStringFile = sprintf "Data Source=%s;Version=3;New=False;Compress=True;" databaseFilename  

// Create database
// SQLiteConnection.CreateFile(databaseFilename)

// Open connection
let addStuff () = 
    let connection = new SQLiteConnection(connectionStringFile)
    connection.Open()

    // Create table structure
    let structureSql =
        "create table Trades (" +
        "Symbol varchar(20), " +
        "Timestamp datetime, " + 
        "Price float, " + 
        "TradeSize float)"

    let structureCommand = new SQLiteCommand(structureSql, connection)
    structureCommand.ExecuteNonQuery() 


let getStuff () = 
    let filteredSql = 
        "select * From Posts " 
        // "where symbol = @symbol and tradesize >= @mintradesize"

    let results1 = 
        let connection = new SQLiteConnection(connectionStringFile)
        connection.Open()
        connection.Query<Post>(filteredSql)

    printfn "Query (1):"
    results1 
//|> Seq.iter (fun x -> 
//    printfn "%-7s %-19s %.2f [%.8f]" x.Symbol (x.Timestamp.ToString("s")) x.Price x.TradeSize)
