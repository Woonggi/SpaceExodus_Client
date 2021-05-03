using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource[] sources;
    public AudioClip hit;
    public AudioClip shoot;
    public AudioClip destroyPlayer;
    public AudioClip destroyAsteroid;
    public AudioClip powerUp;
    void Start()
    {
        sources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    public void Hit()
    {
        sources[0].PlayOneShot(hit);
    }
    public void Shoot()
    {
        sources[1].PlayOneShot(shoot); 
    }
    public void DestroyPlayer()
    {
        sources[2].PlayOneShot(destroyPlayer); 
    }
    public void DestroyAsteroid()
    {
        sources[3].PlayOneShot(destroyAsteroid); 
    }
    public void PowerUP()
    {
        sources[4].PlayOneShot(powerUp);
    }
}
