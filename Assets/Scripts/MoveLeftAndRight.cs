using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftAndRight : MonoBehaviour {


    [SerializeField]
    private float start;

    [SerializeField]
    private float end;

    [SerializeField]
    private float speed;

	
    void Start()
    {
    }
	// Update is called once per frame
	void Update () 
    {
        transform.Translate( new Vector3(speed * Time.deltaTime, 0f));

        if (transform.position.x > end)
        {
            transform.position = new Vector3(end, transform.position.y);
            speed *= -1f;
        }
        else if (transform.position.x < start)
        {
            transform.position = new Vector3(start, transform.position.y);
            speed *= -1f;
        }
	}
}
