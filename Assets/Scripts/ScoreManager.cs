using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


    private int totalSceneKills;

    [SerializeField]
    private int killLimit;

    [SerializeField]
    private string nextScene;

    public Text player1text;
    public Text player2text;

    private int player1Score;
    private int player2Score;

	void Start () 
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main"))
        {
            PlayerPrefs.SetInt("p1Score", 0);
            PlayerPrefs.SetInt("p2Score", 0);
        }
        player1Score = PlayerPrefs.GetInt("p1Score", 0);
        player2Score = PlayerPrefs.GetInt("p2Score", 0);
        totalSceneKills = 0;
        UpdateScore();
	}

    void AddSceneKills()
    {
        totalSceneKills++;

        if (totalSceneKills == killLimit)
            SwitchScene();
    }

    public void AddPlayerScore(string sentPlayer)
    {
        if (sentPlayer == "Player")
            player1Score++;
        else if (sentPlayer == "Player2")
            player2Score++;

        UpdateScore();
        AddSceneKills();

    }

    public void SubtractPlayerScore(string sentPlayer)
    {
        if (sentPlayer == "Player" && player1Score > 0)
            player1Score--;
        else if (sentPlayer == "Player2" && player2Score > 0)
            player2Score--;

        UpdateScore();
        AddSceneKills();

    }

    void SwitchScene()
    {
        if (nextScene == null)
            nextScene = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetInt("p1Score", player1Score);
        PlayerPrefs.SetInt("p2Score", player2Score);

        SceneManager.LoadScene(nextScene);
    }

    void UpdateScore()
    {
        player1text.text = player1Score.ToString();
        player2text.text = player2Score.ToString();
    }
	
}
