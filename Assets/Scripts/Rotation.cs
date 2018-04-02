using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    public float t = 1;
    public float startAngle;
    public float targetAngle;

    float currentRotation;

    private void Start()
    {
        StartCoroutine(RotateTimer());
    }

    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(startAngle, targetAngle, t));

        if (t < 1)
        {
            t += 0.05f;
        }
        else
        {
            t = 1;
        }

    }

    private void RotateLeft()
    {
        if ((int)transform.eulerAngles.z % 180 == 0)
        {
            //set the target angle to the current one minus 90 degrees
            t = 0;
            startAngle = transform.eulerAngles.z;
            targetAngle = transform.eulerAngles.z - 180;

        }
    }
    private void RotateRight()
    {

        if ((int)transform.eulerAngles.z % 180 == 0)
        {
            //set the target angle to the current one plus 90 degrees
            t = 0;
            startAngle = transform.eulerAngles.z;
            targetAngle = transform.eulerAngles.z + 180;
        }

    }

    IEnumerator RotateTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(10f);
            int randomNum = Random.Range(0, 1);

            if (randomNum == 0)
                RotateLeft();
            else if (randomNum == 1)
                RotateRight();
        }
    }
}