using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource source;
    public AudioClip hit;
    public AudioClip shoot;
    public AudioClip destroyPlayer;
    public AudioClip destroyAsteroid;
    public AudioClip powerUp;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Hit()
    {
        source.PlayOneShot(hit);
    }
    public void Shoot()
    {
        source.PlayOneShot(shoot); 
    }
    public void DestroyPlayer()
    {
        source.PlayOneShot(destroyPlayer); 
    }
    public void DestroyAsteroid()
    { 
        source.PlayOneShot(destroyAsteroid); 
    }
    public void PowerUP()
    {
        source.PlayOneShot(powerUp);
    }
}
