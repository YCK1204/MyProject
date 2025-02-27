// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct PosInfo : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_24_3_25(); }
  public static PosInfo GetRootAsPosInfo(ByteBuffer _bb) { return GetRootAsPosInfo(_bb, new PosInfo()); }
  public static PosInfo GetRootAsPosInfo(ByteBuffer _bb, PosInfo obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public PosInfo __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Dir Dir { get { int o = __p.__offset(4); return o != 0 ? (Dir)__p.bb.GetUshort(o + __p.bb_pos) : Dir.NONE; } }
  public int X { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Y { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<PosInfo> CreatePosInfo(FlatBufferBuilder builder,
      Dir dir = Dir.NONE,
      int x = 0,
      int y = 0) {
    builder.StartTable(3);
    PosInfo.AddY(builder, y);
    PosInfo.AddX(builder, x);
    PosInfo.AddDir(builder, dir);
    return PosInfo.EndPosInfo(builder);
  }

  public static void StartPosInfo(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddDir(FlatBufferBuilder builder, Dir dir) { builder.AddUshort(0, (ushort)dir, 0); }
  public static void AddX(FlatBufferBuilder builder, int x) { builder.AddInt(1, x, 0); }
  public static void AddY(FlatBufferBuilder builder, int y) { builder.AddInt(2, y, 0); }
  public static Offset<PosInfo> EndPosInfo(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<PosInfo>(o);
  }
}


static public class PosInfoVerify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, uint tablePos)
  {
    return verifier.VerifyTableStart(tablePos)
      && verifier.VerifyField(tablePos, 4 /*Dir*/, 2 /*Dir*/, 2, false)
      && verifier.VerifyField(tablePos, 6 /*X*/, 4 /*int*/, 4, false)
      && verifier.VerifyField(tablePos, 8 /*Y*/, 4 /*int*/, 4, false)
      && verifier.VerifyTableEnd(tablePos);
  }
}
