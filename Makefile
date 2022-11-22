all:
	@dotnet clean "SystemReadinessCore.csproj"
	@dotnet publish "SystemReadinessCore.csproj" --nologo --self-contained --runtime win-x64 --configuration Release --output "build/"
	@dotnet test SystemReadinessCore.csproj --no-build --verbosity normal
