using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData (CustomPacket packet)
    {
        packet.WriteLength();
        Client.instance.tcp.SendData(packet);
    }

    private static void SendUDPData (CustomPacket packet)
    {
        packet.WriteLength();
        Client.instance.udp.SendData(packet);
    }

    public static void WelcomeReceived()
    {
        using (CustomPacket packet = new CustomPacket((int)ClientPackets.CP_WELCOME_RECEIVED))
        {
            packet.Write(Client.instance.myId);
            packet.Write(UIManager.instance.usernameField.text);
            SendTCPData(packet);
        }
    }
    public static void PlayerMovement(bool[] inputs)
    {
        using (CustomPacket packet = new CustomPacket((int)ClientPackets.CP_PLAYER_MOVEMENT))
        {
            packet.Write(inputs.Length);
            foreach(bool input in inputs)
            {
                packet.Write(input);
            }
            // TEST!
            packet.Write(GameManager.players[Client.instance.myId].transform.rotation.eulerAngles.z);
            SendUDPData(packet);
        }
    }

    public static void PlayerShooting()
    {
        using (CustomPacket packet = new CustomPacket((int)ClientPackets.CP_PLAYER_SHOOTING))
        {
            packet.Write(Client.instance.myId);
            SendTCPData(packet);
        }
    }

}
