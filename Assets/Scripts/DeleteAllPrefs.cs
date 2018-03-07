using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAllPrefs : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        PlayerPrefs.SetInt("p1Score", 0); 
        PlayerPrefs.SetInt("p2Score", 0); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
