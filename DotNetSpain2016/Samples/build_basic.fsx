// include Fake lib
#r @"tools/FAKE/tools/FakeLib.dll"

open Fake
open System
open System.IO
open System.Linq

// *** Define Targets ***  
Target "core-build" (fun () ->

    RestorePackages ...

    MSBuild ...
)

Target "core-tests" (fun () ->

    RunNUnitTests ...
)

// *** Define Dependencies ***  
"core-build"
  ==> "core-tests"
  
// *** Start Build ***
RunTarget()
