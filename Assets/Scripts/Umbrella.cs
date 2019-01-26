using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    public GameObject center;
    public GameObject left;
    public GameObject right;
    public GameObject extend;
    private List<Extender> rightExtensions;
    private List<Extender> leftExtensions;
    int currentRight = 0;
    int currentLeft = 0;
    float extensionWidth = 0;
    // Start is called before the first frame update
    void Start()
    {
        leftExtensions = new List<Extender>();
        leftExtensions.Add(new Extender(left, false));
        rightExtensions = new List<Extender>();
        rightExtensions.Add(new Extender(right, false));
        extensionWidth = extend.GetComponent<SpriteRenderer>().bounds.size.x;
    }
    public float GetMaxRight()
    {
        return currentRight * extensionWidth;
    }
    public float GetMaxLeft()
    {
        return currentLeft * extensionWidth * -1;
    }
    public void Extend()
    {
        if (currentRight <= currentLeft)
        {
            currentRight++;
            GameObject newExtension = Instantiate(extend,
                new Vector2(transform.position.x - extendRightAmount, transform.position.y),
                Quaternion.identity, transform);
            // newExtension.transform.localScale = new Vector2(0, newExtension.transform.localScale.y);
            rightExtensions.Add(new Extender(newExtension, true));
            extendRightAmount += extensionWidth;
        }
        else
        {
            currentLeft++;
            GameObject newExtension = Instantiate(extend,
              new Vector2(transform.position.x - extendLeftAmount, transform.position.y),
              Quaternion.identity, transform);
            //  newExtension.transform.localScale = new Vector2(0, newExtension.transform.localScale.y);
            leftExtensions.Add(new Extender(newExtension, true));
            extendLeftAmount -= extensionWidth;
        }
    }
    bool removingRight = false;
    bool removingLeft = false;
    public void Shrink()
    {
        if (currentRight > currentLeft)
        {
            currentRight--;
            Extender removeEl = rightExtensions[rightExtensions.Count - 1];
            removeEl.removing = true;
            removingRight = true;
            extendRightAmount -= extensionWidth;
        }
        else
        {
            currentLeft--;
            Extender removeEl = leftExtensions[leftExtensions.Count - 1];
            removeEl.removing = true;
            removingLeft = true;
            extendLeftAmount += extensionWidth;
        }

    }
    float extendRightAmount = 0;
    float extendLeftAmount = 0;
    public float growSpeed = 0.01f;
    // Update is called once per frame
    void Update()
    {
        if (extendRightAmount > 0)
        {
            float eAmount = 0;
            if (growSpeed > extendRightAmount)
            {
                eAmount = extendRightAmount;
            }
            else
            {
                eAmount = growSpeed;
            }
            foreach (Extender e in rightExtensions)
            {
                float xPos = e.extension.transform.position.x;

                e.extension.transform.position = new Vector2(xPos + eAmount,
                                                                e.extension.transform.position.y);
                // if (e.growing)
                // {
                //     e.extension.transform.localScale = new Vector2(1,
                //                                                     e.extension.transform.localScale.y);
                // }
                // if (Mathf.Abs(e.extension.transform.localScale.x) >= 1)
                // {
                //     e.growing = false;
                // }
            }
            extendRightAmount -= eAmount;
        }
        else if (extendRightAmount < 0)
        {
            float eAmount = 0;
            if (growSpeed > Mathf.Abs(extendRightAmount))
            {
                eAmount = extendRightAmount;
            }
            else
            {
                eAmount = -growSpeed;
            }
            foreach (Extender e in rightExtensions)
            {
                float xPos = e.extension.transform.position.x;

                e.extension.transform.position = new Vector2(xPos + eAmount,
                                                                e.extension.transform.position.y);
            }
            extendRightAmount -= eAmount;
        }
        else
        {
            if (removingRight)
            {
                removingRight = false;

                for (int i = 0; i < rightExtensions.Count; i++)
                {
                    var e = rightExtensions[i];
                    if (e.removing)
                    {
                        Destroy(e.extension);
                        rightExtensions.Remove(e);
                        i--;
                    }
                }
            }
        }


        if (extendLeftAmount < 0)
        {
            float eAmount = 0;
            if (growSpeed > Mathf.Abs(extendLeftAmount))
            {
                eAmount = extendLeftAmount;
            }
            else
            {
                eAmount = -growSpeed;
            }
            foreach (Extender e in leftExtensions)
            {
                float xPos = e.extension.transform.position.x;
                e.extension.transform.position = new Vector2(xPos + eAmount,
                                                                e.extension.transform.position.y);
            }
            extendLeftAmount -= eAmount;
        }
        else if (extendLeftAmount > 0)
        {
            float eAmount = 0;
            if (growSpeed > extendLeftAmount)
            {
                eAmount = extendLeftAmount;
            }
            else
            {
                eAmount = growSpeed;
            }
            foreach (Extender e in leftExtensions)
            {
                float xPos = e.extension.transform.position.x;

                e.extension.transform.position = new Vector2(xPos + eAmount,
                                                                e.extension.transform.position.y);
            }
            extendLeftAmount -= eAmount;
        }
        else
        {
            if (removingLeft)
            {
                removingLeft = false;

                for (int i = 0; i < leftExtensions.Count; i++)
                {
                    var e = leftExtensions[i];
                    if (e.removing)
                    {
                        Destroy(e.extension);
                        leftExtensions.Remove(e);
                        i--;
                    }
                }
            }
        }
    }
    class Extender
    {
        public bool growing;
        public GameObject extension;
        public bool removing = false;
        public Extender(GameObject extension, bool growing)
        {
            this.extension = extension;
            this.growing = growing;
        }
    }
}
