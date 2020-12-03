using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed;
    public Animator animator;
    public bool canMove = true;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Joystick joystick;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

#if UNITY_EDITOR || UNITY_STANDALONE
      joystick.gameObject.SetActive(false);
#else
        joystick.gameObject.SetActive(true);
#endif

    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        AudioManager.instance.Play("Attack");
    }

    public IEnumerator Die()
    {
        animator.SetTrigger("Dead");
        AudioManager.instance.Play("GameOver");
        yield return new WaitForSeconds(2f);
        GameOver.instance.ShowGameOver();
    }

#if UNITY_EDITOR || UNITY_STANDALONE

    void Update()
    {
             
        if (!canMove)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);  
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

#else
    void Update()
    {
        if (!canMove)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        animator.SetFloat("Horizontal",  movement.x);
        animator.SetFloat("Vertical",  movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

   private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

#endif
}