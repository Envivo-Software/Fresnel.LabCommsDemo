@echo "This will destroy all BIN and OBJ folders. Hit Ctrl-C to abort, or any key to continue"
@pause

for /d /r . %%d in (bin,obj) do @if exist "%%d" rd /s/q "%%d"
