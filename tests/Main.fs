module tests
open Expecto
open HCRD.CLIArgs

[<EntryPoint>]
let main argv =
    Tests.runTestsInAssemblyWithCLIArgs [] [||]