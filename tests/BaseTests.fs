module Tests

open HCRD.CLIArgs
open Expecto

[<Tests>]
let tests =
  testList "argTests" [
    testCase "flag parse" <| fun _ ->
        [|"--parse"; "this"|]
        |> parseArgs
        |> Seq.head
        |> fun x -> Expect.equal x ("parse", Some "this") "Simple flag does not parse"
    testCase "switch parse" <| fun _ ->
        [|"-b"|]
        |> parseArgs
        |> Seq.head
        |> fun x -> Expect.equal x ("b", None) "Simple switch does not parse"
    testCase "switch as flag" <| fun _ ->
        //using switch as a flag shorthand
        [|"-p"; "this"|]
        |> parseArgs
        |> Seq.head
        |> fun x -> Expect.equal x ("p", Some "this") "Switch with text does not parse"
    testCase "extra text is filtered" <| fun _ ->
        [|"--flag"; "the actual arguments"; "some more text"|]
        |> parseArgs
        |> fun x -> Expect.equal (Seq.length x) 1 "Extra text is not filtered"; x
        |> Seq.head
        |> fun x -> Expect.equal x ("flag", Some "the actual arguments") "Extra text flag is not parsed properly"
    testCase "parsedArgs are typified" <| fun _ ->
        [|"--flag"; "the actual arguments"; "-b"; "--second"; "two"|]
        |> parseArgs
        |> typify
        |> fun x -> 
            [|("flag", Some "the actual arguments"); ("b", None); ("second", Some "two")|]
            |> dict
            |> fun manual -> 
                let keys1 = x.Keys
                let keys2 = manual.Keys
                Seq.append keys1 keys2
                |> set
                //presumably this will throw an exn when the two seq<Key>s arent equal, equivalent to failure.
                |> fun s -> Expect.all s (fun key -> x[key] = manual[key]) "Typifications not equal"
    testCase "parsedArgsAndTypication composition works" <| fun _ ->
        [|"--flag"; "the actual arguments"; "-b"; "--second"; "two"|]
        |> parseArgsAndTypify
        |> fun x -> 
            [|("flag", Some "the actual arguments"); ("b", None); ("second", Some "two")|]
            |> dict
            |> fun manual -> 
                let keys1 = x.Keys
                let keys2 = manual.Keys
                Seq.append keys1 keys2
                |> set
                //presumably this will throw an exn when the two seq<Key>s arent equal, equivalent to failure.
                |> fun s -> Expect.all s (fun key -> x[key] = manual[key]) "Typifications not equal"
  ]
