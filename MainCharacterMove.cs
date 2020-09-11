using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacterMove : MonoBehaviour
{

    private float speed = 5f;
    private bool isGoingRight = true;
    private Animator animator;
    private float move;
    private Rigidbody2D rigidbody2D;
    private bool isOnGround = true;
    private Transform checkpoint = null;
    private GameObject hp_1 = null;
    private GameObject hp_2 = null;
    private GameObject hp_3 = null;
    private GameObject banana;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        hp_1 = GameObject.Find("HP_1");
        hp_2 = GameObject.Find("HP_2");
        hp_3 = GameObject.Find("HP_3");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rigidbody2D.AddForce(new Vector2(0, 200));
        }

        if (Input.GetKeyUp(KeyCode.E) && rigidbody2D.gravityScale == 0)
        {
            rigidbody2D.gravityScale = 1;
            banana.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        move = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move * speed, rigidbody2D.velocity.y);

        if (move > 0 && !isGoingRight)
        {
            flipChar();
        }
        else if (move < 0 && isGoingRight)
        {
            flipChar();
        }

        if (!isOnGround)
        {
            animator.SetBool("isOnGround", false);
        }
        else
        {
            animator.SetBool("isOnGround", true);
        }
    }

    private void flipChar()
    {
        isGoingRight = !isGoingRight;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isOnGround = true;
        }

        if (collision.transform.tag == "Death")
        {
            checkHealth(false);
        }

        if (collision.transform.tag == "Enemy")
        {          
            checkHealth(false);
        }

        if (collision.transform.tag == "Spike")
        {
            checkHealth(true);
        }

        if (collision.transform.tag == "Checkpoint")
        {
            checkpoint = collision.transform;
        }

        if (collision.transform.tag == "Apple")
        {
            if (!hp_1.activeSelf)
            {
                hp_1.SetActive(true);
                Destroy(collision.gameObject);
            }
            else if (!hp_2.activeSelf)
            {
                hp_2.SetActive(true);
                Destroy(collision.gameObject);
            }
            else if (!hp_3.activeSelf)
            {
                hp_3.SetActive(true);
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isOnGround = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (collision.transform.tag == "Banana")
            {
                rigidbody2D.gravityScale = 0;
                banana = collision.gameObject;
                banana.SetActive(false);
            }
        }
    }

    public bool isLookingRight()
    {
        return isGoingRight;
    }

    private void checkHealth(bool isSpike)
    {
        if (hp_1.activeSelf)
        {
            rigidbody2D.gravityScale = 1;
            if (banana != null)
            {
                banana.SetActive(true);
            }
            if (checkpoint != null)
            {
                if (hp_3.activeSelf)
                {
                    hp_3.SetActive(false);
                }
                else if (hp_2.activeSelf)
                {
                    hp_2.SetActive(false);
                }
                else
                {
                    hp_1.SetActive(false);
                }
            }
            else
            {
                if (hp_3.activeSelf)
                {
                    hp_3.SetActive(false);
                }
                else if (hp_2.activeSelf)
                {
                    hp_2.SetActive(false);
                }
                else
                {
                    hp_1.SetActive(false);
                }
            }
            if (!isSpike && checkpoint != null)
            {
                transform.position = new Vector3(checkpoint.position.x, checkpoint.position.y, checkpoint.position.z);
                rigidbody2D.AddForce(new Vector2(0, 200));
            }
            if (!isSpike && checkpoint == null)
            {
                transform.position = new Vector3(-9.15f, -3.3f, -0.1f);
                rigidbody2D.AddForce(new Vector2(0, 200));
            }
            if (isSpike)
            {
                rigidbody2D.AddForce(new Vector2(0, -200));
            }
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
