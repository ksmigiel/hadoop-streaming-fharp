namespace MapReduce

open System
open System.IO

module Mapper =

    [<EntryPoint>]
    let main argv = 
        let chars = [| ' '; '.'; ','; '!'; ';'; '?'; '|'; '-'; '{'; '}'; ':'; '('; ')' |]

        match argv.Length with
        | 1 -> Console.SetIn(new StreamReader(argv.[0]))
        | _ -> ()

        let isWord w =
            let n = ref 0
            not (Int32.TryParse(w, n))

        let output (word:string) =
            Console.WriteLine("{0}\t{1}", word.Trim(), 1)

        Seq.initInfinite (fun _ -> Console.ReadLine())
        |> Seq.takeWhile (fun line -> line <> null)
        |> Seq.iter (fun (line : string) -> 
            line.ToLower().Split(chars, StringSplitOptions.RemoveEmptyEntries)
            |> Seq.filter isWord
            |> Seq.iter output )

        0
