using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {
    private Vector3 scale;
    private FollowPlayer fp;
    private Player player;
    private float totlife;

    // Use this for initialization
    void Start () {
        scale = new Vector3(1, 1, 1);
        fp = FindObjectOfType<FollowPlayer>() as FollowPlayer;
        player = fp.player.GetComponent<Player>();
        totlife = player.life;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        scale.y = player.life/totlife;
        transform.localScale = scale;
	}
}
