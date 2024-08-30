# DigaoRunner

## Run scripts locally and remotely

In my work environment, I frequently need to create scripts to automate processes. Whether it's to copy files from one place to another for deploying an application or even for more complex tasks involving multiple steps, such as installing services, configuring Windows settings, reading files and extracting information, or any other activity that can be automated.

These are typically tasks where it's not worth writing a specific program. In such cases, we usually resort to a **Batch script (.BAT or .CMD)** or the more modern **PowerShell**.

But let's be honest, for any of these scripts, the language is not very practical, and the script ends up being hard to read and understand, and it's not efficient. For example, error handling in these scripts is cumbersome, and typically, the processes continue even if an error occurs in the middle of the script.

With all this in mind, I developed Digao Runner, which is essentially a debugger for scripts with the **.drs (Digao Runner Script)** extension, where you can write the script using **C# code** in a very practical and easy way. The code is very clean, has **exception handling**, and you can also configure **input fields** for the user to fill in at runtime, like parameters or variables of the script.

Here is an example of a Digao Runner script, used specifically to create the package for the program itself:

```csharp
@DIGAOSCRIPT
VERSION=1
TITLE=Create Digao Runner Package

@CODE
Echo("========================================================", Color.Cyan);
Echo("Generate Digao Runner zip package", Color.Cyan);
Echo("========================================================", Color.Cyan);

int ret;

string vsWhere = @"C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe";
string sevenZip = @"C:\Program Files\7-zip\7z.exe";
string pathProject = @".\DigaoRunnerApp\DigaoRunnerApp.csproj";
string tmpBuildDir = @".\Temp_Build";
string packageFile = @".\DigaoRunner.zip";

string vsPath;
ret = RunProcessReadOutput(vsWhere, "/latest /property installationPath", ref vsPath);
if (ret != 0) Abort("Error getting Visual Studio installation path");

vsPath = vsPath.Split(Environment.NewLine).FirstOrDefault();
if (string.IsNullOrEmpty(vsPath)) Abort("Visual Studio path empty");

string msBuildExe = Path.Combine(vsPath, @"MSBuild\Current\bin\msbuild.exe");

if (Directory.Exists(tmpBuildDir)) Directory.Delete(tmpBuildDir, true);
if (File.Exists(packageFile)) File.Delete(packageFile);

Echo("Compile project");
ret = RunProcess(msBuildExe, 
	$"\"{pathProject}\" /clp:ErrorsOnly /t:Rebuild /p:PlatformTarget=x64 /p:Configuration=Release /p:OutputPath=\"{Path.GetFullPath(tmpBuildDir)}\"");
//using GetFullPath because MSBUILD internally uses current path as project folder path
	
if (ret != 0) Abort("Error compiling project");

Echo("Create zip file");
//RunProcess(sevenZip, $"a -sfx DigaoRunnerSetup.exe \"{tmpBuildDir}\"");
ZipFile.CreateFromDirectory(tmpBuildDir, packageFile);

Directory.Delete(tmpBuildDir, true);

Echo("Package successfully created!", Color.Yellow);

```

# Installation

1. Download and install [.NET 8 Desktop Runtime x64](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

2. Download last Digao Runner release (.zip) from [here](https://github.com/digao-dalpiaz/DigaoRunner/releases/latest)

3. Extract zip to a folder in your computer (Example: C:\DigaoRunner)

4. You may open DigaoRunnerApp.exe and go to menu "More" > "Register .drs files", so the scripts will automatically run with this program.

You are set. Enjoy!