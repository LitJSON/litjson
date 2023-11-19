// Install tools
#tool nuget:?package=NUnit.ConsoleRunner&version=3.16.3

// Install .NET Core Global tools.
#tool dotnet:?package=GitVersion.Tool&version=5.12.0

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PARAMETERS
//////////////////////////////////////////////////////////////////////

DotNetMSBuildSettings msBuildSettings = null;
string  version = null,
        semVersion = null,
        milestone = null;

FilePath    litjsonProjectPath = "./src/LitJson/LitJSON.csproj",
            litjsonSourceProjectPath = "./src/LitJson.Source/LitJSON.Source.csproj";

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

    msBuildSettings = new DotNetMSBuildSettings()
                            .WithProperty("Version", semVersion)
                            .WithProperty("AssemblyVersion", version)
                            .WithProperty("FileVersion", version)
                            .WithProperty("ContinuousIntegrationBuild", BuildSystem.IsLocalBuild ? bool.FalseString : bool.TrueString);

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
    DotNetRestore("./LitJSON.sln",
        new DotNetRestoreSettings { MSBuildSettings = msBuildSettings }
    );
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
    DotNetBuild("./LitJSON.sln",
        new DotNetBuildSettings {
            Configuration = configuration,
            MSBuildSettings = msBuildSettings,
            ArgumentCustomization = args => args.Append("--no-restore")
        }
    );
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
    DotNetTest("./test/LitJSON.Tests.csproj",
        new DotNetTestSettings {
            Configuration = configuration,
            Framework = "net6.0",
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
    .WithCriteria(IsRunningOnWindows() && AppVeyor.IsRunningOnAppVeyor)
    .Does(() => {
    foreach(var asssembly in GetFiles("./src/LitJson/bin/" + configuration + "/**/*.dll"))
    {
        DotNetTool(litjsonProjectPath.FullPath, "sourcelink", $"test {asssembly}");
    }
});

Task("Package")
    .IsDependentOn("Test")
    .IsDependentOn("Test-SourceLink")
    .Does(() => {
    DotNetPack(litjsonProjectPath.FullPath,
        new DotNetPackSettings {
            Configuration = configuration,
            NoRestore = true,
            NoBuild = true,
            IncludeSymbols = true,
            OutputDirectory = "./artifacts/nuget",
            MSBuildSettings = msBuildSettings
        }
    );
    DotNetPack(litjsonSourceProjectPath.FullPath,
        new DotNetPackSettings {
            Configuration = configuration,
            NoBuild = true,
            NoRestore = true,
            OutputDirectory = "./artifacts/nuget",
            MSBuildSettings = msBuildSettings
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
    .WithCriteria(
        (
            AppVeyor.IsRunningOnAppVeyor &&
            !AppVeyor.Environment.PullRequest.IsPullRequest &&
            (
                !AppVeyor.Environment.Repository.Branch.Equals("master", StringComparison.OrdinalIgnoreCase) ||
                AppVeyor.Environment.Repository.Tag.IsTag
            )
        )
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
        DotNetNuGetPush(package.FullPath,
        new DotNetNuGetPushSettings {
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
        throw new InvalidOperationException("Could not resolve NuGet API key.");
    }

    // Resolve the API url.
    var apiUrl = EnvironmentVariable("NUGET_API_URL");
    if(string.IsNullOrEmpty(apiUrl)) {
        throw new InvalidOperationException("Could not resolve NuGet API url.");
    }

    foreach(var package in (GetFiles("./artifacts/nuget/*.nupkg") - GetFiles("./artifacts/nuget/*.symbols.nupkg")))
    {
        DotNetNuGetPush(package.FullPath,
        new DotNetNuGetPushSettings {
            ApiKey = apiKey,
            Source = apiUrl
        }
    );
    }
});

Task("Push-GitHub-Packages")
  .IsDependentOn("Package")
    .WithCriteria(
        GitHubActions.IsRunningOnGitHubActions &&
        !GitHubActions.Environment.PullRequest.IsPullRequest &&
        IsRunningOnWindows())
    .Does(() => {

      // Resolve the API key.
    var apiKey = EnvironmentVariable("GH_PACKAGES_NUGET_APIKEY");
    if(string.IsNullOrEmpty(apiKey)) {
        throw new InvalidOperationException("Could not resolve GitHub API key.");
    }

    // Resolve the API url.
    var apiUrl = EnvironmentVariable("GH_PACKAGES_NUGET_SOURCE");
    if(string.IsNullOrEmpty(apiUrl)) {
        throw new InvalidOperationException("Could not resolve GitHub API url.");
    }

    foreach(var package in (GetFiles("./artifacts/nuget/*.nupkg") - GetFiles("./artifacts/nuget/*.symbols.nupkg")))
    {
        DotNetNuGetPush(package.FullPath,
        new DotNetNuGetPushSettings {
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

Task("GitHub-Actions")
  .IsDependentOn("Push-GitHub-Packages");

Task("Default")
  .IsDependentOn("Package");

RunTarget(target);