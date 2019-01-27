using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Umbrella umbrella;
    Camera thisCamera;
    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, ((umbrella.GetMaxRight() - umbrella.GetMaxLeft() + 2) * Screen.height / Screen.width) / 2, Time.deltaTime);
        if (thisCamera.orthographicSize < 4)
        {
            thisCamera.orthographicSize = 4;
        }
        thisCamera.transform.position = new Vector3(thisCamera.transform.position.x, Mathf.Lerp(thisCamera.transform.position.y, transform.parent.position.y, Time.deltaTime * 10), -10);
    }
}
