# BSTransparentWall
TransparentWall is made to do one simple thing
### - Make the in-game walls transparent for you! -
<p align="left">
  <img src="https://i.imgur.com/lCgqOtz.jpg" width="430" title="transparentwalls">
</p>

## Features
* Enable transparent walls in VR
* Disable the transparent walls for the LIV camera (introduced in Beat Saber v0.13.0)

These options can be changed from inside the game, so there's no need to edit configuration files.

## Note
While TransparentWall is enabled in the HMD/VR view, high scores will be temporarily disabled.

By turning this option off again in the settings, high scores will be re-enabled.
<p align="left">
  <img src="https://i.imgur.com/gWxKjQK.jpg" width="650" title="scoresubdisable">
</p>

## For developers

### Contributing to CustomNotes
In order to build this project, please add your Beat Saber directory path to the `TransparentWall.csproj.user` file located in the project directory like for example this:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <!-- Set "YOUR OWN" Beat Saber folder here to resolve most of the dependency paths! -->
    <BeatSaberDir>E:\Program Files (x86)\Steam\steamapps\common\Beat Saber</BeatSaberDir>
  </PropertyGroup>
</Project>
```

If you plan on adding any new dependencies which are located in the Beat Saber directory, it would be nice if you edited the paths to use `$(BeatSaberDir)` in `TransparentWall.csproj` like so to keep some consistency

```xml
...
<Reference Include="BS_Utils">
  <HintPath>$(BeatSaberDir)\Plugins\BS_Utils.dll</HintPath>
</Reference>
<Reference Include="IPA.Loader">
  <HintPath>$(BeatSaberDir)\Beat Saber_Data\Managed\IPA.Loader.dll</HintPath>
</Reference>
...
```
