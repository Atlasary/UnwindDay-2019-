using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMenu : MonoBehaviour {
    private ChoiceOfPlayer choiceplayer;
    private Vector3 deplacement = new Vector3(0, 10, 0);
    private GameObject onmenuscore;
    private void Start()
    {
        onmenuscore = GameObject.FindGameObjectWithTag("Score");
    }

    public void GoScene(int i)
    {
        transform.position = deplacement*i;
    }
    public void GoLvl(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }
    public void LoadPlayerInScene(GameObject playerchoice)
    {
        choiceplayer = FindObjectOfType<ChoiceOfPlayer>() as ChoiceOfPlayer;
        choiceplayer.player = playerchoice.GetComponent<Player>();
    }
    public void QuitPlaying()
    {
        Application.Quit();
    }
    public void EraseData()
    {
        PlayerPrefs.SetInt("DarkSoul", 0);
        PlayerPrefs.SetInt("LightSoul", 0);
        PlayerPrefs.SetInt("NeutralSoul", 0);
        onmenuscore.GetComponent<Score>().ActualizeOnMenu();
    }
}
