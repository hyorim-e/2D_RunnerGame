using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isJump = false;
    bool isTop = false;
    public float jumpHeight = 0;
    public float jumpSpeed = 0;

    Vector2 startPosition;
    Animator animator;

    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(GameManager.instance.isPlay)
            animator.SetBool("run", true);
        else
            animator.SetBool("run", false);

        if (Input.GetMouseButtonDown(0) && GameManager.instance.isPlay)
        {
            isJump = true;
        } else if(transform.position.y <= startPosition.y)
        {
            animator.SetBool("jump", false);
            isJump = false;
            isTop = false;
            transform.position = startPosition;
        }

        if (isJump)
        {
            animator.SetBool("jump", true);
            if (transform.position.y <= jumpHeight - 0.1f && !isTop)
            {
                transform.position = Vector2.Lerp(transform.position, 
                    new Vector2(transform.position.x, jumpHeight), jumpSpeed*Time.deltaTime);
            }
            else
            {
                isTop = true;
            }
            if(transform.position.y > startPosition.y && isTop)
            {
                transform.position = Vector2.MoveTowards(transform.position, startPosition, jumpSpeed*Time.deltaTime);
            }           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob"))
        {
            GameManager.instance.GameOver();
        }
    }
}
