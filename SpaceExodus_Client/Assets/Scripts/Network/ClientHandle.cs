using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
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
        GameManager.players[id].transform.position = position;
    }
    public static void PlayerRotation(CustomPacket packet)
    {
        int id = packet.ReadInt();
        // TEST!
        // Quaternion rotation = packet.ReadQuaternion();
        float rotation = packet.ReadFloat();
        Debug.Log(rotation);
        // BUG: Ship is shaking while rotating sometimes. Possible reason is the UDP packet order.
        // For now, to minimize shaking, cut out the decimal parts as much as I can.
        Quaternion target = Quaternion.Euler(0, 0, (float)Mathf.RoundToInt(rotation));
        GameManager.players[id].transform.rotation = target; 
    }

    public static void PlayerShooting(CustomPacket packet)
    {
        int id = packet.ReadInt();
        Vector3 position = GameManager.players[id].transform.position;
        Quaternion rotation = GameManager.players[id].transform.rotation;
        GameManager.instance.SpawnBullet(id, position, rotation);

    }
}
