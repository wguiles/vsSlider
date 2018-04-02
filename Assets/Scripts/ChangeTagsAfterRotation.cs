using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTagsAfterRotation : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (transform.rotation.z == 0)
            gameObject.tag = "floor";
        else if (transform.rotation.z == 90)
            gameObject.tag = "wall";
	}
}
