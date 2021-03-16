using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] inputs = new bool[]
        {
            Input.GetKey(KeyCode.UpArrow),
            Input.GetKey(KeyCode.DownArrow),
            Input.GetKey(KeyCode.LeftArrow),
            Input.GetKey(KeyCode.RightArrow)
        };
        ClientSend.PlayerMovement(inputs);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClientSend.PlayerShooting();
        }
    }
    private void Shooting()
    {
    }
}
