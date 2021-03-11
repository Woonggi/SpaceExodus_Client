using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    bool isStarted = false;
    GameObject myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        int id = Client.instance.myId;
        myPlayer = GameManager.players[id].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isStarted == true)
        {  
            transform.position = myPlayer.transform.position;
        }
    }

}
