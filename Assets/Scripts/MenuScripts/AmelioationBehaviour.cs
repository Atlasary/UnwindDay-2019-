using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmelioationBehaviour : MonoBehaviour
{
    public Text atttxt, hptxt, spetxt;
    public int attlvl, hplvl, spelvl;
    public GameObject[] attindic, hpindic,speindic;
    public GameObject controller;
    public int mode;
    public Text nsoul, esoul, hsoul;
    public Button attbut, hpbut, spebut;
    private float price;
    void Start()
    {
        
    }

    public void AmeliorationUpdate()
    {
        attlvl = controller.GetComponent<MenuSceneControler>().demonList[2].gameObject.GetComponent<PlayerGiver>().attlvl;
        hplvl = controller.GetComponent<MenuSceneControler>().demonList[2].gameObject.GetComponent<PlayerGiver>().hplvl;
        spelvl = controller.GetComponent<MenuSceneControler>().demonList[2].gameObject.GetComponent<PlayerGiver>().spelvl ;
        for (int i = 0; i < attlvl; i++)
        {
            attindic[i].GetComponent<Image>().color = new Color(1, 0, 0);
        }
        for (int i = attlvl; i < 10; i++)
        {
            attindic[i].GetComponent<Image>().color = new Color(1, 1, 1);
        }
        for (int i = 0; i < hplvl; i++)
        {
            hpindic[i].GetComponent<Image>().color = new Color(1, 0, 0);
        }
        for (int i = hplvl; i < 10; i++)
        {
            hpindic[i].GetComponent<Image>().color = new Color(1, 1, 1);
        }
        for (int i = 0; i < spelvl; i++)
        {
            speindic[i].GetComponent<Image>().color = new Color(1, 0, 0);
        }
        for (int i = spelvl; i < 10; i++)
        {
            speindic[i].GetComponent<Image>().color = new Color(1, 1, 1);
        }
        atttxt.text = "" + controller.GetComponent<MenuSceneControler>().choice.player.attack;
        hptxt.text = "" + controller.GetComponent<MenuSceneControler>().choice.player.life;
        spetxt.text = "" + controller.GetComponent<MenuSceneControler>().choice.player.timesp;
        attbut.interactable = !(attlvl >= 10);
        hpbut.interactable = !(hplvl >= 10);
        spebut.interactable = !(spelvl >= 10);
        PriceAmelioUpdate();
    }

    public void AmelioPurchase()
    {
        int n = PlayerPrefs.GetInt("NeutralSoul");
        int d = PlayerPrefs.GetInt("DarkSoul");
        int l = PlayerPrefs.GetInt("LightSoul");
        if(n< Mathf.Ceil(price)||d< Mathf.Floor(price / 10)||l< Mathf.Floor(price / 100))
        {
            Debug.Log("Not enough minerals !!");
            return;
        }
        n -= (int)Mathf.Ceil(price);
        d -= (int)Mathf.Floor(price / 10);
        l -= (int)Mathf.Floor(price / 100);
        PlayerPrefs.SetInt("NeutralSoul", n);
        PlayerPrefs.SetInt("DarkSoul", d);
        PlayerPrefs.SetInt("LightSoul", l);
        controller.GetComponent<MenuSceneControler>().onmenuscore.GetComponent<Score>().ActualizeOnMenu();
        if (mode == 0)
        {
            controller.GetComponent<MenuSceneControler>().demonList[2].gameObject.GetComponent<PlayerGiver>().attlvl++;
        }
        if (mode == 1)
        {
            controller.GetComponent<MenuSceneControler>().demonList[2].gameObject.GetComponent<PlayerGiver>().hplvl++;
        }
        if (mode == 2)
        {
            controller.GetComponent<MenuSceneControler>().demonList[2].gameObject.GetComponent<PlayerGiver>().spelvl++;
        }
        controller.GetComponent<MenuSceneControler>().DemonAmelioration();
        AmeliorationUpdate();
    }

    public void SetMode(int cmode)
    {
        mode = cmode;
        PriceAmelioUpdate();
    }

    private void PriceAmelioUpdate()
    {
        if (mode == 0)
        {
            float basef = controller.GetComponent<MenuSceneControler>().choice.player.battack;
            price = basef + 1.5f * basef * attlvl;
            nsoul.text = "" + Mathf.Ceil(price);
            esoul.text = "" + Mathf.Floor(price/10);
            hsoul.text = "" + Mathf.Floor(price / 100);
        }
        if (mode == 1)
        {
            float basef = controller.GetComponent<MenuSceneControler>().choice.player.blife;
            price = basef + 1.5f * basef * hplvl;
            nsoul.text = "" + Mathf.Ceil(price);
            esoul.text = "" + Mathf.Floor(price / 10);
            hsoul.text = "" + Mathf.Floor(price / 100);
        }
        if (mode == 2)
        {
            float basef = controller.GetComponent<MenuSceneControler>().choice.player.btimesp;
            price = basef + 1.5f * basef * spelvl;
            nsoul.text = "" + Mathf.Ceil(price);
            esoul.text = "" + Mathf.Floor(price / 10);
            hsoul.text = "" + Mathf.Floor(price / 100);
        }

    }
}
