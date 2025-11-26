param(
    [string]$SourceDir = "D:\1-Razvoj\Leg\SaobracajMTA\Saobracaj\bin\Release",
    [string]$AppName = "Leget",
    [string]$AppVersion = "1.0.1",
    [string]$ExeName = "Saobracaj.exe",
    [string]$OutputIsc = "setup.iss",
    [switch]$IncludePdb
)

if (-not (Test-Path $SourceDir)) {
    Write-Error "Source directory '$SourceDir' not found. Build project in Release configuration first and rerun."
    exit 1
}

# Gather files
$files = Get-ChildItem -Path $SourceDir -Recurse -File | Where-Object {
    if (-not $IncludePdb) {
        $_.Extension -ne '.pdb'
    } else { $true }
}

# Write plain list
$files | ForEach-Object { $_.FullName } | Out-File -FilePath "filelist.txt" -Encoding utf8
Write-Host "Found $($files.Count) files. List written to filelist.txt"

# Build Inno Setup script
$setupHeader = @"
[Setup]
AppName={0}
AppVersion={1}
DefaultDirName={{pf}}\{0}
DefaultGroupName={0}
OutputBaseFilename={0}_Setup_{1}
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
"@ -f $AppName, $AppVersion

# For each file generate a Files entry with relative path preserved
$entries = foreach ($f in $files) {
    $rel = Resolve-Path -Path $f.FullName | ForEach-Object { $_.Path }
    $rel = $rel.Substring((Resolve-Path -Path $SourceDir).Path.Length).TrimStart('\')
    $destDir = "{app}"
    $destPath = Split-Path $rel -Parent
    if ($destPath -and $destPath -ne '.') { $destDir = "{app}\$destPath" }
    # escape double quotes by doubling them for Inno Setup and escape quotes in the output with backtick
    $srcEsc = $f.FullName -replace '"','""'
    $entry = "Source: `"$srcEsc`"; DestDir: `"$destDir`"; Flags: ignoreversion recursesubdirs createallsubdirs"
    $entry
}

$icons = @"

[Icons]
Name: "{group}\$AppName"; Filename: "{app}\$ExeName"
Name: "{userdesktop}\$AppName"; Filename: "{app}\$ExeName"

[Run]
Filename: "{app}\$ExeName"; Description: "Launch $AppName"; Flags: nowait postinstall
"@

$setupContent = $setupHeader + ($entries -join "`r`n") + "`r`n" + $icons

$setupContent | Out-File -FilePath $OutputIsc -Encoding ASCII
Write-Host "Inno Setup script generated: $OutputIsc"
Write-Host "To build installer: install Inno Setup, then run: ISCC.exe $OutputIsc"
