using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class Move : MonoBehaviour
    {
    private Vector3 dir;
    private JoystickBehavior joy;
    public float speed;
    private FollowPlayer fp;
    private Animator anim;

        // Use this for initialization
        void Start()
        {
        joy = FindObjectOfType(typeof(JoystickBehavior)) as JoystickBehavior;
        speed = 55;
        fp = FindObjectOfType(typeof(FollowPlayer)) as FollowPlayer;
        anim = fp.player.GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
        dir = joy.GetJoystickValues();
        if (dir != Vector3.zero)
        {
            transform.Translate(dir * speed, Space.World);
            anim.SetBool("isrunning", true);
        }
        else
        {
            anim.SetBool("isrunning", false);
        }
            //if (sl.Getfil() <= 0)
            //{
            //    Destroy(this.gameObject);
            //}
        }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "CubeDeFin")
        {
            SceneManager.LoadScene("Menu_2");
        }
    }
    private void OnDestroy()
    {
        if(Player.GetComponent.life < 0)
        {
            GameObject g = GameObject.FindGameObjectWithTag("Score");
            Destroy(g);
            SceneManager.LoadScene("Menu_2");
        }
    }
    */
}

