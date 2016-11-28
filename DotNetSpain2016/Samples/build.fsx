// include Fake lib
#r @"tools/FAKE/tools/FakeLib.dll"
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

    RestorePackages "Calculator.sln"

    MSBuild "Calculator/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "Calculator.sln" ] |> ignore
)

Target "core-build" (fun () ->

    RestorePackages "Calculator.Core.sln"

    MSBuild "Calculator/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "Calculator.Core.sln" ] |> ignore
)

Target "core-tests" (fun () -> 
    RunNUnitTests "Calculator/bin/Debug/Calculator.Tests.dll" "Calculator/bin/Debug/testresults.xml" |> ignore
)

Target "android-build" (fun () ->
    RestorePackages "Calculator.Android.sln"

    MSBuild "Droid/bin/Debug" "Build" [ ("Configuration", "Debug") ] [ "Calculator.Android.sln" ] |> ignore
)

Target "android-package" (fun () ->
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = "Droid/Calculator.Android.csproj"
            Configuration = "Debug"
            OutputPath = "Droid/bin/Debug"
        }) 
    |> fun file -> TeamCityHelper.PublishArtifact file.FullName
)

Target "android-uitests" (fun () ->
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = "Droid/Calculator.Android.csproj"
            Configuration = "Debug"
            OutputPath = "Calculator.Android/bin/Debug"
        }) |> ignore

    let appPath = Directory.EnumerateFiles(Path.Combine("Droid", "bin", "Debug"), "*.apk", SearchOption.AllDirectories).First()

    RunUITests appPath
)

Target "android-deploy" (fun () ->

    let buildCounter = BuildHelpers.GetBuildCounter TeamCityHelper.TeamCityBuildNumber

    let hockeyAppApiToken = ""

    let appPath = Directory.EnumerateFiles(Path.Combine( "Calculator.Android", "bin", "Release"), "*.apk", SearchOption.AllDirectories).First()

    HockeyAppHelper.Upload hockeyAppApiToken appPath buildCounter
)

// *** Define Dependencies ***  
"core-build"
  ==> "core-tests"

"android-build"
  ==> "android-package"
  ==> "android-uitests"
  ==> "android-deploy"
  
// *** Start Build ***
RunTarget()
