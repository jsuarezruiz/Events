// include Fake lib
#r @"FAKE.4.9.1/tools/FakeLib.dll"
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

    RestorePackages "../../Calculator.sln"

    MSBuild "../../src/Calculator/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "../../Calculator.sln" ] |> ignore
)

Target "core-build" (fun () ->

    RestorePackages "../../Calculator.Core.sln"

    MSBuild "../../src/Calculator/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "../../Calculator.Core.sln" ] |> ignore
)

Target "core-tests" (fun () -> 
    RunNUnitTests "../../src/Calculator/bin/Debug/Calculator.Tests.dll" "../../src/Calculator/bin/Debug/testresults.xml" |> ignore
)

Target "windows-phone-build" (fun () ->
    RestorePackages "../../Calculator.WindowsPhone.sln"

    MSBuild "../../src/Calculator.WindowsPhone/Calculator.WindowsPhone/bin/Debug" "Build" [ ("Configuration", "Debug") ] [ "../../Calculator.WindowsPhone.sln" ] |> ignore
)

Target "ios-build" (fun () ->
    RestorePackages "../../Calculator.iOS.sln"

    MSBuild "../../src/Calculator.iOS/bin/Debug" "Build" [ ("Configuration", "Debug") ] [ "../../Calculator.iOS.sln" ] |> ignore
)

Target "android-build" (fun () ->
    RestorePackages "../Calculator.Android.sln"

    MSBuild "../src/Calculator.Android/bin/Debug" "Build" [ ("Configuration", "Debug") ] [ "../Calculator.Android.sln" ] |> ignore
)

Target "android-package" (fun () ->
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = "../../src/Calculator.Android/Calculator.Android.csproj"
            Configuration = "Debug"
            OutputPath = "../../src/Calculator.Android/bin/Debug"
        }) 
    |> fun file -> TeamCityHelper.PublishArtifact file.FullName
)

// *** Define Dependencies ***
"core-build"
  ==> "core-tests"

"android-build"
  ==> "android-package"
  
// *** Start Build ***
RunTarget()
