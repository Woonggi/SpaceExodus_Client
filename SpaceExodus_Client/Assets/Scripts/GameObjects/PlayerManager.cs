using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public int weaponLevel = 1;
    public int health;
    public int kills = 0;
    public AudioSource source;
    public AudioClip shoot;
    public AudioClip destroy;
    public Dictionary<int, Bullet> bullets = new Dictionary<int, Bullet>();
    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        shoot = audioSources[0].clip;
        destroy = audioSources[1].clip;
    }
    
    public void PlayShoot()
    {
        source.PlayOneShot(shoot);
    }

    public void PlayDestroy()
    {
        source.PlayOneShot(destroy);
    }

    public void Active(bool active)
    {
        gameObject.GetComponent<Renderer>().enabled = active;
        gameObject.GetComponent<Collider2D>().enabled = active;
    }
}
