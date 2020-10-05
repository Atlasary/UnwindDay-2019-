using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceOfPlayer : MonoBehaviour {
    public Player player;
    public string[] objLevel;
    public bool[] tickLevel;
    public Sprite[] spriteLevel;
    public Vector3[] reward;
    public string[] funcobj;
    public float[] arguments;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
