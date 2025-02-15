rem 기존 프로토콜 파일들 삭제
set CLIENT_SCRIPT_PATH=..\..\Client\Assets\Scripts
set SERVER_SCRIPT_PATH=..\Server\Server

set ROOT_FBS=fbs/Protocol.fbs

rem 환경변수 적용 설정
setlocal enabledelayedexpansion

del /S /Q "*.cs"
del /S /Q "%CLIENT_SCRIPT_PATH%\FlatBuffer\*.cs"
del /S /Q "%SERVER_SCRIPT_PATH%\FlatBuffer\*.cs"

set "fileList="

rem 현재 디렉토리의 모든 .fbs 파일을 누적
for %%f in (./fbs/*.fbs) do (
    set "fileList=!fileList! fbs/%%f"
)

rem flatc 실행 (누적된 파일 리스트를 한 번에 전달)
START ./flatc.exe --csharp %fileList%
START ../PacketGenerator/bin/Debug/net8.0/PacketGenerator.exe %ROOT_FBS%

rem 3초 기다린 후 파일 복사
timeout /t 3 /nobreak >nul
for %%f in (*.cs) do (
    copy "%%f" "%CLIENT_SCRIPT_PATH%/FlatBuffer"
    copy "%%f" "%SERVER_SCRIPT_PATH%/FlatBuffer"
)

XCOPY /Y ".\Client\PacketManager.cs" "%CLIENT_SCRIPT_PATH%\Packet"
XCOPY /Y ".\Server\PacketManager.cs" "%SERVER_SCRIPT_PATH%\Packet"

pause