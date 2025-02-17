using Google.FlatBuffers;
using Server.Managers;
using ServerCore;
public partial class PacketHandler
{
    public static void C_CreateRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        FlatBufferBuilder builder = new FlatBufferBuilder(100);

        // 유저 유효성 체크 필요
        CreateRoomError error;
        try
        {
            var pkt = C_CreateRoom.GetRootAsC_CreateRoom(buffer);

            // title과 password valid 값 체크 필요
            string title = pkt.RoomTitle;
            string password = pkt.Password;
            var theme = pkt.Theme;

            error = CreateRoomError.SUCCESS;
        }
        catch (Exception e)
        {
            error = CreateRoomError.UNKNOWN;
        }

        var data = S_CreateRoom.CreateS_CreateRoom(builder, error);
        var packet = Manager.Packet.CreatePacket(data, builder, PacketType.S_CreateRoom);
        session.Send(packet);
    }
    public static void C_RoomInfoHandler(PacketSession session, ByteBuffer buffer)
    {
        FlatBufferBuilder builder = new FlatBufferBuilder(100);

        try
        {
            var pkt = C_RoomInfo.GetRootAsC_RoomInfo(buffer);
            var data = Manager.Room.CreateRoomInfos(builder);
            var packet = Manager.Packet.CreatePacket(data, builder, PacketType.S_RoomInfo);
            session.Send(packet);
        }
        catch (Exception e)
        {
            // todo
        }
    }
}
