using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt3 : MonoBehaviour {
    //public GameObject reference;
    private Vector3 dir;
    public JoystickBehavior joy;
    //int L = Screen.height;
    //int l = Screen.width;
    float deltz;
    float deltx;
    private Vector3 loc = new Vector3();
    private Shoot shoot;
    // Use this for initialization
    void Start()
    {
        joy = FindObjectOfType(typeof(JoystickBehavior)) as JoystickBehavior;
        shoot = FindObjectOfType(typeof(Shoot)) as Shoot;
        //deltx = 2 * reference.transform.position[0];
        //deltz = 2 * reference.transform.position[2];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = joy.GetJoystickValues();
        //float optx = reference.transform.position[0];
        //float optz = reference.transform.position[2];
        if (shoot.rotlock)
        {
            transform.LookAt(loc);
        }
        else
        {
            if (dir[0] != 0 && dir[2] != 0)
            {
                deltx = dir[0];
                deltz = dir[2];
                loc[0] = 1000000 * deltx + transform.position.x;
                loc[2] = 1000000 * deltz + transform.position.z;
                transform.LookAt(loc);
            }
        }


        //float fx = ((deltx / l) * x + (optx - deltx));
        //float fz = ((deltz / L) * z + (optz - deltz));
    }
}
