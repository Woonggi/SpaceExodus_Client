using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGeneration : MonoBehaviour
{
    public float minX, maxX;
    public float minY, maxY;
    public float maxZ;
    public int numStars;
    public GameObject starPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numStars; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(0.0f, maxZ));
            GameObject start = Instantiate(starPrefab, randomPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
