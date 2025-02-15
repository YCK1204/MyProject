rem ���� �������� ���ϵ� ����
set CLIENT_SCRIPT_PATH=..\..\Client\Assets\Scripts
set SERVER_SCRIPT_PATH=..\Server\Server

set ROOT_FBS=fbs/Protocol.fbs

rem ȯ�溯�� ���� ����
setlocal enabledelayedexpansion

del /S /Q "*.cs"
del /S /Q "%CLIENT_SCRIPT_PATH%\FlatBuffer\*.cs"
del /S /Q "%SERVER_SCRIPT_PATH%\FlatBuffer\*.cs"

set "fileList="

rem ���� ���丮�� ��� .fbs ������ ����
for %%f in (./fbs/*.fbs) do (
    set "fileList=!fileList! fbs/%%f"
)

rem flatc ���� (������ ���� ����Ʈ�� �� ���� ����)
START ./flatc.exe --csharp %fileList%
START ../PacketGenerator/bin/Debug/net8.0/PacketGenerator.exe %ROOT_FBS%

rem 3�� ��ٸ� �� ���� ����
timeout /t 3 /nobreak >nul
for %%f in (*.cs) do (
    copy "%%f" "%CLIENT_SCRIPT_PATH%/FlatBuffer"
    copy "%%f" "%SERVER_SCRIPT_PATH%/FlatBuffer"
)

XCOPY /Y ".\Client\PacketManager.cs" "%CLIENT_SCRIPT_PATH%\Packet"
XCOPY /Y ".\Server\PacketManager.cs" "%SERVER_SCRIPT_PATH%\Packet"

pause