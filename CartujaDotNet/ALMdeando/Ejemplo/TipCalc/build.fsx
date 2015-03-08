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
    |> AndroidSignAndAlign (fun defaults ->
        {defaults with
            KeystorePath = "tipcalc.keystore"
            KeystorePassword = "tipcalc" 
            KeystoreAlias = "tipcalc"
        })
    |> fun file -> TeamCityHelper.PublishArtifact file.FullName
)

Target "android-deploy" (fun () ->
    let buildCounter = BuildHelpers.GetBuildCounter TeamCityHelper.TeamCityBuildNumber

    let hockeyAppApiToken = Environment.GetEnvironmentVariable("HockeyAppApiToken")

    let appPath = Directory.EnumerateFiles(Path.Combine( "TipCalc.Android", "bin", "Debug"), "*.apk", SearchOption.AllDirectories).First()

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
