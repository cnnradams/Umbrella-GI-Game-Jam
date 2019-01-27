using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend_Contact : MonoBehaviour
{
    public Friend friend;

    // Start is called before the first frame update
    void Start()
    {
        friend = gameObject.transform.parent.gameObject.GetComponent<Friend>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Home") && friend.cold || friend.player.GetComponent<TestPlayer>().atHome)
        {
            //Debug.Log("Entered Contact");
            friend.atHome = true;
            friend.homePosition = collision.gameObject.transform.position;
        }
    }
}
