namespace Shed.Domain

open System
open System.Runtime.Serialization

type FileType = Image | Video 
type FileData = {
    Extension: string;
    ThumbWidth: int;
    ThumbHeight: int;}

type Tag = { 
    Id:int; 
    Label:string; }

type Post = { 
    Id:int64; 
    Title: Option<string>;
    //FileType: FileType;
    //FileData: FileData;
    CreationDate: DateTime;
    //LastUpdateDate: DateTime;
    //Tags: Option<Tag[]>;
    }

module Say =
    let hello name =
        printfn "Hello %s" name


/// Dummy types for prototyping
[<DataContract>]
type Foo =
  { [<field: DataMember(Name = "foo")>]
    foo : string }

[<DataContract>]
type Bar =
  { [<field: DataMember(Name = "bar")>]
    bar : string }
