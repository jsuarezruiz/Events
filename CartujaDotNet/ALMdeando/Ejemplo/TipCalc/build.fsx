// include Fake lib
#r @"packages/FAKE.3.5.4/tools/FakeLib.dll"
#load "build-helpers.fsx"
#load "hockey-app-helpers.fsx"

open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper
open HockeyAppHelper

// *** Define Targets ***
Target "common-build" (fun () ->

    RestorePackages "TipCalc.sln"

    MSBuild "src/TipCalc/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "TipCalc.sln" ] |> ignore
)

Target "core-build" (fun () ->

    RestorePackages "TipCalc.Core.sln"

    MSBuild "src/TipCalc/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "TipCalc.Core.sln" ] |> ignore
)

Target "core-tests" (fun () -> 
    RunNUnitTests "src/TipCalc/bin/Debug/TipCalc.Tests.dll" "src/TipCalc/bin/Debug/testresults.xml" |> ignore
)

Target "windows-phone-build" (fun () ->
    RestorePackages "TipCalc.WindowsPhone.sln"

    MSBuild "src/TipCalc.WindowsPhone/TipCalc.WindowsPhone/bin/Debug" "Build" [ ("Configuration", "Debug") ] [ "TipCalc.WindowsPhone.sln" ] |> ignore
)

Target "android-build" (fun () ->
    RestorePackages "TipCalc.Android.sln"

    MSBuild "src/TipCalc.Android/bin/Debug" "Build" [ ("Configuration", "Debug") ] [ "TipCalc.Android.sln" ] |> ignore
)

Target "android-package" (fun () ->
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = "src/TipCalc.Android/TipCalc.Android.csproj"
            Configuration = "Debug"
            OutputPath = "src/TipCalc.Android/bin/Debug"
        }) 
    |> fun file -> TeamCityHelper.PublishArtifact file.FullName
)

Target "android-deploy" (fun () ->

    Environment.SetEnvironmentVariable("HockeyAppApiToken", "780b27c279354896b9734b03a9019c97")

    let buildCounter = BuildHelpers.GetBuildCounter TeamCityHelper.TeamCityBuildNumber

    let hockeyAppApiToken = Environment.GetEnvironmentVariable("HockeyAppApiToken")

    let appPath = "src/TipCalc.Android/bin/Debug/TipCalc.Android.apk" // Directory.EnumerateFiles

    HockeyAppHelper.Upload hockeyAppApiToken appPath buildCounter
)

// *** Define Dependencies ***
"core-build"
  ==> "core-tests"

"android-build"
  ==> "android-package"
  
"android-build"
  ==> "android-deploy"
  
// *** Start Build ***
RunTarget()
