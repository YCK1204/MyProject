// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct C_CreateRoom : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_24_3_25(); }
  public static C_CreateRoom GetRootAsC_CreateRoom(ByteBuffer _bb) { return GetRootAsC_CreateRoom(_bb, new C_CreateRoom()); }
  public static C_CreateRoom GetRootAsC_CreateRoom(ByteBuffer _bb, C_CreateRoom obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public C_CreateRoom __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int RoomId { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int MemberCount { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<C_CreateRoom> CreateC_CreateRoom(FlatBufferBuilder builder,
      int room_id = 0,
      int member_count = 0) {
    builder.StartTable(2);
    C_CreateRoom.AddMemberCount(builder, member_count);
    C_CreateRoom.AddRoomId(builder, room_id);
    return C_CreateRoom.EndC_CreateRoom(builder);
  }

  public static void StartC_CreateRoom(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddRoomId(FlatBufferBuilder builder, int roomId) { builder.AddInt(0, roomId, 0); }
  public static void AddMemberCount(FlatBufferBuilder builder, int memberCount) { builder.AddInt(1, memberCount, 0); }
  public static Offset<C_CreateRoom> EndC_CreateRoom(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<C_CreateRoom>(o);
  }
}


static public class C_CreateRoomVerify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, uint tablePos)
  {
    return verifier.VerifyTableStart(tablePos)
      && verifier.VerifyField(tablePos, 4 /*RoomId*/, 4 /*int*/, 4, false)
      && verifier.VerifyField(tablePos, 6 /*MemberCount*/, 4 /*int*/, 4, false)
      && verifier.VerifyTableEnd(tablePos);
  }
}
