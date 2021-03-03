using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{ 
    public static void Welcome(CustomPacket packet)
    {
        // keep the way of reading data identical with the server.
        string msg = packet.ReadString();
        int id = packet.ReadInt();

        Debug.Log($"message from server: {msg}");
        Client.instance.myId = id;
        ClientSend.WelcomeReceived();
    }
}
