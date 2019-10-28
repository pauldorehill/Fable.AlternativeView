open System.IO
open System.Diagnostics

let goToCurrentDir = "cd " + __SOURCE_DIRECTORY__ + ";"

let shellExecute args =
    let startInfo = ProcessStartInfo()
    startInfo.CreateNoWindow <- true
    startInfo.UseShellExecute <- false
    startInfo.FileName <- "powershell.exe"
    startInfo.Arguments <- goToCurrentDir + args
    startInfo.RedirectStandardOutput <- true
    let proc = Process.Start startInfo
    while not proc.StandardOutput.EndOfStream do
        printfn "%s" <| proc.StandardOutput.ReadLine()

    printfn "Complete."
    proc.WaitForExit()

// Build Fable project
printfn "Building Fable project"
shellExecute "dotnet build fable/src"

printfn "Installing NPM packages"
shellExecute "cd fable; npm install"

printfn "Running webpack"
shellExecute "cd fable; npm run-script build"

// Build Elm project

let elmjs = "elm.js"
let elmOptimizedjs = "elmOptimized.js"

printfn "Building unoptimized Elm"
shellExecute <| sprintf "cd elm; elm make src/Main.elm --output=public/%s" elmjs

printfn "Building optimized Elm"

shellExecute <| sprintf "cd elm; elm make src/Main.elm --output=public/%s" elmOptimizedjs

printfn "Compressing optimized Elm"

let uglify = sprintf "uglifyjs %s --compress pure_funcs=[F2,F3,F4,F5,F6,F7,F8,F9,A2,A3,A4,A5,A6,A7,A8,A9],pure_getters,keep_fargs=false,unsafe_comps,unsafe | uglifyjs --mangle --output=elm.min.js" elmOptimizedjs
shellExecute <| sprintf "cd elm/public; %s; rm %s" uglify elmOptimizedjs

let fablePath = Path.Combine(__SOURCE_DIRECTORY__ + "/fable/public/index.html") |> Path.GetFullPath
let elmPath = Path.Combine(__SOURCE_DIRECTORY__ + "/elm/public/index.html") |> Path.GetFullPath

printfn "Fable avalible at: %s" fablePath
printfn "Elm avalible at: %s" elmPath