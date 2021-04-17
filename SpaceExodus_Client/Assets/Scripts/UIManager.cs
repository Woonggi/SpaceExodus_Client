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
        startMenu.SetActive(false);
        usernameField.interactable = false;
        hostnameField.interactable = false;
        Client.instance.ConnectToServer(hostnameField.text);
        Destroy(GameObject.Find("Selector"));
        Destroy(GameObject.Find("Destroy"));
    }
    public void TextGameOver()
    {
        gameOver.color = new Color(0, 1.0f, 1.0f, 1.0f);
    }
    public void UpdateKillscore(Dictionary<int, PlayerManager> players)
    {
        string content = "";
        foreach (KeyValuePair<int, PlayerManager> p in players)
        {
            content += $"{p.Key}.{p.Value.username} : {p.Value.kills} Kills\n";
        }
        userBoard.text = content;
    }
}
