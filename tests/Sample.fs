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
        |> fun x -> Expect.equal x ("--parse", Some "this") "Simple flags parses"
    testCase "switch parse" <| fun _ ->
        [|"-b"|]
        |> parseArgs
        |> Seq.head
        |> fun x -> Expect.equal x ("-b", None) "Simple switch parses"
    testCase "switch as flag" <| fun _ ->
        //using switch as a flag shorthand
        [|"-p"; "this"|]
        |> parseArgs
        |> Seq.head
        |> fun x -> Expect.equal x ("-p", Some "this") "Simple switch parses"
    testCase "extra text is filtered" <| fun _ ->
        [|"--flag"; """ "the actual arguments" """; "some more text"|]
        |> parseArgs
        |> Seq.head
        |> fun x -> Expect.equal x ("--flag", Some "\"the actual arguments\"") ""
  ]
