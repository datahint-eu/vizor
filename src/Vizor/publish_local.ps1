# usage:
#   powershell .\publish_nuget.ps1
#   powershell .\publish_nuget.ps1 -Push:1

param (
    [string]$Folder = "D:\\Dev\\NugetLocal",
    [string]$BuildConfig = "Release"
)

[xml]$xmlElem = Get-Content -Path Vizor.csproj
$version = ($xmlElem.Project.PropertyGroup.Version).Trim()

Write-Host -ForegroundColor Green "Building Vizor version $version"
&dotnet "pack" "-p:PackageVersion=$version" "Vizor.csproj" "-c:$BuildConfig"

Write-Host -ForegroundColor Green "Pushing Vizor version $version to $Folder"
&nuget "add" "bin/$BuildConfig/Vizor.$version.nupkg" "-Source" "$Folder"
