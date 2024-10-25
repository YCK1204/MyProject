using Google.FlatBuffers;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public static void C_TestHandler(PacketSession session, ByteBuffer buffer)
    {
        ServerSession serverSession = session as ServerSession;
        var test = C_Test.GetRootAsC_Test(buffer);
        Debug.Log(test.Test);
    }
}
