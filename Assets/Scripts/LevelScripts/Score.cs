using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {
    public GameObject neutralsoul, darksoul, lightsoul;
    public int n, d,l;
    private float i;
    void Awake()
    {
        n = 0;
        d = 0;
        l = 0;
        darksoul.GetComponent<Text>().text = "+ " + d;
        neutralsoul.GetComponent<Text>().text = "+ " + n;
        lightsoul.GetComponent<Text>().text = "+ " + l;
    }
    private void Update()
    {

    }
    public void ScoreActualizeOnLevel()
    {
        i = Random.value;
        if (i < 0.8f)
        {
            n++;
            neutralsoul.GetComponent<Text>().text = "+ " +n;
        }
        else
        {
            d++;
            darksoul.GetComponent<Text>().text = "+ " + d;
        }
    }
    public void ActualizeOnMenu()
    {
        neutralsoul.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("NeutralSoul");
        darksoul.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("DarkSoul");
        lightsoul.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("LightSoul");
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("NeutralSoul", PlayerPrefs.GetInt("NeutralSoul") + n);
        PlayerPrefs.SetInt("DarkSoul", PlayerPrefs.GetInt("DarkSoul") + d);
        PlayerPrefs.SetInt("LightSoul", PlayerPrefs.GetInt("LightSoul") + l);
    }
}
