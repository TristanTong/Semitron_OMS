@echo off
REM
REM 假设VS安装路径
REM
call "C:\Program Files\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x86

set EnableNuGetPackageRestore=true
msbuild   kickstart.msbuild
msbuild   /p:configuration=release Semitron_OMS.sln

Start Semitron_OMS.UI\bin\release
pause


 