namespace MapReduce

open System
open System.IO
open FsUnit
open NUnit.Framework
open MapReduce

module TestsUtiliy =
    let path = Directory.GetCurrentDirectory()
    let pwd = new DirectoryInfo(path)
    let txtPath = pwd.Parent.Parent.FullName

[<TestFixture>]
module ``Mapper tests`` =
    open TestsUtiliy
    
    [<Test>]
    let ``Mapper should parse stream correctly`` () = 
        let mapper_input = Path.Combine([| txtPath; "mapper_input.txt" |])
        use sw = new StringWriter()
        Console.SetOut(sw)
        Mapper.main [| mapper_input |] |> ignore
        sw.ToString() |> should equal
            "testing\t1\r\n\
             stream\t1\r\n\
             stream\t1\r\n\
             should\t1\r\n\
             work\t1\r\n"

    [<Test>]
    [<ExpectedException>]
    let ``Mapper should throw exception when no arguments provided`` () =
        Mapper.main Array.empty<string>
        |> should throw typeof<System.Exception>

[<TestFixture>]
module ``Reducer tests`` =
    open TestsUtiliy
    
    [<Test>]
    let ``Reducer should parse stream correctly`` () = 
        let reducer_input = Path.Combine([| txtPath; "reducer_input.txt" |])
        use sw = new StringWriter()
        Console.SetOut(sw)
        Reducer.main [| reducer_input |] |> ignore
        sw.ToString() |> should equal
            "testing\t1\r\n\
             stream\t2\r\n\
             should\t1\r\n\
             work\t1\r\n"

    [<Test>]
    [<ExpectedException>]
    let ``Reducer should throw exception when no arguments provided`` () =
        Reducer.main Array.empty<string>
        |> should throw typeof<System.Exception>
