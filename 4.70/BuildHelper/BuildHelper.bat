@echo off
set helper=%cd%\..\BuildHelper\%computername%-widgets-NewProduct-%1.bat
Echo Running buildhelper for %1 on %computername% (%helper%)
if exist %helper% ( call %helper% ) else ( echo Not found)