using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloorScript : MonoBehaviour {


    public float waitTime;

    private bool isMoving = false;

	void Start () 
    {
        StartCoroutine(StartMoving());
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isMoving)
            transform.Translate(new Vector3(-1.0f * Time.deltaTime, 0f, 0f));
	}

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(waitTime);
        isMoving = true;
    }
}
