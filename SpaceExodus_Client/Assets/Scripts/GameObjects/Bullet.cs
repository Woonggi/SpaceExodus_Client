using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletId;
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject.tag == "Player" && otherObject.GetComponent<PlayerManager>().id != bulletId)
        {
            Destroy(gameObject); 
        } 
    }
}
