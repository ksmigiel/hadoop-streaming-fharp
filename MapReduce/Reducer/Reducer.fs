namespace MapReduce

open System
open System.IO

module Reducer =

    [<EntryPoint>]
    let main argv = 
        match argv.Length with
        | 1 -> Console.SetIn(new StreamReader(argv.[0]))
        | _ -> ()

        let currentWord = ref String.Empty
        let count = ref 0

        Seq.initInfinite (fun _ -> Console.ReadLine())
        |> Seq.takeWhile (fun line -> line <> null)
        |> Seq.iter (fun line ->
            let splitted = line.Split('\t')
            let word = (splitted.[0])

            match (word) with
            | word when word = !currentWord ->
                incr count
            | _ ->
                if !currentWord <> String.Empty then
                    Console.WriteLine("{0}\t{1}", !currentWord, !count)
                count := 1
                currentWord := word)
        |> ignore
        Console.WriteLine("{0}\t{1}", !currentWord, !count)

        0