using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlayers : PlayerScript 
{

    public bool isSpinning;

    private float speedY = 5.0f;


    public new void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    public new void Update () 
    {

        base.Update();

        if (isInvincible)
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        else
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            

        if (transform.position.y > 5.5f)
        {
            transform.position = new Vector3(transform.position.x, -5.5f, transform.position.z);
        }
        else if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);
        }

		if(GetIsAttacking())
        {
            transform.Rotate(new Vector3(0f, 0f, 100f));
        }
        else if (!stunned)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f)) ;
        }
	}


    public override void Jumping()
    {
        if(Input.GetKeyDown(jumpKey))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, jumpForce), ForceMode2D.Impulse);
        }
    }

    IEnumerator Spin()
    {
        Quaternion tempRotation = Quaternion.identity;
        isSpinning = true;
        yield return new WaitForSeconds(3.0f);
        isSpinning = false;
        transform.rotation = tempRotation;
    }











}
