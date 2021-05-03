using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, Asteroid> asteroids = new Dictionary<int, Asteroid>();
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject[] projectilePrefab;
    public GameObject[] asteroidPrefabs;
    public GameObject powerUpsPrefab;
    public float projectileSpeed;
    public bool isGameover = true;

    public int maxHealth;
    public int goalKillScore;
    public float respawnTime;

    // Audio Clips.
    private AudioManager audioManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
    }

    public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation, int maxHealth)
    {
        GameObject player;
        if (id == Client.instance.myId)
        {
            player = Instantiate(localPlayerPrefab, position, rotation);
        }
        else
        {
            player = Instantiate(playerPrefab, position, rotation);
        }
        player.GetComponent<PlayerManager>().id = id;
        player.GetComponent<PlayerManager>().username = username;
        player.GetComponent<PlayerManager>().health = maxHealth;
        players.Add(id, player.GetComponent<PlayerManager>());
        UIManager.instance.JoinMessage(username);
        UIManager.instance.UpdateKillscore(players);
    }

    public void SpawnBullet(int id, int index, Vector3 position, Quaternion rotation)
    {
        int weaponLevel = players[id].weaponLevel;
        GameObject projectile = Instantiate(projectilePrefab[weaponLevel - 1], position, rotation);
        projectile.GetComponent<Bullet>().bulletId = id;
        players[id].bullets.Add(index, projectile.GetComponent<Bullet>());
        audioManager.Shoot();
    }

    public void BulletPosition(int id, int index, Vector3 position)
    {
        if (players[id].bullets.ContainsKey(index))
        {
            if (players[id].bullets[index] != null)
            {
                players[id].bullets[index].transform.position = position;
            }
        }
    }

    public void BulletDestroy(int id, int index)
    {
        if (players[id].bullets.ContainsKey(index))
        {
            if (players[id].bullets[index] != null)
            {
                audioManager.Hit();
                Destroy(players[id].bullets[index].gameObject);
                players[id].bullets.Remove(index);
            }
        }
    }

    public void DestroyPlayer(int killed, int killer)
    {
        players[killed].Active(false);
        if (killer != 0)
        {
            players[killer].kills++;
            if (players[killer].kills == goalKillScore)
            {
                GameOver(killer);
            }
        }
        players[killed].weaponLevel = 1;
        audioManager.DestroyPlayer();
        players[killed].GetComponent<ExplosionEffect>().SpawnParticle();
        UIManager.instance.UpdateKillscore(players);
    }
    public void RespawnPlayer(int id, int health, Vector3 spawnPosition)
    {
        players[id].Active(true);
        players[id].health = health;
        players[id].transform.position = spawnPosition;
    }
    public void GameOver(int killer)
    {
        isGameover = true;        
        UIManager.instance.TextGameOver(killer);
        ClientSend.GameOver(killer);
        StartCoroutine("GoBackToMenu");
    }
    
    public IEnumerator GoBackToMenu()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Menu");
    }
    public void PowerUp(int id, int weaponLevel)
    {
        audioManager.PowerUP();
        players[id].weaponLevel = weaponLevel;
    }
    public void SpawnPowerUp(Vector3 position)
    {  
        Instantiate(powerUpsPrefab, position, Quaternion.identity);
    }
    public void SpawnAsteroid(int id, int type, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Asteroid asteroid = Instantiate(asteroidPrefabs[type], position, rotation).GetComponent<Asteroid>();
        asteroids.Add(id, asteroid);
    }
    public void AsteroidPosition(int id, int type, Vector3 position)
    {
        if (asteroids.ContainsKey(id) == false)
        { 
            Asteroid asteroid = Instantiate(asteroidPrefabs[type], position, Quaternion.identity).GetComponent<Asteroid>();
            asteroids.Add(id, asteroid); 
        }
        if (asteroids[id] != null)
        {
            asteroids[id].transform.position = position;
        }
    }

    public void DestroyAsteroid(int id)
    {
        if (asteroids.ContainsKey(id))
        {
            if (asteroids[id] != null)
            {
                audioManager.DestroyAsteroid();
                asteroids[id].gameObject.GetComponent<ExplosionEffect>().SpawnParticle();
                Destroy(asteroids[id].gameObject);
            }
        }
    }
}
