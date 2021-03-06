using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(CustomPacket packet)
    {
        packet.WriteLength();
        Client.instance.tcp.SendData(packet);
    }

    private static void SendUDPData(CustomPacket packet)
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

    public static void UDPTestReceived()
    {
        using (CustomPacket packet = new CustomPacket((int)ClientPackets.UDP_RECEIVED))
        {
            packet.Write("Received a UDP Packet");
            SendUDPData(packet);
        }
    }
}
