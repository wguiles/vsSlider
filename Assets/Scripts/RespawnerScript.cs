using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnerScript : MonoBehaviour 
{
    
    public Vector3 player1Respawn;
    public Vector3 player2Respawn;

    public GameObject player1;
    public GameObject player2;

    public float respawnTime;

    public void startRespawn()
    {
        StartCoroutine(Respawn());
    }

    public IEnumerator Respawn ()
    {
        
        yield return new WaitForSeconds(respawnTime);

        GameObject playerToSpawn = null;

        if (GameObject.FindWithTag("Player") == null)
            playerToSpawn = player1;
        else if (GameObject.FindWithTag("Player2") == null)
            playerToSpawn = player2;


        Rigidbody2D playerRigidbody = playerToSpawn.GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerToSpawn = Instantiate(playerToSpawn, getRespawnPosition(playerToSpawn.tag), Quaternion.identity);


        yield return new WaitForSeconds(2.0f);

        playerRigidbody = playerToSpawn.GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.None;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;


    }

    public Vector3 getRespawnPosition(string player)
    {
        
        if (player == "Player")
            return player2Respawn;
        else if (player == "Player2")
            return player1Respawn;
        else
            return new Vector3(0f, 0f, 0f);
        
    }
}
