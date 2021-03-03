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

    public static void WelcomeReceived()
    {
        using (CustomPacket packet = new CustomPacket((int)ClientPackets.CP_WELCOME_RECEIVED))
        {
            packet.Write(Client.instance.myId);
            packet.Write(UIManager.instance.usernameField.text);
            SendTCPData(packet);
        }
    }
}
