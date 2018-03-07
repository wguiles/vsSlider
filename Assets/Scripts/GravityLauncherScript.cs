using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityLauncherScript : MonoBehaviour {

    public float launchForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Rigidbody2D _playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            _playerRigidBody.AddForce(new Vector2(0f, launchForce), ForceMode2D.Impulse);
        }
    }
}
