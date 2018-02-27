using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerScript : MonoBehaviour {

    public void Respawn(GameObject deadPlayer)
    {
        Instantiate(deadPlayer, transform.position, Quaternion.identity);
    }
}
