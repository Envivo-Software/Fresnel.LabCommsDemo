set ORIG_DIR=%CD%
cd AppLauncher
dotnet publish /p:PublishProfile=FolderProfile -p:PublishTrimmed=false
cd %ORIG_DIR%
