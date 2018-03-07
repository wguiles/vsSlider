using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		transform.Translate(new Vector3(0f, -2.0f * Time.deltaTime)); 
        if (transform.position.y < -7.0f)
        {
            Destroy(gameObject); 
        }
	}
}
