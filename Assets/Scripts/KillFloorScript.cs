using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloorScript : MonoBehaviour 
{

    RespawnerScript _respawner;

    ScoreManager _scoreManager;

    private void Start()
	{
        _respawner = GameObject.Find("Respawner").GetComponent<RespawnerScript>();
        _scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            PlayerScript killedPlayer = collision.gameObject.GetComponent<PlayerScript>();
            killedPlayer.DestroyAndRespawn();
        }
	}
}
