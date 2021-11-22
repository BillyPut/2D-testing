using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5f;

    private Animator animator;
    private string currentState;
    private float xAxis;
    private float yAxis;
    private Rigidbody2D rb2d;
    private bool isJumpPressed;
    private float jumpForce = 850;
    private int groundMask;
    private bool isGrounded;
    private string currentAnimation;
    private bool isAttackPressed;
    private bool isAttacking;

    [SerializeField]
    private float attackDelay = 0.3f;

    const string MEGAMANIDLE = "MegamanIdle";
    const string MEGAMANRUN = "MegamanRun";
    const string MEGAMANJUMP = "MegamanJump";
    const string MEGAMANATTACK = "MegamanAttack";


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            isAttackPressed = true;
        }
    }

    private void FixedUpdate()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }



        Vector2 vel = new Vector2(0, rb2d.velocity.y);

        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-5, 5);
            ChangeAnimationState(MEGAMANRUN);
        }
        else if (xAxis > 0)
        {
            vel.x = walkSpeed;
            transform.localScale = new Vector2(5, 5);
            ChangeAnimationState(MEGAMANRUN);
        }
        else
        {
            vel.x = 0;
            ChangeAnimationState(MEGAMANIDLE);

        }

       

        if (isJumpPressed && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
        }


        rb2d.velocity = vel;



        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking)
            {
                isAttacking = true;
            }




        }






    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }


}
