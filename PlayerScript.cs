using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D _rigidbody;

    private SpriteRenderer _renderer;

    public float speed;

    public float chargeMultiplier = 1.0f;

    public float jumpForce;

    private bool canJump = true;



    [SerializeField]
    private string playerAxis;

    [SerializeField]
    private KeyCode jumpKey;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //never ending movement
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody.velocity = new Vector2(speed * chargeMultiplier, _rigidbody.velocity.y);

        // jumping

        if (Input.GetKeyDown(jumpKey) && canJump)
        {
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }
            

        // change direction and boost
        if (Input.GetButtonDown(playerAxis))
        {
            if (Input.GetAxisRaw(playerAxis) / _rigidbody.velocity.x < 0)
                changeDirection();
            else if (Input.GetAxisRaw(playerAxis) / _rigidbody.velocity.x > 0)
                StartCoroutine(AddBoost());
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            changeDirection();
        }
        else if (collision.gameObject.tag == "floor")
        {
            canJump = true;
        }
    }

    private void changeDirection()
    {
         
        speed *= -1f;
    }

    private IEnumerator AddBoost()
    {
        if (chargeMultiplier == 1.0f)
        {
            chargeMultiplier = 2.0f;
            _renderer.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.35f);
            _renderer.color = new Color(255, 255, 255);
            chargeMultiplier = 1.0f;
        }

    }

}
