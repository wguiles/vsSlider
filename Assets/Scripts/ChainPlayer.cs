using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainPlayer : PlayerScript 
{


    public override IEnumerator AddBoost(float multipier, float seconds)
	{
        if (!isAttacking && !stunned)
        {
            isAttacking = true;
            _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
            _rigidbody.AddForce(new Vector2(16f * Mathf.Sign(Input.GetAxisRaw(playerAxis)), _rigidbody.velocity.y), ForceMode2D.Impulse);
            _renderer.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(seconds);
            _renderer.color = thisColor;
            chargeMultiplier = 1.0f;
            isAttacking = false;
        }


             
	}

	public override void Movement()
    {
        
    }






}
