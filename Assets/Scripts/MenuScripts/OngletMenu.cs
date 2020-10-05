using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngletMenu : MonoBehaviour {
    public GameObject demon, obj, lvl, info,shop,param;
    private GameObject[] pack;
    private void Start()
    {
        pack = new GameObject[6];
        pack[0] = demon;
        pack[1] = obj;
        pack[2] = lvl;
        pack[3] = info;
        pack[4] = shop;
        pack[5] = param;
    }
    public void Affonglet(GameObject g)
    {
        for(int i=0; i < pack.Length; i++)
        {
            if (pack[i] == g)
            {
                pack[i].SetActive(true);
            }
            else
            {
                pack[i].SetActive(false);
            }
        }
    }

}
