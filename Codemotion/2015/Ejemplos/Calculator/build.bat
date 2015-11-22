@echo off
.nuget\NuGet.exe install FAKE -Version 4.9.1
packages\FAKE.4.9.1\tools\FAKE.exe build.fsx %1 