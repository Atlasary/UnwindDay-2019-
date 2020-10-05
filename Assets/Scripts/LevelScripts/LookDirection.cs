using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirection : MonoBehaviour
{

    private Vector3 dirleft, dirright;
    public JoystickBehavior joyleft, joyright;
    float deltz;
    float deltx;
    public float speed;
    private Vector3 loc = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        speed = 100;
        joyleft = GameObject.Find("joyleft").GetComponent<JoystickBehavior>();
        joyright = GameObject.Find("joyright").GetComponent<JoystickBehavior>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dirleft = joyleft.GetJoystickValues();
        dirright = joyright.GetJoystickValues();
        if (dirleft != Vector3.zero && dirright==Vector3.zero)
        {
            transform.Translate(dirleft * speed, Space.World);
            deltx = dirleft[0];
            deltz = dirleft[2];
            loc[0] = 1000000 * deltx + transform.position.x;
            loc[2] = 1000000 * deltz + transform.position.z;
            transform.LookAt(loc);
        }
        else if(dirleft == Vector3.zero && dirright != Vector3.zero)
        {
            deltx = dirright[0];
            deltz = dirright[2];
            loc[0] = 1000000 * deltx + transform.position.x;
            loc[2] = 1000000 * deltz + transform.position.z;
            transform.LookAt(loc);
        }
        else if(dirleft != Vector3.zero && dirright != Vector3.zero)
        {
            transform.Translate(dirleft * speed, Space.World);
            deltx = dirright[0];
            deltz = dirright[2];
            loc[0] = 1000000 * deltx + transform.position.x;
            loc[2] = 1000000 * deltz + transform.position.z;
            transform.LookAt(loc);
        }
    }
}
