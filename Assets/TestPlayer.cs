using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

	public float moveSpeed;
	public float jumpHeight;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb= GetComponent<Rigidbody2D>();
    }

	void FixedUpdate(){
		
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
    }
}
