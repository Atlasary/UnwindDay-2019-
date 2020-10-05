using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shoot : MonoBehaviour {
    public GameObject proj;
    private bool shootable;
    public bool rotlock;
    private float timer;
    private FollowPlayer fp;
    //private Animator anim;

	// Use this for initialization
	void Start () {
        shootable = true;
        rotlock = false;
        fp = FindObjectOfType(typeof(FollowPlayer)) as FollowPlayer;
        //anim = fp.Player.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //if ((Input.touchCount>0 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)) || Input.GetKeyDown(KeyCode.Space))
        //{
          //  StartCoroutine(Shootable());
        //}
        if ((((Input.touchCount > 1)||(Input.touchCount>0 && Input.GetTouch(0).position.x >2000)) || Input.GetKey(KeyCode.Space))&& shootable)
        {
            StartCoroutine(Shootable());
            timer = Time.time;
            rotlock = true;
        }
        if(Time.time-timer > 2)
        {
            rotlock = false;
        }
        //anim.SetBool("isshooting",rotlock);
    }
    private IEnumerator Shootable()
    {
        shootable = false;
        Instantiate(proj, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.4f);
        shootable = true;
    }

}



