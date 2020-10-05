using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesHabits : MonoBehaviour {
    private GameObject Player;
    private GameObject score;
	// Use this for initialization
	void Start () {
        score =  GameObject.FindGameObjectWithTag("Score");
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.LookAt(Player.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            score.GetComponent<Score>().ScoreActualizeOnLevel();
            Destroy(this.gameObject);
        }
    }

}
