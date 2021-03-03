using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096; // 4kb

    public string ip = "127.0.0.1";
    public int port = 3100;
    public int myId = 0;
    public TCP tcp;

    private delegate void PacketHandler(CustomPacket packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    private void Start()
    {
        tcp = new TCP();
    }
    public void OnConnectedToServer()
    {
        InitializeClientData();
        tcp.Connect();
    }

    public class TCP
    {
        public TcpClient socket;
        private CustomPacket receivedData;
        private NetworkStream stream;
        private byte[] recvBuffer;

        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };
            recvBuffer = new byte[dataBufferSize];
            socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
        }

        public void SendData(CustomPacket packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"Error sending data to server {ex}");
            }
        }

        private void ConnectCallback(IAsyncResult result)
        {
            socket.EndConnect(result);
            if (!socket.Connected)
            {
                return;
            }
            stream = socket.GetStream();
            receivedData = new CustomPacket();
            stream.BeginRead(recvBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int byteLength = stream.EndRead(result);
                if(byteLength <= 0)
                {
                    // TODO : disconnect
                    return;
                }
                byte[] data = new byte[dataBufferSize];
                Array.Copy(recvBuffer, data, byteLength);

                // TCP guarantees all packets arrival in order but doesn't guarantee it comes at once.
                receivedData.Reset(HandleData(data));
                stream.BeginRead(recvBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch
            {
                // TODO: disconnect

            }
        }

        private bool HandleData(byte[] data)
        {
            int packetLength = 0;
            receivedData.SetBytes(data);
            // When the bytes that contain length data, 
            if (receivedData.UnreadLength() >= 4)
            {
                // Read length.
                packetLength = receivedData.ReadInt();
                Debug.Log(packetLength);
                if (packetLength <= 0)
                {
                    return true; // Reset the data. We don't have to read.
                }
            }

            while (packetLength > 0 && packetLength <= receivedData.UnreadLength())
            {
                byte[] packetBytes = receivedData.ReadBytes(packetLength);
                // Action
                ThreadManager.ExecuteOnMainThread(() => 
                {
                    using (CustomPacket packet = new CustomPacket(packetBytes))
                    {
                        int packetId = packet.ReadInt();
                        Debug.Log(packetId);
                        packetHandlers[packetId](packet);
                    }
                });
                packetLength = 0;
                if (receivedData.UnreadLength() >= 4)
                {
                    // Read length.
                    packetLength = receivedData.ReadInt();
                    if (packetLength <= 0)
                    {
                        return true; // Reset the data. We don't have to read.
                    }
                }
            }
            if (packetLength <= 1)
            {
                return true; 
            }
            
            // Still left partial packets to read.
            return false;
        }
    }

    private void InitializeClientData()
    {
        // Initial packet will be welcome packet.
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int)ServerPackets.SP_WELCOME, ClientHandle.Welcome }
        };
        Debug.Log("Initialized packets.");
    }
}
