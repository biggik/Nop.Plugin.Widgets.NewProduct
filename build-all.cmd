@echo off
cls
set sln_file=Nop.Plugin.Widgets.NewProduct

..\nopPackager\dist\nopPackager.exe c:\development\Status\nopCommerce-Plugins\%sln_file%
