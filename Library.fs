namespace HCRD

module CLIArgs =
    open System.Text.RegularExpressions



    let private re = Regex("""(-{1,2}[\S]+)[ ]*([^\r\n\t\f\v "-]+|"[\S ]+"){0,1}""", RegexOptions.Compiled)

    let parseArgs (args : string []) =
        re.Matches(String.concat " " args)
        |> Seq.map 
            (fun mch ->
            //expect one XOR two groups, never more:
                let groups = mch.Groups 
                (Seq.head groups, Seq.head <| Seq.tail groups) 
            ) 
