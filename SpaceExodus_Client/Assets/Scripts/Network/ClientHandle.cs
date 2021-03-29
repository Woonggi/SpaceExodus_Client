using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    private static int lastFramePos = -1;
    private static int lastFrameRot = -1;
    private static float lastRot = 0.0f;
    public static void Welcome(CustomPacket packet)
    {
        string msg = packet.ReadString();
        int myId = packet.ReadInt();

        Debug.Log($"Message from server: {msg}");
        Client.instance.myId = myId;

        ClientSend.WelcomeReceived();
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(CustomPacket packet)
    {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(id, username, position, rotation);
    }

    public static void PlayerPosition(CustomPacket packet)
    {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        int currFrame = packet.ReadInt();
        GameManager.players[id].transform.position = position; 
        lastFramePos = Mathf.Max(lastFramePos, currFrame);
    }
    public static void PlayerRotation(CustomPacket packet)
    {
        int id = packet.ReadInt();
        float rotation = packet.ReadFloat();
        int currFrame = packet.ReadInt();
        Vector3 target = new Vector3(0, 0, rotation);
        GameManager.players[id].transform.Rotate(target);//+= target;
    }

    public static void PlayerShooting(CustomPacket packet)
    {
        int id = packet.ReadInt();
        Vector3 position = GameManager.players[id].transform.position;
        Quaternion rotation = GameManager.players[id].transform.rotation;
        GameManager.instance.SpawnBullet(id, position, rotation);
    }
}
