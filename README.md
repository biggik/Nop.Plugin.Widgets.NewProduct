# Nop.Plugin.Widgets.NewProduct
This is a nopCommerce widget plugin for displaying a marker for new products as defined in the nopCommerce
product admin

# Building
## Directory structure
The repository contains separate directories for each supported nopCommerce version, 4.70, and so on.

## Common folder
The Common folder contains most of the common source code for each nopCommerce version of the plugin. Files are referenced (linked) from there.

## NopCommerce source
Each *Nop.Plugin.Widgets.NewProduct* project contains references to nopCommerce projects. For convenience the nopCommerce files are references via drive *n:*. It is recommended to map n: to the location of the nopCommerce source. For example, if nopCommerce source files are available in *c:\dev\nopCommerce public releases*, and each version in a subdirectory named by the release (i.e. *nopCommerce 4.20*) then the following command will map n: to that location

    net use n: "\\mycomputer\c$\dev\nopCommerce public releases" /persistent:yes

where mycomputer is the name of the development machine.
 
 ## Build the projects
 To build the projects, either open the preferred solution (.sln) file, or execute the build-all.cmd file in the root directory. That command builds all versions both in debug and release mode.

The output of the build, whether from Visual Studio, or from the build-all.cmd file, goes to the *_build* directory in the root directory
