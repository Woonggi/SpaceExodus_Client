using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        UIManager.instance.UpdateKillscore(players);
    }

    public void SpawnBullet(int id, Vector3 position, Quaternion rotation)
    {
        int weaponLevel = players[id].weaponLevel;
        GameObject projectile = Instantiate(projectilePrefab[weaponLevel - 1], position, rotation);
        projectile.GetComponent<Bullet>().bulletId = id;
        float heading = rotation.eulerAngles.z + 90.0f;
        Vector3 direction = new Vector3(Mathf.Cos(heading * Mathf.Deg2Rad), Mathf.Sin(heading * Mathf.Deg2Rad), 0.0f);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    public void DestroyPlayer(int killed, int killer)
    {
        players[killed].Active(false);
        players[killer].kills++;
        if (players[killer].kills == goalKillScore)
        {
            GameOver(killer);
        }
        SpawnPowerUps(players[killed]);
        players[killed].GetComponent<ExplosionEffect>().SpawnParticle();
        UIManager.instance.UpdateKillscore(players);
        Debug.Log($"player {killer} killed player {killed}");
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
        UIManager.instance.TextGameOver();
    }
    public void PowerUp(int id, int weaponLevel)
    {
        players[id].weaponLevel = weaponLevel;
    }
    private void SpawnPowerUps(PlayerManager player)
    {
        Instantiate(powerUpsPrefab, player.transform.position, Quaternion.identity);
    }
    public void SpawnAsteroid(int id, int type, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Asteroid asteroid = Instantiate(asteroidPrefabs[type], position, rotation).GetComponent<Asteroid>();
        asteroids.Add(id, asteroid);
        Debug.Log($"Spawn : {id}");
    }
    public void AsteroidPosition(int id, int type, Vector3 position)
    {
        if (asteroids.ContainsKey(id) == false)
        { 
            Asteroid asteroid = Instantiate(asteroidPrefabs[type], position, Quaternion.identity).GetComponent<Asteroid>();
            asteroids.Add(id, asteroid); 
        }
        asteroids[id].transform.position = position;
    }

    public void DestroyAsteroid(int id)
    {
        asteroids[id].gameObject.GetComponent<ExplosionEffect>().SpawnParticle();
        Debug.Log($"Destroy: {id}");
        Destroy(asteroids[id].gameObject);
        //asteroids.Remove(id);    
    }

}
