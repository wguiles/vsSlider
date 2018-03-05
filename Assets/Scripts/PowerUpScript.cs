using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
		
	}

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            StartCoroutine(player.AddBoost(3.0f, 10f)); 
        }

        Destroy(gameObject); 
    }
}
