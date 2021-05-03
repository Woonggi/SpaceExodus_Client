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

        GameManager.instance.goalKillScore = packet.ReadInt();
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
        int index = packet.ReadInt();
        Vector3 position = GameManager.players[id].transform.position;
        Quaternion rotation = GameManager.players[id].transform.rotation;
        GameManager.instance.SpawnBullet(id, index, position, rotation);
    }

    public static void BulletPosition(CustomPacket packet)
    {
        int id = packet.ReadInt();
        int index = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        GameManager.instance.BulletPosition(id, index, position);
    }

    public static void BulletDestroy(CustomPacket packet)
    {
        int id = packet.ReadInt();
        int index = packet.ReadInt();
        GameManager.instance.BulletDestroy(id, index);
    }

    public static void PlayerDisconnected(CustomPacket packet)
    {
        int id = packet.ReadInt();
        UIManager.instance.DisconnectMessage(GameManager.players[id].username);
        Destroy(GameManager.players[id].gameObject);
        GameManager.players.Remove(id);  
    }

    public static void PlayerHit(CustomPacket packet)
    {
        // id that shoot
        int id = packet.ReadInt();
        int health = packet.ReadInt();
        GameManager.players[id].health = health;
        Debug.Log(GameManager.players[id].GetComponent<PlayerManager>().health);
    }

    public static void PlayerDestroy(CustomPacket packet)
    {
        int id = packet.ReadInt();
        int killerId = packet.ReadInt();
        Debug.Log($"player {killerId} killed {id}");
        GameManager.instance.DestroyPlayer(id, killerId);
    }
    public static void PlayerRespawn(CustomPacket packet)
    {
        int id = packet.ReadInt();
        int health = packet.ReadInt();
        Vector3 spawnPosition = packet.ReadVector3();
        GameManager.instance.RespawnPlayer(id, health, spawnPosition);
    }
    public static void GameOver(CustomPacket packet)
    {
        int id = packet.ReadInt();
        Debug.Log($"player {id} has won the game!");
        GameManager.instance.GameOver(id);
    }
    public static void SpawnPowerUp(CustomPacket packet)
    {
        Vector3 position = packet.ReadVector3();
        GameManager.instance.SpawnPowerUp(position);
    }
    public static void PowerUp(CustomPacket packet)
    {
        int id = packet.ReadInt();
        int weaponLevel = packet.ReadInt();
        GameManager.instance.PowerUp(id, weaponLevel);
    }

    public static void SpawnAsteroid(CustomPacket packet)
    {
        int id = packet.ReadInt();
        int type = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();
        Vector3 scale = packet.ReadVector3();
        GameManager.instance.SpawnAsteroid(id, type, position, rotation, scale); 
    }
    public static void AsteroidPosition(CustomPacket packet)
    {
        int id = packet.ReadInt();
        int type = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        GameManager.instance.AsteroidPosition(id, type, position);
    }

    public static void AsteroidDestroy(CustomPacket packet)
    {
        int id = packet.ReadInt();
        GameManager.instance.DestroyAsteroid(id);
    }
}
