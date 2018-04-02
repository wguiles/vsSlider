using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public GameObject player1Explosion;
    public GameObject player2Explosion;

    public Rigidbody2D _rigidbody;

    [HideInInspector]
    public SpriteRenderer _renderer;

    private SoundManager _soundManager;

    public float speed;

    public float chargeMultiplier = 1.0f;

    public float jumpForce;

    public GameObject tempWall;

    private ScoreManager _scoreManager;
    private RespawnerScript _respawner;

    [SerializeField]
    public  KeyCode placeWall;


    public KeyCode changeDirection;

    [SerializeField]
    private KeyCode changeDirection2;

    public float initalJumpForce;

    public bool canJump = true;

    private bool usingAction;

    private int score;

    public Color thisColor;

    [SerializeField]
    private int actionsLeft;

    [SerializeField]
    private float cooldownTime;


    public string playerAxis;


    public KeyCode jumpKey;

    [HideInInspector]
    public bool isAttacking;

    [HideInInspector]
    public bool isInvincible;

    [HideInInspector]
    public bool stunned;

    //for shootout Level

    private bool canShoot;
    public GameObject bullet;



    // *** Maybe make an OnDestroy Method for scoring and respawning?


    public void Start()
    {
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        if (initalJumpForce != 0)
            jumpForce = initalJumpForce;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Shootout"))
            canShoot = true;
            

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
    public virtual void Update()
    {
        if (!stunned)
        {
            Actions();
        }
        Jumping();
        Movement();
    }

   
    public virtual void Jumping()
    {
        if (Input.GetKeyDown(jumpKey) && canJump)
        {
            _soundManager.PlaySound("jump");
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }
    }

    public virtual void Movement()
    {
        _rigidbody.velocity = new Vector2(speed * chargeMultiplier, _rigidbody.velocity.y);
    }

    public virtual void Actions()
    {

        if (actionsLeft > 0)
        {
            if (Input.GetKeyDown(changeDirection) || Input.GetKeyDown(changeDirection2))
            {
                if (Input.GetAxisRaw(playerAxis) != Mathf.Sign(_rigidbody.velocity.x))
                {
                    StartCoroutine(ActionCooldown());
                    ChangeDirection();

                }
                _soundManager.PlaySound("attack");
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
        _soundManager.PlaySound("changeDirection");
        speed *= -1f;
    }


    public virtual IEnumerator AddBoost(float multipier, float seconds)
    {
        if (!isAttacking && !stunned)
        {
            isAttacking = true;
            chargeMultiplier = multipier;
            _renderer.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(seconds);
            _renderer.color = thisColor;
            chargeMultiplier = 1.0f;
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
            canJump = true;
        }
        else if (collision.gameObject.tag == "topCrusher" && _rigidbody.velocity.y <= 0)
        {
            _respawner.Respawn();
            _scoreManager.SubtractPlayerScore(gameObject.tag);
            Destroy(gameObject); 
        }

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            PlayerScript other = collision.gameObject.GetComponent<PlayerScript>();

            if (!isAttacking && other.isAttacking)
            {
                if ( !isInvincible && !other.isInvincible)
                {
                    DestroyAndRespawn();
                }
                else if (isInvincible || other.isInvincible)
                {
                    ChangeDirection();
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

    public virtual void PlaceWall()
    {
        GameObject wallPlaced = Instantiate(tempWall, new Vector3(transform.position.x + (0.85f * (Mathf.Sign(_rigidbody.velocity.x))), transform.position.y), Quaternion.identity);
        if (canShoot)
        {
            Instantiate(bullet, wallPlaced.transform.position, Quaternion.identity);
        }
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

    public IEnumerator Stun()
    {
        _soundManager.PlaySound("stunned");
        _renderer.color = thisColor;
        isAttacking = false;
        stunned = true;
        transform.Rotate(new Vector3(0f, 0f, 90.0f));
        yield return new WaitForSeconds(2.0f);
        stunned = false;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void DestroyAndRespawn()
    {
        
        _respawner.startRespawn();
        Destroy(gameObject);
        SpawnExplosion();
        _scoreManager.AddPlayerScore(OtherPlayer(gameObject.tag));
    }

	private void SpawnExplosion()
	{
        _soundManager.PlaySound("explosion");
        if (gameObject.tag == "Player")
            Instantiate(player1Explosion, gameObject.transform.position, Quaternion.identity);
        else if (gameObject.tag == "Player2")
            Instantiate(player2Explosion, gameObject.transform.position, Quaternion.identity);
            
	}

    private string OtherPlayer (string playerString)
    {
        if (playerString == "Player")
            return "Player2";
        else
            return "Player";
    }

}




