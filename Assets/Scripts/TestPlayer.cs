using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float people_saved;
    public float friend_list_length = 0f;

    public Umbrella umbrella;
    public Light pointLight;
    public float warmth = 100;
    public float drop_distance = 0.5f;

    public float speed_scale = 8f;
    public float speed_power = 1f;
    public float min_speed = 7f;
    public float jump_scale = 20f;
    public float jump_power = 1f;
    public float min_jump = 10f;

    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rb;
    public bool atHome;

    private List<GameObject> friend_list = new List<GameObject>();
    private CircleCollider2D boxColl;
    private Animator anim;

    public bool canJump;
    private bool dropping;
    private float dropTimer = 0.2f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxColl = gameObject.GetComponent<CircleCollider2D>();
        umbrella = transform.GetComponentInChildren<Umbrella>();
        pointLight = transform.GetComponentInChildren<Light>();
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



    void OnCollisionStay2D(Collision2D Other)
    {
        //Debug.Log(Other.collider.gameObject.tag);
        if (Other.collider.gameObject.tag == "Ground" || Other.collider.gameObject.tag == "Platform")
        {
            if (dropping && Other.collider.gameObject.tag == "Platform")
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - drop_distance);
                dropping = false;
            }
            canJump = true;
        }
        if (Other.collider.gameObject.tag == "Home")
        {
            atHome = true;
        }
        atHome = false;
    }
    void OnCollisionExit2D(Collision2D Other)
    {
        if (Other.collider.gameObject.tag == "Ground" || Other.collider.gameObject.tag == "Platform")
        {
            canJump = false;
            if (Other.collider.gameObject.tag == "Platform")
            {
                Debug.Log("ENTERED TRIGGER");
                boxColl.isTrigger = false;
                dropping = false;
            }
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
                pointLight.range++;
                pointLight.intensity++;
                umbrella.Extend();
                friend_list.Add(go);
                friend.pickup();
            }

        }
    }

    void Update()
    {
        friend_list_length = friend_list.Count;

        moveSpeed = speed_scale * Mathf.Exp(-speed_power * friend_list.Count) + min_speed;
        jumpForce = jump_scale * Mathf.Exp(-jump_power * friend_list.Count) + min_jump;
        // Drop Friends
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (friend_list.Count > 0)
            {
                if ( friend_list[0] == null)
                {
                    friend_list.RemoveAt(0);
                }
                else
                {
                    Friend friend = friend_list[0].GetComponent<Friend>();
                    umbrella.Shrink();
                    friend_list.RemoveAt(0);
                    friend.drop();
                    pointLight.range--;
                    pointLight.intensity--;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dropping = true;
        }
        if (dropping) dropTimer -= Time.deltaTime;

        if (dropTimer < 0)
        {
            dropping = false;
            dropTimer = 0.2f;
        }
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
}