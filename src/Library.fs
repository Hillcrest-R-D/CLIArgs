namespace HCRD

module CLIArgs =
    open System.Text.RegularExpressions



    let private re = Regex("""(-{1,2}[\S]+)[ ]*([^\r\n\t\f\v "-]+|"[\S ]+"){0,1}""", RegexOptions.Compiled)

    
    let parseArgs (args : string []) =
        re.Matches(String.concat " " args)
        |> Seq.map 
            (fun mch ->
                mch.Groups
                |> Seq.toList
                |> function
                | _ :: g1 :: rest ->
                    Some (g1 |> _.Value,
                    rest 
                    |> function
                    | head :: _ when head.Value <> "" -> head |> _.Value |> Some
                    | _ -> None 
                    )
                | _ -> None
                // (groups |> (Seq.head << Seq.tail) |> _.Value, groups |> (Seq.head << Seq.tail << Seq.tail) |> _.Value) 
            ) 
        |> Seq.filter (Option.isSome) |> Seq.map (Option.get)
