using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPositionGiver : MonoBehaviour
{
    public Quaternion position;
    public GameObject controller;
    public string level;
    public string[] obj;
    public bool[] check;
    public Sprite[] rewardIcon;
    public Vector3[] reward;
    public string[] funcobj;
    public float[] arguments;

    private void Start()
    {
        check = new bool[4];
        check[0] = PlayerPrefs.GetInt(level+"_check_0") == 1;
        check[1] = PlayerPrefs.GetInt(level + "_check_1") == 1;
        check[2] = PlayerPrefs.GetInt(level + "_check_2") == 1;
        check[3] = PlayerPrefs.GetInt(level + "_check_3") == 1;
    }

    public void TerrainGoto()
    {
        Quaternion prev = controller.GetComponent<MenuSceneControler>().terrain.rectint[0];
        controller.GetComponent<MenuSceneControler>().terrain.switchToNewState(prev, position,false);
        controller.GetComponent<MenuSceneControler>().currentLevel = this;
        controller.GetComponent<MenuSceneControler>().LevelUpdate();

        ChoiceOfPlayer choice = controller.GetComponent<MenuSceneControler>().choice;
        choice.objLevel = obj;
        choice.tickLevel = check;
        choice.spriteLevel = rewardIcon;
        choice.reward = reward;
        choice.funcobj = funcobj;
        choice.arguments = arguments;

    }
}
