using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Run : MonoBehaviour
{

    public float jumpForce = 10f;
    public float slideSpeed = 5f;
    public int HP = 3;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded = true;
    private bool isSliding = false;


    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isSliding", isSliding);

        if (HP <= 0)
        {
            animator.SetTrigger("doDie");
        }
    }

    public void Jump()
    {
        if (isGrounded && !isSliding)
        {
            rb.velocity = Vector2.up * jumpForce;
            isGrounded = false;
        }
    }

    public void StartSlide()
    {
        if (isGrounded && !isSliding) {
            isSliding = true;
        }
    }

    public void StopSlide()
    {
        if (isSliding)
        {
            isSliding=false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded=true;
        }
    }

}
