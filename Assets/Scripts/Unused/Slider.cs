using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour {

    //private float fillamount;
    //private Image content;
    private FollowPlayer fp;

	// Use this for initialization
	void Start () {
        fp = FindObjectOfType(typeof(FollowPlayer)) as FollowPlayer;
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if ((fp.Getvitproj() >= 15) && (fillamount<1))
        {
            fillamount = fillamount + 0.001f;
        }
        else
        {
            fillamount = fillamount - (15 - fp.Getvitproj()) * 0.001f / 14;
        }
        
        content.fillAmount = fillamount;
        */
	}

}
