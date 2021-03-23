using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool shoot = false;
    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot = true;
        }
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
        if (shoot == true) 
        {
            Debug.Log("SHOOT");
            ClientSend.PlayerShooting();
            shoot = false;
        }
    }
    private void Shooting()
    {
    }
}
