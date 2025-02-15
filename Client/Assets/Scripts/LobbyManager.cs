using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

public class LobbyManager : MonoBehaviour
{
    private UIDocument uiDocument;
    private ListView roomListView;
    private TextField roomNameInput;
    private TextField passwordInput;
    private DropdownField memberCountDropdown;
    private Button makeRoomButton;
    private List<Room> rooms = new List<Room>();
    private Room selectedRoom = null;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("LobbyManager: UIDocument가 연결되지 않았습니다!");
            return;
        }

        roomListView = uiDocument.rootVisualElement.Q<ListView>("room-list");
        roomNameInput = uiDocument.rootVisualElement.Q<TextField>("roomname-input");
        passwordInput = uiDocument.rootVisualElement.Q<TextField>("password-input");
        memberCountDropdown = uiDocument.rootVisualElement.Q<DropdownField>("member-count");
        makeRoomButton = uiDocument.rootVisualElement.Q<Button>("make-room");

        if (roomListView == null || roomNameInput == null || passwordInput == null || memberCountDropdown == null || makeRoomButton == null)
        {
            Debug.LogError("LobbyManager: UI 요소를 찾을 수 없습니다!");
            return;
        }

        // 멤버 수 Dropdown 설정 (1~4명 선택 가능)
        memberCountDropdown.choices = new List<string> { "1", "2", "3", "4" };
        memberCountDropdown.index = 0;

        // 방 생성 버튼 클릭 이벤트 처리
        makeRoomButton.clicked += OnMakeRoomClicked;

        // ListView 항목을 구성하는 부분
        roomListView.makeItem = () => new Button();  // 각 항목에 새로운 버튼 생성
        roomListView.bindItem = (element, index) =>
        {
            var room = rooms[index];
            var button = element as Button;

            if (button != null)
            {
                // 버튼의 텍스트를 설정
                button.text = $"{room.RoomTitle} - {room.PlayerCount}/{room.MaxPlayers} - {(string.IsNullOrEmpty(room.Password) ? "No Password" : "Protected")}";

                // 버튼 클릭 이벤트 설정
                button.clicked += () => OnRoomSelected(button, room);

                // 선택된 방이 있다면 초록색 배경 적용
                if (selectedRoom == room)
                {
                    button.style.backgroundColor = Color.green;
                    button.style.fontSize = 16; // 텍스트 크기 증가
                    button.style.unityFontStyleAndWeight = FontStyle.Bold; // 텍스트 볼드체
                }
                else
                {
                    button.style.backgroundColor = Color.white;
                    button.style.fontSize = 14; // 기본 텍스트 크기
                    button.style.unityFontStyleAndWeight = FontStyle.Normal; // 기본 폰트 스타일
                }
            }
        };

        roomListView.itemsSource = rooms;
    }

    // 방 생성 클릭 시 실행되는 메서드
    private void OnMakeRoomClicked()
    {
        string roomTitle = roomNameInput.value;
        string password = passwordInput.value;
        int maxPlayers = int.Parse(memberCountDropdown.value); // 선택한 인원 가져오기

        if (string.IsNullOrWhiteSpace(roomTitle))
        {
            Debug.Log("방 제목을 입력하세요!");
            return;
        }

        // 새로운 방을 생성
        Room newRoom = new Room(roomTitle, "SteamID", 0, 0, maxPlayers, password);
        rooms.Add(newRoom);

        // ListView 업데이트
        roomListView.Rebuild();
        Debug.Log($"방 생성: {roomTitle}, 최대 인원: {maxPlayers}명");
    }

    // 방 선택 시 실행되는 메서드
    private void OnRoomSelected(Button selectedButton, Room room)
    {
        // 모든 버튼을 확인하여 이전 선택 방의 스타일을 복원
        foreach (var button in roomListView.Query<Button>().ToList())
        {
            if (selectedRoom != null && selectedRoom != room && button.text.StartsWith(selectedRoom.RoomTitle))
            {
                // 이전 선택 방의 버튼 색 복원
                button.style.backgroundColor = Color.white;
                button.style.fontSize = 14;
                button.style.unityFontStyleAndWeight = FontStyle.Normal;
            }
        }

        // 현재 방을 선택된 방으로 설정
        selectedRoom = room;

        // 현재 방 버튼의 스타일을 선택된 스타일로 설정
        selectedButton.style.backgroundColor = Color.green;
        selectedButton.style.fontSize = 16;
        selectedButton.style.unityFontStyleAndWeight = FontStyle.Bold;
    }
}
