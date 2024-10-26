rem ���� �������� .cs���ϵ� ����
del /S /Q "*.cs"
del /S /Q "..\Server\Server\FlatBuffer\*.cs"
del /S /Q "..\Assets\Script\FlatBuffer\*.cs"

rem .cs���� ����
START ./flatc.exe --csharp "./Protocol.fbs" "./Room.fbs" "Object.fbs"

START ../PacketGenerator/bin/Debug/net8.0/PacketGenerator.exe "./Protocol.fbs"

rem 2�� ��ٸ� �� ���� ����
timeout /t 2 /nobreak >nul
for %%f in (*.cs) do (
    copy "%%f" "../Assets/Script/FlatBuffer"
    copy "%%f" "../Server/Server/FlatBuffer"
)
XCOPY /Y ".\Server\PacketManager.cs" "..\Server\Server\Packet"
XCOPY /Y ".\Client\PacketManager.cs" "..\Assets\Script\Packet"

rem ���� ������ �� pause
if %ERRORLEVEL% neq 0 pause