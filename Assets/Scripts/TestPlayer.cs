using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

	public float moveSpeed = 7f;
	public float jumpForce = 10f;
    public float warmth = 100;

    private Rigidbody2D rb;
    private List<GameObject> friend_list = new List<GameObject>();

    private Animator anim;

    private bool canJump;

    void Start () {
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

	void FixedUpdate(){

        float move = Input.GetAxis("Horizontal");

        // Jump
        if (Input.GetButton("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            canJump = false;
        }

        // Left and right
       
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.collider.gameObject.tag == "Ground")
        {
            canJump = true;
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

    void Update () {

        // Drop Friends
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (friend_list.Count > 0) {
                Friend friend = friend_list[0].GetComponent<Friend>();
                friend.drop();
            friend_list.RemoveAt(0);
            }
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
}
