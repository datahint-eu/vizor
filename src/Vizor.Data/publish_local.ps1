# usage:
#   powershell .\publish_nuget.ps1
#   powershell .\publish_nuget.ps1 -Push:1

param (
    [string]$Folder = "D:\\Dev\\NugetLocal",
    [string]$BuildConfig = "Release",
    [Boolean]$Push = $false
)

[xml]$xmlElem = Get-Content -Path Vizor.Data.csproj
$version = ($xmlElem.Project.PropertyGroup.Version).Trim()

Write-Host -ForegroundColor Green "Building Vizor.Data version $version"
&dotnet "pack" "-p:PackageVersion=$version" "Vizor.Data.csproj" "-c:$BuildConfig"

if ($Push) {
	Write-Host -ForegroundColor Green "Pushing Vizor.Data version $version to $Folder"
	&nuget "add" "bin/$BuildConfig/Vizor.Data.$version.nupkg" "-Source" "$Folder"
}

