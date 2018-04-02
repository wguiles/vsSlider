using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityLauncherScript : MonoBehaviour {


    public float tempJumpForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Player2")
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
             tempJumpForce = player.jumpForce;
            player.initalJumpForce = tempJumpForce;
            player.jumpForce *= 1.5f;
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Player2")
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            player.jumpForce = tempJumpForce;
        }
    }
}
