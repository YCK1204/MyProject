using System.Collections.Generic;
using System.ComponentModel;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Reflection;
using Unity.Android.Gradle;
using System.Xml.Linq;
using Google.FlatBuffers;

public class RoomList : MonoBehaviour
{
    public static RoomList Instance;

    UIDocument document;
    ListView listView;
    List<RoomInfo> _rooms = new List<RoomInfo>();
    VisualElement root;
    int roomInfo = 1;
    VisualElement createRoom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        document = GetComponent<UIDocument>();
        // Create some list of data, here simply numbers in interval [1, 1000]
        root = document.rootVisualElement;


        #region ListView 초기화
        Func<VisualElement> makeItem = () =>
        {
            var button = new Button();
            var level = new Label();
            var memberCnt = new Label();
            var password = new Label();

            level.AddToClassList("Label-Level");
            button.AddToClassList("Button-room");
            memberCnt.AddToClassList("Label-MemberCount");
            password.AddToClassList("Label-Password");

            level.name = "Label-Level";
            memberCnt.name = "Label-MemberCount";
            password.name = "Label-Password";

            button.Add(level);
            button.Add(memberCnt);
            button.Add(password);
            return button as VisualElement;
        };
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            RoomInfo roomInfo = _rooms[i];
            Button button = e as Button;
            button.RegisterCallback<ClickEvent>((e) => HandleEnterRoom(e, roomInfo));
            Label level = e.Q<Label>("Label-Level");
            Label memberCnt = e.Q<Label>("Label-MemberCount");
            Label password = e.Q<Label>("Label-Password");

            level.text = $"레벨 입니다 {roomInfo.Level.ToString()}";
            password.text = $"비밀번호 입니다 {roomInfo.Password}";
            memberCnt.text = $" {roomInfo.CurMemberCount} / {roomInfo.MemberCount} ";
        };

        listView = root.Q<ListView>();
        listView.fixedItemHeight = 150;
        listView.makeItem = makeItem;
        listView.bindItem = bindItem;
        listView.itemsSource = _rooms;
        listView.selectionType = SelectionType.Multiple;
        #endregion
        #region 방 생성
        createRoom = root.Q<VisualElement>("Container-CreateRoom");
        Button close = createRoom.Q<Button>("Button-Close");
        close.RegisterCallback<ClickEvent>((e) => { createRoom.AddToClassList("CreateRoom-Hide"); });
        Button submit = createRoom.Q<Button>("Button-Submit");
        submit.RegisterCallback<ClickEvent>((e) =>
        {
            TextField id = createRoom.Q<TextField>("TextField-RoomId");
            TextField password = createRoom.Q<TextField>("TextField-Password");
            TextField level = createRoom.Q<TextField>("TextField-Level");
            TextField memberCount = createRoom.Q<TextField>("TextField-MemberCount");

            FlatBufferBuilder builder = new FlatBufferBuilder(1024);
            var ps = builder.CreateString(password.text);

            var data = C_CreateRoom.CreateC_CreateRoom(builder, int.Parse(id.text), int.Parse(level.text), ps, int.Parse(memberCount.text));
            var packet = GameManager.Packet.CreatePacket(data, builder, PacketType.C_CreateRoom);
            GameManager.Network.Send(packet);
        });

        Button createRoomButton = root.Q<Button>("Button-CreateRoom");
        createRoomButton.RegisterCallback<ClickEvent>((e) =>
        {
            Debug.Log("??");
            createRoom.RemoveFromClassList("CreateRoom-Hide");
        });
        #endregion
        Button resetButton = root.Q<Button>("Button-Reset");
        resetButton.RegisterCallback<ClickEvent>((e) =>
        {
            FlatBufferBuilder builder = new FlatBufferBuilder(1024);

            C_RoomList.StartC_RoomList(builder);
            var data = C_RoomList.EndC_RoomList(builder);
            var packet = GameManager.Packet.CreatePacket(data, builder, PacketType.C_RoomList);
            GameManager.Network.Send(packet);
        });

        
    }

    public void InitRoomList(S_RoomList roomList)
    {
        int length = roomList.RoomsLength;
        _rooms.Clear();
        for (int i = 0; i < length; i++)
            _rooms.Add(roomList.Rooms(i).Value);
        listView.RefreshItems();
    }
    void HandleEnterRoom(ClickEvent e, RoomInfo roomInfo)
    {
        int id = roomInfo.RoomId;

        FlatBufferBuilder builder = new FlatBufferBuilder(1024);
        var password = builder.CreateString(roomInfo.Password);
        var data = C_EnterRoom.CreateC_EnterRoom(builder, id, password);
        var packet = GameManager.Packet.CreatePacket(data, builder, PacketType.C_EnterRoom);
        GameManager.Network.Send(packet);
    }
}
