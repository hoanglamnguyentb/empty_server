Clear-Host
Write-Host "=== CLEAN ARCHITECTURE CODE GENERATOR ===`n"

# ----------------------------------------------------
# CONFIG
# ----------------------------------------------------
$basePath = $PSScriptRoot
$domainPath = Join-Path $basePath "../Domain/Entities"
$templatePath = Join-Path $basePath "templates"
$scribanPath = "C:\Libs\Scriban\scriban.dll"

# ----------------------------------------------
# Load Scriban
# ----------------------------------------------
if (!(Test-Path $scribanPath)) {
    Write-Host " ERROR: scriban.dll not found in CodeGen/"
    exit
}

Add-Type -Path $scribanPath

# ----------------------------------------------------
# SELECT ENTITY FILE
# ----------------------------------------------------
$files = Get-ChildItem $domainPath -Filter *.cs

if ($files.Count -eq 0) {
    Write-Host " No entities found in Domain/Entities"
    exit
}

Write-Host "`n Entities found:"
$index = 1
foreach ($f in $files) {
    Write-Host "$index) $($f.BaseName)"
    $index++
}

$choice = Read-Host "Enter number"

if ($choice -lt 1 -or $choice -gt $files.Count) {
    Write-Host " Invalid selection"
    exit
}

$selectedFile = $files[$choice - 1]
$entityName   = $selectedFile.BaseName

Write-Host "`n Selected: $entityName"

# ----------------------------------------------------
# PARSE PROPERTIES
# ----------------------------------------------------
$content = Get-Content $selectedFile.FullName -Raw
$regex = "public\s+([\w\?\<\>]+)\s+(\w+)\s*\{"
$matches = Select-String -InputObject $content -Pattern $regex -AllMatches

$properties = @()

foreach ($m in $matches.Matches) {
    $type = $m.Groups[1].Value
    $name = $m.Groups[2].Value
    if ($name -ne "Id") {
        $properties += [PSCustomObject]@{
            type = $type
            name = $name
        }
    }
}

Write-Host "`n Detected properties:"
$properties | ForEach-Object { Write-Host " - $($_.type) $($_.name)" }

# ----------------------------------------------------
# Helper: Render template
# ----------------------------------------------------
function RenderTemplate($file, $data) {
    $tmpl = [Scriban.Template]::Parse((Get-Content $file -Raw))
    return $tmpl.Render($data)
}

# ----------------------------------------------------
# OUTPUT PATHS
# ----------------------------------------------------
$paths = @{
    dto   = "../Application/Dtos"
    iface = "../Application/Interfaces"
    repo  = "../Infrastructure/Persistence/Repositories"
    serv  = "../Application/Services"
    ctrl  = "../WebApi/Controllers"
}

foreach ($p in $paths.Values) {
    $full = Join-Path $basePath $p
    if (!(Test-Path $full)) { New-Item -ItemType Directory -Path $full | Out-Null }
}

# ----------------------------------------------------
# GENERATE FILES
# ----------------------------------------------------
$data = @{
    name       = $entityName
    lower      = $entityName.Substring(0,1).ToLower() + $entityName.Substring(1)
    properties = $properties
}

function CreateFile($templateName, $outputDir, $outputName) {
    $templateFile = Join-Path $templatePath $templateName
    $outputPath   = Join-Path $outputDir $outputName

    $content = RenderTemplate $templateFile $data

    Set-Content -Path $outputPath -Value $content -Encoding UTF8

    Write-Host "Created: $outputPath"
}

# DTO
CreateFile "dto.sbn"   (Join-Path $basePath $paths.dto)   "$entityName`Dto.cs"

# Interface repository
CreateFile "interface_repo.sbn" (Join-Path $basePath $paths.iface) "I$entityName`Repository.cs"

# Repository
CreateFile "repository.sbn" (Join-Path $basePath $paths.repo) "$entityName`Repository.cs"

# Service
CreateFile "service.sbn" (Join-Path $basePath $paths.serv) "$entityName`Service.cs"

# Controller
CreateFile "controller.sbn" (Join-Path $basePath $paths.ctrl) "$entityName`Controller.cs"

Write-Host "`n DONE! Files generated for $entityName"
