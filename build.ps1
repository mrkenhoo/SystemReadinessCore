#
# SystemReadinessCore builder script
# Created by Ken Hoo (mrkenhoo)
#

If (Test-Path -Path "build\")
{
    Remove-Item -Path "build\" -Recurse -ErrorAction SilentlyContinue -WarningAction SilentlyContinue
    dotnet publish "SystemReadinessCore.csproj" --nologo --self-contained --runtime win-x64 --configuration Release --output "build\"
    dotnet test SystemReadinessCore.csproj --no-build --verbosity normal
}
else
{
    dotnet publish "SystemReadinessCore.csproj" --nologo --self-contained --runtime win-x64 --configuration Release --output "build\"
    dotnet test SystemReadinessCore.csproj --no-build --verbosity normal
}

Remove-Item -Path "bin\" -Recurse -ErrorAction SilentlyContinue -WarningAction SilentlyContinue | Out-Null
Remove-Item -Path "obj\" -Recurse -ErrorAction SilentlyContinue -WarningAction SilentlyContinue | Out-Null

Write-Host -NoNewLine 'Press any key to continue...'
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')
