@echo off
REM
REM ����VS��װ·��
REM
call "C:\Program Files\Microsoft Visual Studio 10.0\VC\vcvarsall.bat" x86

set EnableNuGetPackageRestore=true
msbuild   kickstart.msbuild
msbuild   /p:configuration=release Report\Report.csproj
msbuild   /p:configuration=release ERPSystem.sln

Start ERPSystem_V4\bin\release
pause


 