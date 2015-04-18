open System
open System.IO
open System.Drawing

let outExtPrefix  = ".out"

let Cutout  (fullName :string) (sizeX :int) (sizeY :int)=
    let ext = Path.GetExtension fullName
    let newFileName = Path.ChangeExtension(fullName, outExtPrefix  + ext)
    use bitmap =  new Bitmap( new Bitmap(fullName),  sizeX , sizeY)
    bitmap.Save(newFileName)
    printfn "New file path %s" newFileName


[<EntryPoint>]
let main args = 
    printfn "Arguments passed to function : %A" args
    if args.Length > 0 then 
        let path = args.[0]
        if args.Length > 3 then
            let fileName = args.[1]
            let sizeX = args.[2] |> int
            let sizeY = args.[3] |> int
            let files = Directory.GetFiles(path,fileName, SearchOption.AllDirectories) |> Seq.filter (fun x ->  x.Contains(outExtPrefix) |> not)
            files |> Seq.iter (fun x -> Cutout x sizeX sizeY)
        else if args.Length > 1 && args.[1].ToLower() = "clear" then
            Directory.GetFiles(path,"*", SearchOption.AllDirectories) 
                |> Seq.filter (fun x ->  x.Contains(outExtPrefix)) 
                |> Seq.iter File.Delete
                |> ignore

    Console.ReadKey() |> ignore
    0

