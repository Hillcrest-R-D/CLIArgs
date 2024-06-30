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
  ]
