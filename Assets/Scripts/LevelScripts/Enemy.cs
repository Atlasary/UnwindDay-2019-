using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private FollowPlayer fp;
    private GameObject Player;
    private GameObject score;
    public float life;
    public float attack;
    public float rate;
    public float speedproj;
    public GameObject lifeEnemy;
    public float lifeScale;
    public float blife;

    void Start()
    {
        lifeScale = lifeEnemy.transform.localScale.x;
        blife = life;
        fp = FindObjectOfType<FollowPlayer>();
        score = GameObject.FindGameObjectWithTag("Score");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        transform.LookAt(Player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            if (life <= 0)
            {
                score.GetComponent<Score>().ScoreActualizeOnLevel();
                fp.nbEnemy--;
                fp.nbEnemyKilled++;
                Destroy(this.gameObject);
            }
        }
    }
}
