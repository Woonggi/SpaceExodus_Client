using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLogic : MonoBehaviour
{
    public float randomTimer;
    private int mod = 1;
    // Start is called before the first frame update
    void Start()
    {
        randomTimer = Random.Range(5f, 10f); 
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = GetComponent<SpriteRenderer>().color.a;

        if (alpha > 1)
        {
            mod *= -1; 
        }

        else if (alpha < 0)
        {
            mod *= -1;
        }

        alpha = (mod * randomTimer * Time.deltaTime * 30f) / 255f; 

        GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, alpha);
    }
}
