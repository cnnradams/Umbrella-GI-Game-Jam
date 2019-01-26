using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{

    public float moveSpeed = 7;
    public float jumpForce = 300;

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
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(new Vector2(0, jumpForce));
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
