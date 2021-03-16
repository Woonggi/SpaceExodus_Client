using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject thisPlayer;
    bool playerInitialized = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Client.instance.isConnected == true)
        {
            if (playerInitialized == false)
            {
                InitializeTarget();
            }
            transform.position = new Vector3(thisPlayer.transform.position.x,
                thisPlayer.transform.position.y,
                transform.position.z); 
        }
    }
    void InitializeTarget()
    {
        int id = Client.instance.myId;
        thisPlayer = GameManager.players[id].gameObject;
        playerInitialized = true;
    }
}
