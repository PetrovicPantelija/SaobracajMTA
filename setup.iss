; Inno Setup skripta za instalaciju aplikacije Saobracaj
; Pre pokretanja napraviti Release build: MSBuild Saobracaj\Saobracaj.csproj /p:Configuration=Release /p:Platform=AnyCPU
; Instalacija: "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" setup.iss  ->  output\Saobracaj_setup_<verzija>.exe
; Putanje su relativne u odnosu na ovaj fajl, pa skripta radi na svakom racunaru.

#define AppName "Saobracaj"
#define AppVersion "1.0.1"
#define AppExe "Saobracaj.exe"
#define SourceDir "Saobracaj\bin\Release"

[Setup]
; AppId mora ostati isti u svim verzijama da bi nova verzija nadogradila staru
AppId={{3F6C2A1E-8B4D-4E6F-9A2C-5D7E1B0C4F93}
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher=MTA
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
UninstallDisplayIcon={app}\{#AppExe}
OutputDir=output
OutputBaseFilename={#AppName}_setup_{#AppVersion}
Compression=lzma2
SolidCompression=yes
PrivilegesRequired=admin
; AnyCPU build se na 64-bitnom Windows-u instalira u Program Files (ne x86)
ArchitecturesInstallIn64BitMode=x64compatible
WizardStyle=modern
CloseApplications=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
; Ceo Release folder (sa podfolderima za jezike i x86/x64 biblioteke), bez .pdb fajlova
Source: "{#SourceDir}\*"; DestDir: "{app}"; Excludes: "*.pdb,*.vshost.*"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#AppName}"; Filename: "{app}\{#AppExe}"
Name: "{group}\{cm:UninstallProgram,{#AppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\{#AppExe}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExe}"; Description: "{cm:LaunchProgram,{#AppName}}"; Flags: nowait postinstall skipifsilent
