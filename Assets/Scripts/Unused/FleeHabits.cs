using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeHabits : MonoBehaviour {
    private GameObject player;
    private bool isrunning;
    private FollowPlayer fp;
    private GameObject score;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        isrunning = false;
        fp = FindObjectOfType(typeof(FollowPlayer)) as FollowPlayer;
        score = GameObject.FindGameObjectsWithTag("Score")[0];
    }
	
	// Update is called once per frame
	void Update () {
        if (((transform.position - player.transform.position).magnitude < 11 ) && (isrunning == false))
        {
            StartCoroutine(Flee());
        }
        if (isrunning == true)
        {
            //transform.Translate(0, 0, fp.Getvitenemy());
        }
	}

    IEnumerator Flee()
    {
        isrunning = true;
        yield return new WaitForSeconds(1.5f);
        isrunning = false;
    }
    private void OnDestroy()
    {
        score.GetComponent<Score>().ScoreActualizeOnLevel();
    }
}
