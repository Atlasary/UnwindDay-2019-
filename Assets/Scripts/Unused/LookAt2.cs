using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2 : MonoBehaviour {
    //public GameObject reference;
    private Vector3 dir;
    public JoystickBehavior joy;
    //int L = Screen.height;
    //int l = Screen.width;
    float deltz;
    float deltx;
    private Camera cam;
    private Vector3 loc = new Vector3();
    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        joy = FindObjectOfType(typeof(JoystickBehavior)) as JoystickBehavior;
        //deltx = 2 * reference.transform.position[0];
        //deltz = 2 * reference.transform.position[2];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = joy.GetJoystickValues();
        //float optx = reference.transform.position[0];
        //float optz = reference.transform.position[2];
        if (Input.touchCount > 1)
        {
            loc = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, 0, Input.GetTouch(0).position.y));
            transform.LookAt(loc);
        }
        else
        {
            if (dir[0] != 0 && dir[2] != 0)
            {
                deltx = dir[0];
                deltz = dir[2];
                loc[0] = 100 * deltx + transform.position.x;
                loc[2] = 100 * deltz + transform.position.z;
                transform.LookAt(loc);
            }
        }


        //float fx = ((deltx / l) * x + (optx - deltx));
        //float fz = ((deltz / L) * z + (optz - deltz));
    }
}
