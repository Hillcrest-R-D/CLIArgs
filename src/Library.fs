namespace HCRD

module CLIArgs =
    open System.Text.RegularExpressions

    let private re = Regex("""-{1,2}([\d\w]+)[|]{0,1}([^|-]+){0,1}[|]{0,1}""", RegexOptions.Compiled)

    ///<summary>Takes the</summary>
    let parseArgs (args : string []) =
        args
        |> fun args -> re.Matches(String.concat "|" args)
        |> Seq.map 
            (fun mch ->
                mch.Groups
                |> Seq.toList
                |> function
                | a :: g1 :: rest -> //like ("--someFlag someArgValue", "--someFlag", "someArgValue")
                    Some (g1 |> _.Value,
                    rest 
                    |> function
                    | head :: _ when head.Value <> "" -> head |> _.Value |> Some
                    | _ -> None 
                    )
                | _ -> None
            ) 
        |> Seq.filter (Option.isSome) |> Seq.map (Option.get)

    let typify (parsedArgs : seq<string * option<string>>) =
        parsedArgs
        |> dict

    let parseArgsAndTypify =
        parseArgs >> typify