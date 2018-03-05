using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{

    public GameObject m_oneWayFloor;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnFloors());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnFloors()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
           GameObject tempFloor = Instantiate(m_oneWayFloor, new Vector2(Random.Range(-5.88f, 5.88f), transform.position.y), Quaternion.identity);
            tempFloor.transform.localScale = new Vector3(Random.Range(1.0f, 3.0f), transform.localScale.y);
        }
    }
}
