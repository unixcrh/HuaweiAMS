@echo off
"%windir%"\microsoft.net\framework\v4.0.30319\msbuild.exe "%MCS2013ConfigDir%"\..\Framework\Framework.csproj

xcopy "%MCS2013ConfigDir%"\..\Bin .\Bin\ /Y /D /R

pause