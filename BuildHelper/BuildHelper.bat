@echo off
set plugin=Widgets.NewProduct
if "%~1" == "" goto MissingParam
if "%~2" == "" goto MissingParam
set nopVersion=%1
set configuration=%2
echo Running custom build step for %plugin% (%configuration%) and %nopVersion%

echo Local folder is %cd%
echo Cleaning up ..\_build ...
rem Cleanup local folders
del ..\_build\%configuration%\nop.core.* /q > nul
del ..\_build\%configuration%\nop.data.* /q > nul
del ..\_build\%configuration%\nop.web.* /q > nul
del ..\_build\%configuration%\nop.services.* /q > nul
rd ..\_build\%configuration%\ref /s /q > nul
rd ..\_build\%configuration%\refs /s /q > nul
rd ..\_build\%configuration%\runtimes /s /q > nul
echo ... all cleaned up

if "%~3" NEQ "" copy %3 ..\_build\%configuration%\. /y
if "%~4" NEQ "" copy %4 ..\_build\%configuration%\. /y
if "%~5" NEQ "" copy %5 ..\_build\%configuration%\. /y
if "%~6" NEQ "" copy %6 ..\_build\%configuration%\. /y
if "%~7" NEQ "" copy %7 ..\_build\%configuration%\. /y

rem Copy to nopCommerce src folder
if "%configuration%" == "release" goto AllDone

set nopOut="n:\nopCommerce %nopVersion%\Presentation\Nop.Web\Plugins\%plugin%"
if not exist %nopOut% (
    echo Creating %nopout%
    mkdir %nopOut%
    echo Created %nopOut%
)
if not exist %nopOut%\Views (
    echo Creating %nopOut%\Views
    mkdir %nopOut%\Views
    echo Created %nopOut%\Views
)
if not exist %nopOut%\Content (
    echo Creating %nopOut%\Content
    mkdir %nopOut%\Content
    echo Created %nopOut%\Content
)

echo Copying extra items ...
xcopy /d /y /Q "..\_build\%configuration%\logo.jpg" %nopOut%\. > nul
xcopy /d /y /Q "..\_build\%configuration%\plugin.json" %nopOut%\. > nul
xcopy /d /y /Q "..\_build\%configuration%\Nop.Plugin.%plugin%*.*" %nopOut%\. > nul
xcopy /d /y /Q "..\_build\%configuration%\nopLocalizationHelper.dll" %nopOut%\. > nul
xcopy /d /y /Q /S "..\_build\%configuration%\Views\." %nopOut%\Views\. > nul
xcopy /d /y /Q /S "..\_build\%configuration%\Content\." %nopOut%\Content\. > nul
echo ... all done copying

echo %nopOut% was updated
goto AllDone
:MissingParam
echo This command script requires two parameter - the nopCommerce version being targeted (e.g. 4.40)
echo And the build configuration (%configuration% or release)
:AllDone