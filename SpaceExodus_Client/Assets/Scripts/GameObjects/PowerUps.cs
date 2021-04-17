using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private void Update()
    {
        float[] random = { Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f) };
        Color randomColor = new Color(random[0], random[1], random[2]);
        gameObject.GetComponent<SpriteRenderer>().color = randomColor; 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject; 
        if (otherObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
