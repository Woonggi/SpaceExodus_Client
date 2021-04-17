using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particle;
    public int numParticle;

    public void SpawnParticle()
    {
        for (int i = 0; i < numParticle; ++i)
        {
            GameObject p = Instantiate(particle, transform.position, Quaternion.identity);
            Vector2 randomVel = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            float angle = Random.Range(0.0f, 360.0f);
            float speed = Random.Range(150, 200);
            p.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed);
        }
    }
}
