param([string[]]$names)

# Load config
$config = Get-Content "./config.json" | ConvertFrom-Json

foreach ($rawName in $names) {

    $name = ($rawName.Substring(0,1).ToUpper() + $rawName.Substring(1))

    Write-Host "🔧 Generating for entity: $name"

    $idType = $config.defaultIdType

    $mapping = @{
        "dto"         = "dto.scriban"
        "irepository" = "irepository.scriban"
        "repository"  = "repository.scriban"
        "iservice"    = "iservice.scriban"
        "service"     = "service.scriban"
        "controller"  = "controller.scriban"
    }

    foreach ($key in $mapping.Keys) {

        $templatePath = "./templates/$($mapping[$key])"
        $outputFolder = $config.outputPaths.$key
        $outputPath   = "$outputFolder/$name$($key -replace 'i','').cs"

        $content = Get-Content $templatePath | Out-String
        $content = $content.Replace("{{ name }}", $name)
        $content = $content.Replace("{{ idType }}", $idType)

        # Ensure directory exists
        if (!(Test-Path $outputFolder)) {
            New-Item -ItemType Directory -Path $outputFolder | Out-Null
        }

        Set-Content -Path $outputPath -Value $content -Encoding UTF8

        Write-Host "✅ $key generated: $outputPath"
    }
}

Write-Host "🎉 Done!"
