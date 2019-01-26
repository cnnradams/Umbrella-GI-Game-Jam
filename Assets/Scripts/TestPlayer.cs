using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

	public float moveSpeed;
	public float jumpHeight;

    private Rigidbody2D rb;
    private List<Friend> friend_list = new List<Friend>();

    private Animator anim;

    // Use this for initialization
    void Start () {
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

	void FixedUpdate(){
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Friend>())
        {
            Debug.Log("Collided With Friend!");
            Friend friend = collision.gameObject.GetComponent<Friend>();
            if (friend.cold)
            {
                friend_list.Add(friend);
                friend.pickup();
            }
            
        }
    }
    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown (KeyCode.Space)){
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
		}
		if(Input.GetKey (KeyCode.D)){
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
		if(Input.GetKey (KeyCode.A)){
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(friend_list.Count > 0)
            friend_list[0].drop();
            friend_list.RemoveAt(0);
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
}
