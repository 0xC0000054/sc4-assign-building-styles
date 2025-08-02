# sc4-assign-building-styles

A utility that assigns building styles to DBPF plugins.

It can be downloaded from the Releases tab: https://github.com/0xC0000054/sc4-assign-building-styles/releases

## System Requirements

[.NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

## Usage

`SC4AssignBuildingStyles OPTIONS <file or directory>`

Options:

| Long form | Short form | Description |
|---------------|------------|-----------|
| --building-styles | -s | The hexadecimal building style ids. Multiple values must be separated by commas. |
| --wall-to-wall | -w | Indicates if the building are wall to wall (W2W). Must be true or false. |
| --recurse-subdirectories | -r | Search subdirectories of the input folder for DBPF files. |
| --install-folder-path | -i | The path of your SimCity 4 installation folder. Used to find parent cohorts. |
| --plugin-folder-path | -p | The path of your SimCity 4 plugin folder. Used to find parent cohorts. |

Example: 

`SC4AssignBuildingStyles --building-styles 0x2000,0x2001 --wall-to-wall true file.dat`

The above command will edit all building exemplars in `file.dat` to have the _Building Styles_ property
set to 0x2000,0x2001 and the _Building is W2W_ property set to true.

## License

This project is licensed under the terms of the MIT License.   
See [License.txt](License.txt) for more information.

### 3rd party code

[DBPFSharp](https://github.com/0xC0000054/DBPFSharp) - MIT License    
[Mono.Options](https://github.com/xamarin/XamarinComponents/tree/main/XPlat/Mono.Options) - MIT License    
[zlib](https://github.com/madler/zlib) - zlib License   

# Source Code

## Prerequisites

* Visual Studio 2022
* git submodule update --init

## Building the plugin

* Open the solution in the src folder
* Build the solution
