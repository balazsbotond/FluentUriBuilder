@echo off

echo(
echo Creating version '%1'.
echo(
echo Please open FluentUriBuilder.nuspec and update it manually.
echo(
pause

"C:\Program Files (x86)\MSBuild\14.0\bin\msbuild.exe" FluentUriBuilder.sln /t:Build /p:Configuration=Release /p:TargetFramework=v2.0

echo Packing FluentUriBuilder v%1...
nuget pack FluentUriBuilder\FluentUriBuilder.csproj -Prop Configuration=Release

echo(
echo Press any key to publish FluentUriBuilder.%1.nupkg.
echo(
pause
nuget push FluentUriBuilder.%1.nupkg -source nuget.org