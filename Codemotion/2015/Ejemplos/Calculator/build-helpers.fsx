module BuildHelpers

open Fake
open Fake.XamarinHelper
open System
open System.IO
open System.Linq

let Exec command args =
    let result = Shell.Exec(command, args)

    if result <> 0 then failwithf "%s exited with error %d" command result

let RestorePackages solutionFile =
    Exec "../../tools/NuGet/NuGet.exe" ("restore " + solutionFile)
    solutionFile |> RestoreComponents (fun defaults -> {defaults with ToolPath = "../../tools/xpkg/xamarin-component.exe" })

let RunNUnitTests dllPath xmlPath =
    Exec "../../tools/NUnit/nunit-console" (dllPath + " -xml=" + xmlPath) 
    TeamCityHelper.sendTeamCityNUnitImport xmlPath
	
let GetBuildCounter (str:Option<string>) =
    match str with
    | Some(v) -> v
    | None -> "Local"
