@echo off

echo(
echo Creating version '%1'.
echo(
echo Please open FluentUriBuilder.nuspec and update it manually.
echo(
pause

echo Packing FluentUriBuilder v%1...
nuget pack FluentUriBuilder\FluentUriBuilder.csproj -Prop Configuration=Release

echo(
echo Press any key to publish FluentUriBuilder.%1.nupkg.
echo(
pause
nuget push FluentUriBuilder.%1.nupkg -source nuget.org