rem 기존 프로토콜 .cs파일들 삭제
del /S /Q "*.cs"
del /S /Q "..\Server\Server\FlatBuffer\*.cs"
del /S /Q "..\Assets\Script\FlatBuffer\*.cs"

rem .cs파일 생성
START ./flatc.exe --csharp "./Protocol.fbs" "./Room.fbs" "Object.fbs"

START ../PacketGenerator/bin/Debug/net8.0/PacketGenerator.exe "./Protocol.fbs"

rem 2초 기다린 후 파일 복사
timeout /t 2 /nobreak >nul
for %%f in (*.cs) do (
    copy "%%f" "../Assets/Script/FlatBuffer"
    copy "%%f" "../Server/Server/FlatBuffer"
)
XCOPY /Y ".\Server\PacketManager.cs" "..\Server\Server\Packet"
XCOPY /Y ".\Client\PacketManager.cs" "..\Assets\Script\Packet"

rem 문제 생겼을 시 pause
if %ERRORLEVEL% neq 0 pause