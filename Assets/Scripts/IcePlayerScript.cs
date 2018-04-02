using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IcePlayerScript : MonoBehaviour
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
    private KeyCode dash;

    [SerializeField]
    private KeyCode left;

    [SerializeField]
    private KeyCode right;


    
    private bool canJump = true;

    private bool usingAction;

    private int score;

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

    public float pushForce;

    void Start()
    {
        _respawner = GameObject.Find("Respawner").GetComponent<RespawnerScript>();
        _scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        thisColor = _renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        Jumping();
       // Movement();
        Actions();
        PacmanEffect();

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
        if (hasAction())
        {   // {{
            //if (Input.GetAxisRaw(playerAxis) / _rigidbody.velocity.x < 0)
            //if ((Input.GetAxisRaw(playerAxis) != Mathf.Sign(_rigidbody.velocity.x)))
            //{
            //    ChangeDirection();

            //    if (canJump)
            //        PlaceWall();

            //    actionsLeft--;
            //    StartCoroutine(ActionCooldown());

            if (Input.GetAxis(playerAxis) != 0f && !isAttacking)
            {
                StartCoroutine(AddBoost());
                actionsLeft--;
                StartCoroutine(ActionCooldown());
            }
            //}

        }


        
    }

    private void ChangeDirection()
    {
        speed *= -1f;
    }

    private IEnumerator AddBoost()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            // chargeMultiplier = 2.0f;
            _rigidbody.AddForce(new Vector2(pushForce * Time.deltaTime, _rigidbody.velocity.y), ForceMode2D.Force);
            _renderer.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.75f);
            _renderer.color = thisColor;
            //chargeMultiplier = 1.0f;
            isAttacking = false;
        }

    }

    private IEnumerator ActionCooldown()
    {
        if (actionsLeft < 3)
        {
            yield return new WaitForSeconds(1.5f);
            actionsLeft++;
        }

        usingAction = true;
        yield return new WaitForSeconds(cooldownTime);
        usingAction = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "tempWall")
        {
            ChangeDirection();

            if (collision.gameObject.tag == "tempWall")
                Destroy(collision.gameObject);

            speed += 0.01f * (Mathf.Abs(speed) / speed);
            Debug.Log(speed); 
            
        }
        else if (collision.gameObject.tag == "floor")
        {
            canJump = true;
        }
        else if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            IcePlayerScript other = collision.gameObject.GetComponent<IcePlayerScript>();

            if (isAttacking && !other.isAttacking)
            {
                _scoreManager.AddPlayerScore(gameObject.tag);
                _respawner.Respawn();
                Destroy(collision.gameObject);
            }
                
            else
                ChangeDirection();

            speed += 0.01f * (Mathf.Abs(speed) / speed);
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
        return(!usingAction && actionsLeft > 0);
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

}
