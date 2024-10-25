START ./flatc.exe --csharp "./Protocol.fbs"
START ../PacketGenerator/bin/Debug/net8.0/PacketGenerator.exe "./Protocol.fbs"

for %%f in (*.cs) do (
    copy "%%f" "../Assets/Script/FlatBuffer"
    copy "%%f" "../Server/Server/FlatBuffer"
)

XCOPY /Y ".\Server\PacketManager.cs" "..\Server\Server\Packet"
XCOPY /Y ".\Client\PacketManager.cs" "..\Assets\Script\Packet"

if %ERRORLEVEL% neq 0 pause