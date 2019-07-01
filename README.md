LitJSON
=======

[![NuGet](https://img.shields.io/nuget/v/LitJson.svg)](https://www.nuget.org/packages/LitJson) [![MyGet](https://img.shields.io/myget/litjson/vpre/LitJson.svg?label=myget)](https://www.myget.org/gallery/litjson)

A *.Net* library to handle conversions from and to JSON (JavaScript Object
Notation) strings.

> _It's quick and lean, without external dependencies.
> Just a few classes so easily embeddable in your own code or a very small assembly to ship with your code.
> The code is highly portable, which in general makes it easy to adapt for new platforms._


## Continuous integration

| Build server                | Platform      | Build status                                                                                                                                                   |
|-----------------------------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| AppVeyor                    | Windows       | [![AppVeyor branch](https://img.shields.io/appveyor/ci/litjson/litjson/develop.svg)](https://ci.appveyor.com/project/litjson/litjson/branch/develop)           |
| Bitrise                     | MacOS         | [![Build Status](https://www.bitrise.io/app/5975a00ca2666fb1/status.svg?token=OZnv4YWRw71IVax38Wi50Q&branch=develop)](https://www.bitrise.io/app/5975a00ca2666fb1) |
| Bitrise                     | Linux         | [![Build Status](https://www.bitrise.io/app/4c9ee62c6ba13630/status.svg?token=RBH8UKw-68lQYjageT8VoQ&branch=develop)](https://www.bitrise.io/app/4c9ee62c6ba13630) |
| Travis                      | Linux / MacOS | [![Travis build status](https://travis-ci.org/LitJSON/litjson.svg?branch=develop)](https://travis-ci.org/LitJSON/litjson)                                      |


## Compiling

Code can be compiled using .NET CLI or by launching the bootstrappers in the root of the repository.

#### Windows

```powershell
./build.ps1
```

#### Linux / OS X

```console
./build.sh
```

#### Prerequisites

The bootstrappers will (locally in repo)
  
  * Fetch and install .NET Core CLI / SDK version needed to compile LitJSON.
  * Fetch and install Cake runner
  * Execute build script with supplied target (`--target=[Target]`) or by default
    1. Clean previous artifacts
    1. Restore build dependencies from NuGet
    1. Build
    1. Run unit tests
    1. Create NuGet package
  
#### Testing

This library comes with a set of unit tests using the [NUnit][nunit]
framework.

## Using LitJSON from an application

#### Package manager

```PowerShell
Install-Package LitJson -Version 0.10.0
```

#### .NET CLI

```PowerShell
dotnet add package LitJson --version 0.10.0
```

#### Paket CLI

```PowerShell
paket add LitJson --version 0.10.0
```

Alternatively, just copy the whole tree of files under `src/LitJSON` to your
own project's source tree and integrate it with your development environment.

#### Requirements

LitJSON currently targets and supports
* .NET Standard 2.0
* .NET Standard 1.5
* .NET Framework 4.5 and above
* .NET Framework 4.0
* .NET Framework 3.5 (including SQLCLR, for which [WCOMAB/SqlServerSlackAPI](https://github.com/WCOMAB/SqlServerSlackAPI) is an example of)
* .NET Framework 2.0
* Mono 4.4.2 and above

#### Prereleases

Each merge to develop is published to our NuGet feed on [MyGet](mygetgallery).

## Contributing

So you’re thinking about contributing to LitJSON? Great! It’s **really** appreciated.

* Create an issue
* Fork the repository.
* Create a feature branch from `develop` to work in.
* Make your feature addition or bug fix.
* Don't forget the unit tests.
* Send a pull request.

## License

[Unlicense][unlicense] (public domain).

[mygetgallery]: [https://www.myget.org/gallery/litjson]
[litjson]: [unlicense](http://unlicense.org/
[nunit]: http://www.nunit.org/
[pkg-config]: http://www.freedesktop.org/wiki/Software/pkg-config
[unlicense]: http://unlicense.org/
