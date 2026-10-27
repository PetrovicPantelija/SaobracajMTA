; Inno Setup skripta za instalaciju aplikacije Saobracaj
; Pre pokretanja napraviti Release build: MSBuild Saobracaj\Saobracaj.csproj /p:Configuration=Release /p:Platform=AnyCPU
; Instalacija: "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" setup.iss  ->  output\Saobracaj_setup_<verzija>.exe
; Putanje su relativne u odnosu na ovaj fajl, pa skripta radi na svakom racunaru.
;
; Instalacija za jednu firmu: ISCC.exe /DFirma=Leget setup.iss  ->  output\Saobracaj_Leget_setup_<verzija>.exe
; Takva instalacija posle instaliranja ostavlja <Firma>.txt pored Saobracaj.exe (po njemu Program.cs bira firmu),
; a prethodno brise .txt drugih firmi da ih ne bi bilo vise. Bez /DFirma se .txt ne dira.

#define AppName "Saobracaj"
#define AppVersion "1.0.2"
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
#ifdef Firma
OutputBaseFilename={#AppName}_{#Firma}_setup_{#AppVersion}
#else
OutputBaseFilename={#AppName}_setup_{#AppVersion}
#endif
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

#ifdef Firma
[InstallDelete]
; firma se bira po jedinom .txt pored exe-a: .txt drugih firmi iz ranije instalacije se brisu
Type: files; Name: "{app}\Leget.txt"
Type: files; Name: "{app}\TA.txt"
Type: files; Name: "{app}\TA_TEST.txt"
Type: files; Name: "{app}\DPT.txt"
Type: files; Name: "{app}\VSD.txt"
Type: files; Name: "{app}\KP.txt"

[UninstallDelete]
Type: files; Name: "{app}\{#Firma}.txt"

[Code]
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
    SaveStringToFile(ExpandConstant('{app}\{#Firma}.txt'), '', False);
end;
#endif
