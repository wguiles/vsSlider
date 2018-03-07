using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnerScript : MonoBehaviour {

    public void Respawn(GameObject deadPlayer)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Shootout"))
        {
            Instantiate(deadPlayer, deadPlayer.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(deadPlayer, transform.position, Quaternion.identity); 
        }

    }
}
