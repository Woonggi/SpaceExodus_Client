﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
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

        // needed to be deleted
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(CustomPacket packet)
    {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector();
        float rotation = packet.ReadFloat();
        GameManager.instance.SpawnPlayer(id, username, position, rotation);
    }

    public static void PlayerPosition(CustomPacket packet)
    {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector();
        GameManager.players[id].transform.position = position;
    }
    public static void PlayerRotation(CustomPacket packet)
    {
        int id = packet.ReadInt();
        float rotation = packet.ReadFloat();
        Debug.Log(rotation);
        GameManager.players[id].GetComponent<Rigidbody2D>().rotation = rotation;
    }
    
}
