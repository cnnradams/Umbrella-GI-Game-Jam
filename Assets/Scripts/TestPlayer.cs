﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{


<<<<<<< HEAD



=======
	public float moveSpeed = 7f;
	public float jumpForce = 10f;
>>>>>>> parent of 2080973... Velocity Setting and Lighting
    public float warmth = 100;

<<<<<<< HEAD

    public float speed_scale = 8f;
    public float speed_power = 1f;
    public float min_speed = 7f;
    public float jump_scale = 20f;
    public float jump_power = 1f;
    public float min_jump = 10f;

    public float moveSpeed;
    public float jumpForce;

=======

>>>>>>> parent of 2080973... Velocity Setting and Lighting
    private Rigidbody2D rb;
    private List<GameObject> friend_list = new List<GameObject>();

    private Animator anim;

    private bool canJump;
    private bool dropping;
    private BoxCollider2D thisCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        thisCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {

        float move = Input.GetAxis("Horizontal");

        // Jump
        if (Input.GetButton("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }

        // Left and right

        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D Other)
    {
        Debug.Log(Other.collider.gameObject.tag);
        if (Other.collider.gameObject.tag == "Ground" || Other.collider.gameObject.tag == "Platform")
        {
            canJump = true;
        }
    }

    void OnCollisionStay2D(Collision2D Other)
    {
        if (Other.collider.gameObject.tag == "Platform")
        {
            if (dropping)
            {
                Physics2D.IgnoreCollision(Other.collider, GetComponent<Collider2D>());
                StartCoroutine(Recollide(Other, GetComponent<Collider2D>(), 0.5f));
            }
        }
    }
    IEnumerator Recollide(Collision2D Other, Collider2D me, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Physics2D.IgnoreCollision(Other.collider, me, false);
        dropping = false;
        // Now do your thing here
    }
    void OnCollisionExit2D(Collision2D Other)
    {
        if (Other.collider.gameObject.tag == "Ground" || Other.collider.gameObject.tag == "Platform")
        {
            canJump = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Friend"))
        {
            //Debug.Log("Collided With Friend!");
            GameObject go = collision.gameObject.transform.parent.gameObject;
            Friend friend = go.GetComponent<Friend>();

            if (friend.cold)
            {
                umbrella.Extend();
                friend_list.Add(go);
                friend.pickup();
            }

        }
    }

    void Update()
    {

        // Drop Friends
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (friend_list.Count > 0)
            {
                Friend friend = friend_list[0].GetComponent<Friend>();
                umbrella.Shrink();
                friend.drop();
                friend_list.RemoveAt(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dropping = true;
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
}
