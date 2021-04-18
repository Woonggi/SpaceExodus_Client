using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAsteroid : MonoBehaviour
{
    public float rotationSpeed; 
    public string sceneName; 

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<ExplosionEffect>().SpawnParticle();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        if (sceneName == "Exit")
        {
            Application.Quit();
        }
        StartCoroutine("SceneChange");
        GetComponent<AudioSource>().Play();
    }

    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
