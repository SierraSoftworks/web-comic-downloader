@echo off
UpdateFileGenerator -builddir="..\bin\Release" -outputdir="..\bin\Updates" -app="..\bin\Release\NETKey.exe" -updatefile="..\Updates.xml" -filter *.exe -filter *.dll -exclude .*vshost