// Install modules
#module nuget:?package=Cake.DotNetTool.Module&version=0.3.0

// Install tools
#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

// Install .NET Core Global tools.
#tool "dotnet:https://api.nuget.org/v3/index.json?package=GitVersion.Tool&version=5.0.1"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PARAMETERS
//////////////////////////////////////////////////////////////////////

DotNetCoreMSBuildSettings msBuildSettings = null;
string  version = null,
        semVersion = null,
        milestone = null;

FilePath litjsonProjectPath = "./src/LitJson/LitJSON.csproj";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
    Information("Calculating Semantic Version");
    if (!BuildSystem.IsLocalBuild)
    {
        GitVersion(new GitVersionSettings{
            OutputType = GitVersionOutput.BuildServer
        });
    }

    CopyFile("./src/LitJson/AssemblyInfo.cs.in", "./src/LitJson/AssemblyInfo.cs");

    GitVersion assertedVersions = GitVersion(new GitVersionSettings
    {
        UpdateAssemblyInfoFilePath = "./src/LitJson/AssemblyInfo.cs",
        UpdateAssemblyInfo = true,
        OutputType = GitVersionOutput.Json,
    });

    version = assertedVersions.MajorMinorPatch;
    semVersion = assertedVersions.LegacySemVerPadded;
    milestone = string.Concat("v", version);

    Information("Calculated Semantic Version: {0}", semVersion);

    msBuildSettings = new DotNetCoreMSBuildSettings()
                            .WithProperty("Version", semVersion)
                            .WithProperty("AssemblyVersion", version)
                            .WithProperty("FileVersion", version);

    if(!IsRunningOnWindows())
    {
        var frameworkPathOverride = ctx.Environment.Runtime.IsCoreClr
                                        ?   new []{
                                                new DirectoryPath("/Library/Frameworks/Mono.framework/Versions/Current/lib/mono"),
                                                new DirectoryPath("/usr/lib/mono"),
                                                new DirectoryPath("/usr/local/lib/mono")
                                            }
                                            .Select(directory =>directory.Combine("4.5"))
                                            .FirstOrDefault(directory => ctx.DirectoryExists(directory))
                                            ?.FullPath + "/"
                                        : new FilePath(typeof(object).Assembly.Location).GetDirectory().FullPath + "/";

        // Use FrameworkPathOverride when not running on Windows.
        Information("Build will use FrameworkPathOverride={0} since not building on Windows.", frameworkPathOverride);
        msBuildSettings.WithProperty("FrameworkPathOverride", frameworkPathOverride);
    }

    // Executed BEFORE the first task.
    Information("Running tasks...");
});

Teardown(ctx =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() => {
    CleanDirectories(
        new[] {
            "./src/bin",
            "./test/bin",
            "./src/obj",
            "./test/obj",
            "./artifacts/nuget"
        }
    );
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => {
    DotNetCoreRestore("./LitJSON.sln",
        new DotNetCoreRestoreSettings { MSBuildSettings = msBuildSettings }
    );
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
    DotNetCoreBuild("./LitJSON.sln",
        new DotNetCoreBuildSettings {
            Configuration = configuration,
            MSBuildSettings = msBuildSettings,
            ArgumentCustomization = args => args.Append("--no-restore")
        }
    );
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
    DotNetCoreTest("./test/LitJSON.Tests.csproj",
        new DotNetCoreTestSettings {
            Configuration = configuration,
            Framework = "netcoreapp2.0",
            NoBuild = true,
            ArgumentCustomization = args => args.Append("--no-restore")
        }
    );

    NUnit3("./test/**/bin/" + configuration + "/net45/*.Tests.dll", new NUnit3Settings {
        NoResults = true
    });
});

Task("Test-SourceLink")
    .IsDependentOn("Build")
    .WithCriteria(IsRunningOnWindows())
    .Does(() => {
    foreach(var asssembly in GetFiles("./src/LitJson/bin/" + configuration + "/**/*.dll"))
    {
        DotNetCoreTool(litjsonProjectPath.FullPath, "sourcelink", $"test {asssembly}");
    }
});

Task("Package")
    .IsDependentOn("Test")
    .IsDependentOn("Test-SourceLink")
    .Does(() => {
    DotNetCorePack(litjsonProjectPath.FullPath,
        new DotNetCorePackSettings {
            Configuration = configuration,
            NoBuild = true,
            IncludeSymbols = true,
            OutputDirectory = "./artifacts/nuget",
            MSBuildSettings = msBuildSettings,
            ArgumentCustomization = args => args.Append("--no-restore")
        }
    );
});

Task("Upload-AppVeyor-Artifacts")
    .IsDependentOn("Package")
    .WithCriteria(AppVeyor.IsRunningOnAppVeyor)
    .Does(() => {
    foreach(var artifact in GetFiles("./artifacts/**/*.*"))
    {
        AppVeyor.UploadArtifact(artifact);
    }
});

Task("Publish-MyGet")
    .IsDependentOn("Package")
    .WithCriteria((AppVeyor.IsRunningOnAppVeyor && !AppVeyor.Environment.PullRequest.IsPullRequest)
        || StringComparer.OrdinalIgnoreCase.Equals(target, "Publish-MyGet"))
    .Does(() => {

      // Resolve the API key.
    var apiKey = EnvironmentVariable("MYGET_API_KEY");
    if(string.IsNullOrEmpty(apiKey)) {
        throw new InvalidOperationException("Could not resolve MyGet API key.");
    }

    // Resolve the API url.
    var apiUrl = EnvironmentVariable("MYGET_API_URL");
    if(string.IsNullOrEmpty(apiUrl)) {
        throw new InvalidOperationException("Could not resolve MyGet API url.");
    }

    foreach(var package in (GetFiles("./artifacts/nuget/*.nupkg") - GetFiles("./artifacts/nuget/*.symbols.nupkg")))
    {
        DotNetCoreNuGetPush(package.FullPath,
        new DotNetCoreNuGetPushSettings {
            ApiKey = apiKey,
            Source = apiUrl
        }
    );
    }
});

Task("Publish-NuGet")
    .IsDependentOn("Package")
    .WithCriteria((AppVeyor.IsRunningOnAppVeyor && AppVeyor.Environment.Repository.Tag.IsTag && !AppVeyor.Environment.PullRequest.IsPullRequest)
        || StringComparer.OrdinalIgnoreCase.Equals(target, "Publish-NuGet"))
    .Does(() => {

      // Resolve the API key.
    var apiKey = EnvironmentVariable("NUGET_API_KEY");
    if(string.IsNullOrEmpty(apiKey)) {
        throw new InvalidOperationException("Could not resolve MyGet API key.");
    }

    // Resolve the API url.
    var apiUrl = EnvironmentVariable("NUGET_API_URL");
    if(string.IsNullOrEmpty(apiUrl)) {
        throw new InvalidOperationException("Could not resolve MyGet API url.");
    }

    foreach(var package in (GetFiles("./artifacts/nuget/*.nupkg") - GetFiles("./artifacts/nuget/*.symbols.nupkg")))
    {
        DotNetCoreNuGetPush(package.FullPath,
        new DotNetCoreNuGetPushSettings {
            ApiKey = apiKey,
            Source = apiUrl
        }
    );
    }
});

Task("AppVeyor")
  .IsDependentOn("Upload-AppVeyor-Artifacts")
  .IsDependentOn("Publish-MyGet")
  .IsDependentOn("Publish-NuGet");

Task("Default")
  .IsDependentOn("Package");

RunTarget(target);