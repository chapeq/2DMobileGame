using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileTouchController : MonoBehaviour
{
   
    public float speed;                //Floating point variable to store the player's movement speed.
    public Animator animator;
    public Joystick joystick;

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    float moveHorizontal;
    float moveVertical;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Store the current horizontal input in the float moveHorizontal.   
        moveHorizontal = joystick.Horizontal;

        //Store the current vertical input in the float moveVertical.
        moveVertical = joystick.Vertical;

        animator.SetFloat("Horizontal", moveHorizontal);
        animator.SetFloat("Vertical", moveVertical);
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
    }
}