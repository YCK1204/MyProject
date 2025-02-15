// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

public enum PacketType : byte
{
  NONE = 0,
  C_CreateRoom = 1,
  S_CreateRoom = 2,
};



static public class PacketTypeVerify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, byte typeId, uint tablePos)
  {
    bool result = true;
    switch((PacketType)typeId)
    {
      case PacketType.C_CreateRoom:
        result = C_CreateRoomVerify.Verify(verifier, tablePos);
        break;
      case PacketType.S_CreateRoom:
        result = S_CreateRoomVerify.Verify(verifier, tablePos);
        break;
      default: result = true;
        break;
    }
    return result;
  }
}

