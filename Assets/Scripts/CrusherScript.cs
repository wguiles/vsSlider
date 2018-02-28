using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherScript : MonoBehaviour 
{

    [SerializeField]
    private float start;

    [SerializeField]
    private float end;

    [SerializeField]
    private float crusherSpeed;

    [SerializeField]
    private float waitTime;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, start, transform.position.z);
    }
	// Update is called once per frame
	void Update () 
    {
        Debug.DrawLine(new Vector2(transform.position.x, start), new Vector2(transform.position.x, end));

        transform.Translate(new Vector3(0f, crusherSpeed) * Time.deltaTime);

        if (transform.position.y < end)
        {
            transform.position = new Vector3(transform.position.x, end, transform.position.z);
            StartCoroutine(ChangeDirection());
        }
        else if(transform.position.y > start)
        {
            transform.position = new Vector3(transform.position.x, start, transform.position.z);
            StartCoroutine(ChangeDirection());
        }
	}

    IEnumerator ChangeDirection()
    {
        float initialSpeed = crusherSpeed;
        crusherSpeed = 0f;
        yield return new WaitForSeconds(waitTime);
        crusherSpeed = initialSpeed * -1f;
    }
}
