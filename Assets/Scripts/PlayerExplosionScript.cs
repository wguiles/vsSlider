using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosionScript : MonoBehaviour {


    public Vector3 direction;

	private void Start()
	{
        StartCoroutine(DestroyAfterSpawn());
	}

	void Update () 
    {
        transform.Translate(direction * 10f * Time.deltaTime);
	}

    IEnumerator DestroyAfterSpawn()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }
}
