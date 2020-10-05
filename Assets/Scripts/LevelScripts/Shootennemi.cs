using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootennemi : MonoBehaviour {
    public GameObject proj;
    private FollowPlayer fp;
    private bool isshooting;
    private int distance;
    public Enemy enemy;

	// Use this for initialization
	void Start () {
        distance = 25;
        isshooting = false;
        fp = FindObjectOfType(typeof(FollowPlayer)) as FollowPlayer;
    }
    void Update()
    {
        if (!isshooting && (transform.position - fp.player.transform.position).magnitude<distance) 
        {
            StartCoroutine(Shooting());
        }
    }
    IEnumerator Shooting()
    {
        isshooting = true;
        yield return new WaitForSeconds(enemy.rate);
        Instantiate(proj, transform.position, transform.rotation).transform.Rotate(0, 0, Random.Range(-5, 5));
        isshooting = false;
    }

}
