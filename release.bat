@echo off
REM
REM ����VS��װ·��
REM
call "C:\Program Files\Microsoft Visual Studio 10.0\VC\vcvarsall.bat" x86

set EnableNuGetPackageRestore=true
msbuild   kickstart.msbuild
msbuild   /p:configuration=release Semitron_OMS.sln

Start Semitron_OMS.UI\bin\release
pause


 