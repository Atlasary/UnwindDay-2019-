using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject proj;
    public Player player;
    public FollowPlayer fp;
    private JoystickBehavior joyright;
    private bool isshooting;

    void Start()
    {
        fp = GameObject.FindObjectOfType<FollowPlayer>();
        joyright = GameObject.Find("joyright").GetComponent<JoystickBehavior>();
        isshooting = false;
    }
    void Update()
    {
        if (!isshooting && joyright.GetJoystickValues()!=Vector3.zero)
        {
            StartCoroutine(ShootingPlayer());
        }
    }
    private IEnumerator ShootingPlayer()
    {
        isshooting = true;
        Instantiate(proj, transform.position, transform.rotation);
        fp.nbshoot++;
        yield return new WaitForSeconds(player.rate);
        isshooting = false;
    }
}
