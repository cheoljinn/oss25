using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Run : MonoBehaviour
{
    public float jumpForce = 7f;
    public float gravityScale = 2f;

    public int maxHP = 3;
    public int currentHP;
    public heartUI heart;

    public Sprite[] petSprites;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    private bool isGrounded = true;
    private bool isSliding = false;
    private bool isAttacked = false;
    private bool canDoubleJump;


    void Start()
    {
        SoundManager.instance.PlayBgm(true);
        rb =GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        sr=GetComponent<SpriteRenderer>();

        currentHP = maxHP;
        heart.UpdateHearts(currentHP);
        UpdateSprite();
    }

    void Update()
    {
        animator.SetBool("isJump", !isGrounded);
        animator.SetBool("isSliding", isSliding);
    }

    public void Jump()
    {
        SoundManager.instance.Playsfx(SoundManager.Sfx.jump);

        if (!isSliding)
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                isGrounded = false;
                canDoubleJump = true;
            }
            else if(canDoubleJump)
            {
                rb.velocity= Vector2.up * jumpForce;
                canDoubleJump=false;
            }
        }
    }

    public void StartSlide()
    {
        SoundManager.instance.Playsfx(SoundManager.Sfx.land);

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
            rb.gravityScale = gravityScale;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !isAttacked)
        {
            StartCoroutine(TakeDamage());
        }
    }


    private IEnumerator TakeDamage()
    {
        Debug.Log("ÇÇ°Ý");
        isAttacked = true;
        currentHP -= 1;
        heart.UpdateHearts(currentHP);
        SoundManager.instance.Playsfx(SoundManager.Sfx.die);

        if (currentHP > 0) {
            sr.enabled = false;
            yield return new WaitForSeconds(0.15f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.15f);
            sr.enabled = false;
            yield return new WaitForSeconds(0.15f);
            sr.enabled = true;
        }
        else
        {
            currentHP = 0;
            animator.SetTrigger("doDie");
            yield return new WaitForSeconds(0.6f);
            Debug.Log("µÚÁü");
            RunManager.Instance.EndGame();
        }

        isAttacked=false;
    }
    public void UpdateSprite()
    {
        int eggId = DataManager.instance.selectedEgg;
        int spriteIndex = (eggId - 1) * 4 + DataManager.instance.currentLevel;
        sr.sprite = petSprites[spriteIndex];
    }
}
