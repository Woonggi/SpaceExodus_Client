using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    int id;
    public AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayDestroy()
    {
        source.PlayOneShot(source.clip);
    }
}
