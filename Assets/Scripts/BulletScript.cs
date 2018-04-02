using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    // Use this for initialization

    RespawnerScript _respawner;
    ScoreManager _scoreManager;

    public float bulletSpeed;


	void Start () 
    {
        _respawner = GameObject.Find("Respawner").GetComponent<RespawnerScript>();
        _scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();

        if (transform.position.y > 0)
        {
           // bulletSpeed *= -1f;
            Debug.Log("Called");
            Debug.Log(gameObject.tag + "Speed = " + bulletSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Translate(new Vector3(0f, bulletSpeed * Time.deltaTime, 0f));
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            _scoreManager.SubtractPlayerScore(collision.gameObject.tag);
            player.DestroyAndRespawn();
            Destroy(gameObject);
        }
    }

    private string otherPlayer(string playerString)
    {
        if (playerString == "Player")
            return "Player2";
        else
            return "Player";
    }
}
