namespace Shed.Domain

open System

type FileType = Image | Video 
type FileData = {
    Extension: string;
    ThumbWidth: int;
    ThumbHeight: int;}

type Tag = { 
    Id:int; 
    Label:string; }

type Post = { 
    Id:int; 
    Title: Option<string>;
    FileType: FileType;
    FileData: FileData;
    CreationDate: DateTime;
    LastUpdateDate: DateTime;
    Tags: Option<Tag[]>;}

module Say =
    let hello name =
        printfn "Hello %s" name
