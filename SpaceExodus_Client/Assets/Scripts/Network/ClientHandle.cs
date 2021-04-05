using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    private static int lastFramePos = -1;
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
        int maxHealth = packet.ReadInt();

        GameManager.instance.SpawnPlayer(id, username, position, rotation, maxHealth);
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
        Quaternion rotation = packet.ReadQuaternion();
        int currFrame = packet.ReadInt();
        GameManager.players[id].transform.rotation = rotation;
    }

    public static void PlayerShooting(CustomPacket packet)
    {
        int id = packet.ReadInt();
        Vector3 position = GameManager.players[id].transform.position;
        Quaternion rotation = GameManager.players[id].transform.rotation;
        GameManager.instance.SpawnBullet(id, position, rotation);
    }

    public static void PlayerDisconnected(CustomPacket packet)
    {
        int id = packet.ReadInt();
        Destroy(GameManager.players[id].gameObject);
        GameManager.players.Remove(id);  
    }

    public static void PlayerHit(CustomPacket packet)
    {
        // id that shoot
        int id = packet.ReadInt();
        int health = packet.ReadInt();
        GameManager.players[id].GetComponent<PlayerManager>().health = health;
        Debug.Log(GameManager.players[id].GetComponent<PlayerManager>().health);
    }

    public static void PlayerDestroy(CustomPacket packet)
    {
        int id = packet.ReadInt();
        // TODO: need to be temp destroy in some way.
        Destroy(GameManager.players[id].gameObject);

    }
}
