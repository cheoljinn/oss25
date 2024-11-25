using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Run : MonoBehaviour
{

    public float jumpForce = 10f;
    public float slideSpeed = 5f;
    public int HP = 3;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    private bool isGrounded = true;
    private bool isSliding = false;
    private bool isAttacked = false;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        sr=GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        animator.SetBool("isJump", !isGrounded);
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

        if (collision.gameObject.tag == "Enemy"&&!isAttacked)
        {
            StartCoroutine(Attacked());
        }
    }


    private IEnumerator Attacked()
    {
        isAttacked = true;
        HP -= 1;

        if (HP > 0) {
            sr.enabled = true;
            yield return new WaitForSeconds(0.2f);
            sr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.2f);
            sr.enabled = false;
        }
        else
        {
            animator.SetTrigger("doDie");
            yield return new WaitForSeconds(0.6f);
        }

        isAttacked=false;
    }

}
