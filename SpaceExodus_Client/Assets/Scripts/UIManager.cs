using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject startMenu;
    public InputField usernameField;
    public InputField hostnameField;
    public Text gameOver;
    public Text userBoard;
    public Text message;

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

    public void ConnectToServer()
    {
        if (Client.instance.ConnectToServer(hostnameField.text) == true)
        {
            startMenu.SetActive(false);
            usernameField.interactable = false;
            hostnameField.interactable = false;
            Destroy(GameObject.Find("Selector"));
            Destroy(GameObject.Find("Destroy"));
        }
    }
    public void TextGameOver(int id)
    {
        string username = GameManager.players[id].username;
        gameOver.text = username + " Won!";
        gameOver.color = new Color(0, 1.0f, 1.0f, 1.0f);
    }
    public void UpdateKillscore(Dictionary<int, PlayerManager> players)
    {
        string content = "";
        foreach (KeyValuePair<int, PlayerManager> p in players)
        {
            content += $"{p.Value.username} : {p.Value.kills} Kills\n";
        }
        userBoard.text = content;
    }
    public void JoinMessage(string username)
    {
        Debug.Log("Check");
        message.text = username + " has joined the game";
        StartCoroutine("ResetMessage");
    }
    public IEnumerator ResetMessage()
    {
        yield return new WaitForSeconds(3.0f);
        message.text = "";
    }

    public void DisconnectMessage(string username)
    {
        message.text = username + " has diconnected";
        StartCoroutine("ResetMessage");
    }

    public void FailedConnectionMessage(string hostname)
    {
        message.text = "Failed to connect to " + hostname;
        StartCoroutine("ResetMessage");
    }
}
