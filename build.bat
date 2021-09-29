rmdir /s /q build

dotnet publish SharpEngine/SharpEngine.csproj -c Release -r win-x64 -o build/SharpEngine
dotnet publish SharpEngineTest/SharpEngineTest.csproj -c Release -r win-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true -o build/SharpEngineTest