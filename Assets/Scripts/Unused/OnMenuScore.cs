using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMenuScore : MonoBehaviour {
    private int score;
    public string nameoforb;
	// Use this for initialization
	void Start () {
        score = PlayerPrefs.GetInt(nameoforb);
        GetComponent<Text>().text = "" + score;
	}
    public void Actualize()
    {
        score = PlayerPrefs.GetInt(nameoforb);
        GetComponent<Text>().text = "" + score;
    }
}
