using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGiver : MonoBehaviour
{
    public enum State
    {
        notValid = 0,
        valid = 1,
        obtained = 2
    }
    public MenuSceneControler controller;

    public State state;
    public GameObject tick;
    public Vector3 reward;

    public string function;
    public float argument;

    public void CkeckIfValidate()
    {
        StartCoroutine(function);
    }

    private IEnumerator NbEnemyKilled()
    {
        int nb = PlayerPrefs.GetInt("nbEnemyKilled");
        if (nb > argument)
        {
            state = State.valid;
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator TotalTime()
    {
        float time = PlayerPrefs.GetFloat("totalTime");
        if (time > argument)
        {
            state = State.valid;
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator TotalSoul()
    {
        int soul = PlayerPrefs.GetInt("NeutralSoul")
            + PlayerPrefs.GetInt("DarkSoul")
            + PlayerPrefs.GetInt("LightSoul");
        if (soul > argument)
        {
            state = State.valid;
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator SummonAllDemon()
    {
        bool isSum = true;
        foreach (DemonAnchorController demon in controller.demonList)
        {
            if(demon.gameObject.GetComponent<PlayerGiver>() != null && !demon.gameObject.GetComponent<PlayerGiver>().isSummoned)
            {
                isSum = false;
            }
        }
        if (isSum)
        {
            state = State.valid;
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator ReachAttack()
    {
        float att = 0;
        foreach (DemonAnchorController demon in controller.demonList)
        {
            if (demon.gameObject.GetComponent<PlayerGiver>() != null && att< demon.gameObject.GetComponent<PlayerGiver>().player.GetComponent<Player>().attack)
            {
                att= demon.gameObject.GetComponent<PlayerGiver>().player.GetComponent<Player>().attack;
            }
        }
        if (att > argument)
        {
            state = State.valid;
        }
        yield return new WaitForEndOfFrame();
    }
}
