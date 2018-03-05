using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D _rigidbody;

    private SpriteRenderer _renderer;

    public float speed;

    public float chargeMultiplier = 1.0f;

    public float jumpForce;

    public GameObject tempWall;

    private ScoreManager _scoreManager;
    private RespawnerScript _respawner;

    [SerializeField]
    private KeyCode placeWall;

    [SerializeField]
    private KeyCode changeDirection;

    [SerializeField]
    private KeyCode changeDirection2;



    private bool canJump = true;

    private bool usingAction;

    private int score;


    [SerializeField]
    private Color thisColor;

    [SerializeField]
    private int actionsLeft;

    [SerializeField]
    private float cooldownTime;

    [SerializeField]
    private string playerAxis;

    [SerializeField]
    private KeyCode jumpKey;

    private bool isAttacking;

    private bool isInvincible;

    private bool stunned;




    void Start()
    {
        chargeMultiplier = 1.0f;
        isAttacking = false;
        stunned = false;
        actionsLeft = 3;
        _respawner = GameObject.Find("Respawner").GetComponent<RespawnerScript>();
        _scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = thisColor;
        StartCoroutine(Invincible());
    }



    // Update is called once per frame
    void Update()
    {
        if (!stunned)
        {
            Actions();
        }
        Jumping();
        Movement();


       // PacmanEffect();
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(jumpKey) && canJump)
        {
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void Movement()
    {
        _rigidbody.velocity = new Vector2(speed * chargeMultiplier, _rigidbody.velocity.y);
    }

    private void Actions()
    {
        
        // First control scheme

        //if (actionsLeft > 0)
        //{
        //    if (Input.GetKeyDown(changeDirection) || Input.GetKeyDown(changeDirection2))
        //    {
        //        ChangeDirection();

        //        if (canJump && !isAttacking)
        //            PlaceWall();

        //        actionsLeft--;
        //        StartCoroutine(ActionCooldown());
        //    }
        //    else if (Input.GetKeyDown(dash) && !isAttacking)
        //    {
        //        StartCoroutine(AddBoost());
        //        actionsLeft--;
        //        StartCoroutine(ActionCooldown());
        //    }
        //}


        //Second control scheme

        if (actionsLeft > 0)
        {
            if (Input.GetKeyDown(changeDirection) || Input.GetKeyDown(changeDirection2))
            {
                if (Input.GetAxisRaw(playerAxis) != Mathf.Sign(_rigidbody.velocity.x))
                {
                    ChangeDirection();
                }

                StartCoroutine(AddBoost(2.0f, 0.75f));
                actionsLeft--;
                StartCoroutine(ActionCooldown());
            }
            else if (Input.GetKeyDown(placeWall))
            {
                ChangeDirection();

                if (!stunned && !isAttacking)
                    PlaceWall();

                actionsLeft--;
                StartCoroutine(ActionCooldown());
            }
        }


    }


    private void ChangeDirection()
    {
        speed *= -1f;
    }


    public IEnumerator AddBoost(float multipier, float seconds)
    {
        if (!isAttacking && !stunned)
        {
            isAttacking = true;
            chargeMultiplier = multipier;
            _renderer.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(seconds);
            _renderer.color = thisColor;
            chargeMultiplier = 1.0f;
           // Debug.Log("Charge multiplier for " + gameObject.tag ": " + chargeMultiplier); 
            isAttacking = false;

        }

    }

    private IEnumerator ActionCooldown()
    {

        usingAction = true;
        yield return new WaitForSeconds(cooldownTime);
        usingAction = false;

        if (actionsLeft < 3)
        {
            yield return new WaitForSeconds(1.0f);
            actionsLeft++;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            ChangeDirection();

        }
        else if (collision.gameObject.tag == "tempWall")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Stun());
        }
        else if (collision.gameObject.tag == "floor" || collision.gameObject.tag == "floorCrusher")
        {
            transform.SetParent(collision.transform);
            canJump = true;
        }
        else if (collision.gameObject.tag == "topCrusher" && _rigidbody.velocity.y <= 0)
        {
            _respawner.Respawn(gameObject);
            _scoreManager.SubtractPlayerScore(gameObject.tag);
            Destroy(gameObject); 
        }
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            PlayerScript other = collision.gameObject.GetComponent<PlayerScript>();

            if (isAttacking && !other.isAttacking)
            {
                if ( !isInvincible && !other.isInvincible)
                {
                    _scoreManager.AddPlayerScore(gameObject.tag);
                    _respawner.Respawn(collision.gameObject);
                    Destroy(collision.gameObject);
                }
                else if (isInvincible || other.isInvincible)
                {
                    ChangeDirection();
                    speed += 0.05f * (Mathf.Abs(speed) / speed);

                }

            }

            else
            {
                ChangeDirection();
                speed += 0.05f * (Mathf.Abs(speed) / speed);

            }
            Debug.Log(speed);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            transform.SetParent(null);
            canJump = false;
        }
    }

    private bool hasAction()
    {
        return (!usingAction && actionsLeft > 0);
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }

    public int getActionsLeft()
    {
        return actionsLeft;
    }

    private void PlaceWall()
    {
        Instantiate(tempWall, new Vector3(transform.position.x + (-0.85f * (-_rigidbody.velocity.x / Mathf.Abs(_rigidbody.velocity.x))), transform.position.y), Quaternion.identity);
    }

    private void PacmanEffect()
    {
        if (transform.position.x > 9.37f)
        {
            transform.position = new Vector2(-9.37f, transform.position.y);
        }
        else if (transform.position.x < -9.37f)
        {
            transform.position = new Vector2(9.37f, transform.position.y);
        }
    }

    private IEnumerator Invincible()
    {
        if (!isInvincible)
        {
            isInvincible = true;

            _renderer.color = new Color(thisColor.r, thisColor.g, thisColor.b, 0.5f);

            for (int i = 0; i < 10; i ++)
            {
                _renderer.enabled = false;
                yield return new WaitForSeconds(0.1f);
                _renderer.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }

            _renderer.color = thisColor;

            isInvincible = false;
        }
    }

    private IEnumerator Stun()
    {
        _renderer.color = thisColor;
        isAttacking = false;
        Quaternion tempRotation = Quaternion.identity;
       // chargeMultiplier = 0f;
        stunned = true;
        transform.Rotate(new Vector3(0f, 0f, 90.0f));
        yield return new WaitForSeconds(2.0f);
        stunned = false;
        //chargeMultiplier = 1.0f;
        transform.rotation = tempRotation;
    }

}




