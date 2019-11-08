open System.IO
open System.Diagnostics

let goToCurrentDir = "cd " + __SOURCE_DIRECTORY__ + ";"

let shellExecute args =
    let startInfo = ProcessStartInfo()
    startInfo.CreateNoWindow <- true
    startInfo.UseShellExecute <- false
    startInfo.FileName <- "pwsh.exe"
    startInfo.Arguments <- goToCurrentDir + args
    startInfo.RedirectStandardOutput <- true
    let proc = Process.Start startInfo
    while not proc.StandardOutput.EndOfStream do
        printfn "%s" <| proc.StandardOutput.ReadLine()

    printfn "Complete."
    proc.WaitForExit()

let sep() = printfn "-----------------------------------------------------------------------------------"
let newLine() = printfn "%s" System.Environment.NewLine

let buildProject path =
    newLine()
    sep()
    printfn "Building project at: %s" path
    sep()
    newLine()

    printfn "Building Fable project"
    shellExecute <| sprintf "dotnet build %s/src" path

    printfn "Installing NPM packages"
    shellExecute <| sprintf "cd %s; npm install" path

    printfn "Running webpack"
    shellExecute <| sprintf "cd %s; npm run-script build" path

    sep()
    newLine()

let infernoPath = "src/inferno"
let preactPath = "src/preact"
let elmPath = "src/elm"

// Build Inferno project
buildProject infernoPath

// Build Preact project
buildProject preactPath

// Build Elm project
newLine()
sep()
printfn "Building Elm project"
sep()
newLine()

let elmjs = "elm.js"
let elmMin = "elm.min.js"

printfn "Building unoptimized Elm"
shellExecute <| sprintf "cd %s; elm make src/Main.elm --output=public/%s" elmPath elmjs

printfn "Building optimized Elm"
shellExecute <| sprintf "cd %s; elm make src/Main.elm  --optimize --output=public/%s" elmPath elmMin

printfn "Compressing optimized Elm"

let uglify = sprintf "uglifyjs %s --compress 'pure_funcs=[F2,F3,F4,F5,F6,F7,F8,F9,A2,A3,A4,A5,A6,A7,A8,A9],pure_getters,keep_fargs=false,unsafe_comps,unsafe' --output=%s; uglifyjs %s --mangle --output=elm.min.js" elmMin elmMin elmMin
shellExecute <| sprintf "cd %s/public; %s" elmPath uglify
// 'pure_funcs="F2,F3,F4,F5,F6,F7,F8,F9,A2,A3,A4,A5,A6,A7,A8,A9",pure_getters,keep_fargs=false,unsafe_comps,unsafe'
sep()
newLine()

let getFullPath relPath =
    let index = sprintf "/%s/public/index.html" relPath
    Path.Combine(__SOURCE_DIRECTORY__ + index)
    |> Path.GetFullPath

printfn "Inferno avalible at: %s" (getFullPath infernoPath)
printfn "Preact avalible at: %s" (getFullPath preactPath)
printfn "Elm avalible at: %s" (getFullPath elmPath)