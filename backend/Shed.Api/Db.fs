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
        Tags = None;
    }
    peopleStorage.Add(1, onePost)
    peopleStorage.Values |> Seq.map (fun p -> p)

type TradeData = { 
    Symbol:string; 
    Timestamp:DateTime; 
    Price:float;
    TradeSize:float }

// Sample Data
let trades = [
    { Symbol = "BTC/USD"; Timestamp = new DateTime(2017, 07, 28, 10, 44, 33); Price = 2751.20; TradeSize = 0.01000000 };
    { Symbol = "BTC/USD"; Timestamp = new DateTime(2017, 07, 28, 10, 44, 21); Price = 2750.20; TradeSize = 0.01000000 };
    { Symbol = "BTC/USD"; Timestamp = new DateTime(2017, 07, 28, 10, 44, 21); Price = 2750.01; TradeSize = 0.40000000 };
    { Symbol = "BTC/USD"; Timestamp = new DateTime(2017, 07, 28, 10, 44, 21); Price = 2750.01; TradeSize = 0.55898959 };
    { Symbol = "BTC/USD"; Timestamp = new DateTime(2017, 07, 28, 10, 44, 03); Price = 2750.00; TradeSize = 0.86260000 };
    { Symbol = "BTC/USD"; Timestamp = new DateTime(2017, 07, 28, 10, 44, 03); Price = 2750.00; TradeSize = 0.03000000 };
    { Symbol = "BTC/USD"; Timestamp = new DateTime(2017, 07, 28, 10, 43, 31); Price = 2750.01; TradeSize = 0.44120000 } 
    ]

// Initialize connectionstring
let databaseFilename = "shed.db"
let connectionStringFile = sprintf "Data Source=%s;Version=3;" databaseFilename  

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
