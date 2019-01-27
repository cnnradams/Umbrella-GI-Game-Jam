using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public bool cold = true;
    public float returnSpeed = 1f;
    public bool atHome = false;
    public float speed;
    public Vector3 homePosition;

    public AudioClip homesound;

    private Rigidbody2D rb;
    private Animator anim;
    public GameObject player;
    private GameObject gameManager;
    private Umbrella umbrella;

    private float offsetPosition;
    private float wanderPosition;
    private float offsetExit = 1f;
    private bool wander_stopped;

    // Start is called before the first frame update
    void Start()
    {
        cold = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        umbrella = player.GetComponentInChildren<Umbrella>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        speed = Random.Range(0.5f, 1f);
        wander();
        wander_stopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        float current_speed = speed;
        anim.SetBool("Cold", cold);
        if (player.GetComponent<TestPlayer>().rb.velocity.magnitude > 1)
        {
            // wanderPosition = offsetPosition;
        }
        float step = current_speed * Time.deltaTime;
        if (atHome)
        {
            gameManager.GetComponent<GameManager>().droppedHome();
            gameManager.GetComponent<GameManager>().addTime();
            SoundManager.instance.RandomizeSfx(homesound);
            Destroy(gameObject);

        }
        else if (!cold)
        {
            followPlayer(step);
        }
    }



    public void pickup()
    {
        cold = false;
        //Change Sprite
        //Start Following Player
        offsetPosition = Random.Range(umbrella.GetMaxLeft(), umbrella.GetMaxRight());
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        //Change Physics to Kinematic
    }
    public void drop()
    {
        cold = true;
        //Don't move
        float direction = player.GetComponent<Rigidbody2D>().velocity.x;
        if (direction > 0)
        {
            gameObject.transform.position = player.transform.position + new Vector3(umbrella.GetMaxLeft(), 0);
        }
        else
        {
            gameObject.transform.position = player.transform.position + new Vector3(umbrella.GetMaxRight(), 0);
        }
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        //Retain Pyhsical Properties
    }
    private void followPlayer(float step)
    {
        if (wanderPosition - offsetPosition < 0.06 && wander_stopped)
        {
            wander_stopped = false;
            StartCoroutine(waitAndWander());
        }
        offsetPosition = offsetPosition + (wanderPosition - offsetPosition) * step;
        //Debug.Log("offset" + offsetPosition);
        //Debug.Log("Wander" + wanderPosition);
        gameObject.transform.position = player.transform.position + new Vector3(offsetPosition, 0);

    }
    IEnumerator waitAndWander()
    {
        int wait_time = Random.Range(4, 10);
        yield return new WaitForSeconds(wait_time);
        wander();
        wander_stopped = true;
    }
    private void wander()
    {
        wanderPosition = Random.Range(umbrella.GetMaxLeft(), umbrella.GetMaxRight());

    }
}
