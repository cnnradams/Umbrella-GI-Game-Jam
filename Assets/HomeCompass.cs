using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeCompass : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 homeOffset;
    public GameObject player;
    public GameObject home;
    public GameObject thisObject;
    public Camera mainCamera;
    public Text distance;
    public bool onScreen;
    void Start()
    {
        home = GameObject.FindGameObjectWithTag("Home");
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();   
        thisObject = GameObject.FindGameObjectWithTag("HomeArrow");
        distance = thisObject.GetComponentInChildren<Text>();
        thisObject.SetActive(false);
        //Debug.Log(distance.gameObject.name);

    }

    // Update is called once per frame
    void Update()
    {
        homeOffset = player.transform.position - home.transform.position;
        float magnitude = homeOffset.magnitude;
        Vector3 homePoint = mainCamera.WorldToViewportPoint(home.transform.position);
        onScreen = homePoint.z > 0 && homePoint.x > 0 && homePoint.x < 1 && homePoint.y > 0 && homePoint.y < 1;

        if (!onScreen)
        {
            if (homePoint.x > 0)
            {
                thisObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.9355485f, 0.5f);
                thisObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.9355485f, 0.5f);
                thisObject.GetComponent<RectTransform>().eulerAngles = new Vector3(thisObject.GetComponent<RectTransform>().eulerAngles.x, thisObject.GetComponent<RectTransform>().eulerAngles.y, 0);
                Vector3 angle = distance.gameObject.GetComponent<RectTransform>().eulerAngles;
                distance.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(angle.x, angle.y, 0);
            }
            else
            {
                thisObject.GetComponent<RectTransform>().anchorMin = new Vector2(-0.8f, 0.5f);
                thisObject.GetComponent<RectTransform>().anchorMin = new Vector2(-0.8f, 0.5f);
                thisObject.GetComponent<RectTransform>().eulerAngles = new Vector3(thisObject.GetComponent<RectTransform>().eulerAngles.x, thisObject.GetComponent<RectTransform>().eulerAngles.y, 180);
                Vector3 angle = distance.GetComponent<RectTransform>().eulerAngles;
                distance.GetComponent<RectTransform>().eulerAngles = new Vector3(angle.x, angle.y, 0);


            }
            distance.text = "Home\n" + magnitude;
            thisObject.SetActive(true);
        }
        else
        {
            thisObject.SetActive(false);
        }
    }
}
