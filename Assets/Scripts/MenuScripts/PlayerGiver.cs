using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGiver : MonoBehaviour
{
    public int attlvl, hplvl, spelvl;
    public GameObject player;
    public bool isSummoned;
    public Vector3 price;
    private void Awake()
    {
        isSummoned = PlayerPrefs.GetInt(player.name + "_isSummoned")==1;
        attlvl = PlayerPrefs.GetInt(player.name + "_attlvl");
        hplvl = PlayerPrefs.GetInt(player.name + "_hplvl");
        spelvl = PlayerPrefs.GetInt(player.name + "_spelvl");
        Player p = player.GetComponent<Player>();
        p.attack = p.battack;
        for(int i = 0; i < attlvl; i++)
        {
            p.attack = Mathf.Ceil(p.attack * 1.3f);
        }
        p.life = p.blife;
        for (int i = 0; i < attlvl; i++)
        {
            p.life = Mathf.Ceil(p.life * 1.3f);
        }
        p.timesp = p.btimesp;
        for (int i = 0; i < attlvl; i++)
        {
            p.timesp = Mathf.Ceil(p.timesp * 1.3f);
        }
    }
}
