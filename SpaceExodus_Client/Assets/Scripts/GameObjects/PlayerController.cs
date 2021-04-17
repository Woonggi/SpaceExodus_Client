using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool shoot = false;
    private void FixedUpdate()
    {
        if (GameManager.instance.isGameover == false)
        {
            SendInputToServer();
        }
    }

    private void Update()
    {
        if (GameManager.instance.isGameover == false)
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot = true;
        }

        // Cheat code
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClientSend.GameOver(gameObject.GetComponent<PlayerManager>().id);
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
            ClientSend.PlayerShooting();
            shoot = false;
        }
    }
}
