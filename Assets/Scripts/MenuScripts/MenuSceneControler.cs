using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneControler : MonoBehaviour
{
    public GameObject GUIDisable;
    public GameObject fader;
    public GameObject panelKill;
    public GameObject panelDemon;
    public GameObject panelObj;
    public GameObject panelInfo;
    public GameObject panelShop;
    public GameObject panelSettings;
    public GameObject onmenuscore;

    public GameObject ameliobutton, summonbutton;
    public Text nSumDen, dSumDem, lSumDem;

    public AmelioationBehaviour panelAmelio;

    public DemonAnchorController[] demonList;
    
    public ChoiceOfPlayer choice;
    private int current;
    private float time;
    public ButtonBlocker left, right;
    private void Awake()
    {
        time = Time.time;
        current = 2;
        objcurrent = 1;
        choice = GameObject.FindGameObjectWithTag("ChoiceOfPlayer").GetComponent<ChoiceOfPlayer>();
        choice.player = demonList[current].gameObject.GetComponent<PlayerGiver>().player.GetComponent<Player>();
        fader.SetActive(false);
        foreach(DemonAnchorController demon in demonList)
        {
            RectTransform rect = demon.gameObject.GetComponent<RectTransform>();
            demon.rectint[0] = new Quaternion(rect.anchorMin[0], rect.anchorMin[1], rect.anchorMax[0], rect.anchorMax[1]);
        }
        foreach (DemonAnchorController goal in objList)
        {
            if (goal.gameObject.GetComponent<ObjGiver>() != null)
            {
                goal.gameObject.GetComponent<ObjGiver>().state = (ObjGiver.State) PlayerPrefs.GetInt(goal.gameObject.name);
            }
        }
        panelAmelio.AmeliorationUpdate();
    }
    private void Start()
    {
        onmenuscore.GetComponent<Score>().ActualizeOnMenu();
        choice.objLevel = currentLevel.obj;
        choice.tickLevel = currentLevel.check;
        choice.spriteLevel = currentLevel.rewardIcon;
        choice.reward = currentLevel.reward;
        choice.funcobj = currentLevel.funcobj;
        choice.arguments = currentLevel.arguments;
        LevelUpdate();
        SummonDemonUpdate();
        ObjPanelUpdate();
    }

    public void switchToKill()
    {
        StartCoroutine(FadeAway(true, false, false, false,false,false));
    }

    public void switchToDemon()
    {
        StartCoroutine(FadeAway(false, true, false, false, false, false));
    }

    public void switchToObj()
    {
        StartCoroutine(FadeAway(false, false, true, false, false, false));
    }

    public void switchToInfo()
    {
        StartCoroutine(FadeAway(false, false, false, true, false, false));
        bool bloodMode = PlayerPrefs.GetInt("bloodMode") == 1;
        if (bloodMode)
        {
            bloodModeText.text = "Yes";
        }
        else
        {
            bloodModeText.text = "No";
        }
    }

    public void switchToShop()
    {
        StartCoroutine(FadeAway(false, false, false, false, true, false));
    }

    public void switchToSettings()
    {
        StartCoroutine(FadeAway(false, false, false, false, false, true));
    }

    private IEnumerator FadeAway(bool kill,bool demon,bool obj,bool info,bool shop,bool set)
    {
        fader.SetActive(true);
        GUIDisable.SetActive(true);
        Color c = fader.GetComponent<Image>().color;
        for (int i = 0; i < 5; i++)
        {
            c = new Color(c.r, c.g, c.b, c.a + 0.2f);
            fader.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a);
            yield return new WaitForSeconds(0.08f);
        }

        panelKill.SetActive(kill);
        panelDemon.SetActive(demon);
        panelObj.SetActive(obj);
        panelInfo.SetActive(info);
        panelShop.SetActive(shop);
        panelSettings.SetActive(set);

        c = fader.GetComponent<Image>().color;
        for (int i = 0; i < 5; i++)
        {
            c = new Color(c.r, c.g, c.b, c.a - 0.2f);
            fader.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a);
            yield return new WaitForSeconds(0.08f);
        }
        GUIDisable.SetActive(false);
        fader.SetActive(false);
        if (obj)
        {
            foreach(DemonAnchorController goal in objList)
            {
                if(goal.gameObject.GetComponent<ObjGiver>() != null && goal.gameObject.GetComponent<ObjGiver>().state != ObjGiver.State.obtained)
                {
                    goal.gameObject.GetComponent<ObjGiver>().CkeckIfValidate();
                }
                ObjPanelUpdate();
            }
        }
    }

    public void EraseData()
    {
        PlayerPrefs.SetInt("DarkSoul", 0);
        PlayerPrefs.SetInt("LightSoul", 0);
        PlayerPrefs.SetInt("NeutralSoul", 0);
        PlayerPrefs.SetFloat("totalTime", 0);
        for(int i = 1; i < SceneManager.sceneCount - 1; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                PlayerPrefs.SetInt("Level"+i + "_check_"+j, 0);
            }
        }
        foreach(DemonAnchorController demon in demonList)
        {
            if(demon.gameObject.GetComponent<PlayerGiver>() != null)
            {
                GameObject player = demon.gameObject.GetComponent<PlayerGiver>().player;
                PlayerPrefs.SetInt(player.name + "_isSummoned", 0);
                PlayerPrefs.SetInt(player.name + "_attlvl", 0);
                PlayerPrefs.SetInt(player.name + "_hplvl", 0);
                PlayerPrefs.SetInt(player.name + "_spelvl", 0);
                player.GetComponent<Player>().attack = player.GetComponent<Player>().battack;
                player.GetComponent<Player>().life = player.GetComponent<Player>().blife;
                player.GetComponent<Player>().timesp = player.GetComponent<Player>().btimesp;
            }
        }
        foreach (DemonAnchorController goal in objList)
        {
            if (goal.gameObject.GetComponent<ObjGiver>() != null)
            {
                PlayerPrefs.SetInt(goal.gameObject.name,(int) goal.gameObject.GetComponent<ObjGiver>().state);
            }
        }
        time = Time.time;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DemonSwitchLeft()
    {
        left.SwitchButtonState(false);
        right.SwitchButtonState(false);
        if (current == demonList.Length - 1)
        {
            left.SwitchButtonState(true);
            right.SwitchButtonState(true);
            return;
        }        //switch demon place
        print("nn");
        DemonAnchorController temp = demonList[demonList.Length-1];
        for (int i =1;i<demonList.Length;i++)
        {
            demonList[i].switchToNewState(demonList[i].rectint[0], demonList[i - 1].rectint[0],false);
        }
        demonList[0].switchToNewState(demonList[0].rectint[0], temp.rectint[0],true);

        //switch position in list
        temp = demonList[0];
        for (int i = 1; i < demonList.Length; i++)
        {
            demonList[i - 1] = demonList[i];
        }
        demonList[demonList.Length - 1] = temp;
        current++;
        print("eee");
        //since we switched position, the current demon is always nb 2.
        if (demonList[2].gameObject.GetComponent<PlayerGiver>().isSummoned)
        {
            choice.player = demonList[2].gameObject.GetComponent<PlayerGiver>().player.GetComponent<Player>();
        }
        SummonDemonUpdate();
        panelAmelio.AmeliorationUpdate();
    }

    public void DemonSwitchRight()
    {
        left.SwitchButtonState(false);
        right.SwitchButtonState(false);
        if (current == 2)
        {
            left.SwitchButtonState(true);
            right.SwitchButtonState(true);
            return;
        }
        //switch demon place
        DemonAnchorController temp = demonList[0];
        for (int i = 0; i < demonList.Length-1; i++)
        {
            demonList[i].switchToNewState(demonList[i].rectint[0], demonList[i + 1].rectint[0],false);
        }
        demonList[demonList.Length - 1].switchToNewState(demonList[demonList.Length - 1].rectint[0], temp.rectint[0],true);

        //switch position in list
        temp = demonList[demonList.Length - 1];
        for (int i = demonList.Length-1; i > 0; i--)
        {
            demonList[i] = demonList[i-1];
        }
        demonList[0] = temp;
        current--;
        //since we switched position, the current demon is always nb 2.
        choice.player = demonList[2].gameObject.GetComponent<PlayerGiver>().player.GetComponent<Player>();
        SummonDemonUpdate();
        panelAmelio.AmeliorationUpdate();
    }

    public void DemonAmelioration()
    {
        int mode = panelAmelio.mode;
        if (mode == 0)
        {
            choice.player.attack =  Mathf.Ceil(choice.player.attack*1.3f);
        }
        if (mode == 1)
        {
            choice.player.life = Mathf.Ceil(choice.player.life * 1.3f);
        }
        if (mode == 2)
        {
            choice.player.timesp = Mathf.Ceil(choice.player.timesp * 1.3f);
        }
    }

    public void SummonDemon()
    {
        int n = PlayerPrefs.GetInt("NeutralSoul");
        int d = PlayerPrefs.GetInt("DarkSoul");
        int l = PlayerPrefs.GetInt("LightSoul");
        Vector3 price = demonList[2].gameObject.GetComponent<PlayerGiver>().price;
        if (n < price[0] || d < price[1] || l < price[2])
        {
            Debug.Log("Not enough minerals !!");
            return;
        }
        n -= (int)price[0];
        d -= (int)price[1];
        l -= (int)price[2];
        PlayerPrefs.SetInt("NeutralSoul", n);
        PlayerPrefs.SetInt("DarkSoul", d);
        PlayerPrefs.SetInt("LightSoul", l);
        demonList[2].gameObject.GetComponent<PlayerGiver>().isSummoned = true;
        choice.player = demonList[2].gameObject.GetComponent<PlayerGiver>().player.GetComponent<Player>();
        SummonDemonUpdate();
    }

    public void SummonDemonUpdate()
    {
        ameliobutton.SetActive(demonList[2].gameObject.GetComponent<PlayerGiver>().isSummoned);
        summonbutton.SetActive(!demonList[2].gameObject.GetComponent<PlayerGiver>().isSummoned);
        nSumDen.text = ""+demonList[2].gameObject.GetComponent<PlayerGiver>().price[0];
        dSumDem.text = "" + demonList[2].gameObject.GetComponent<PlayerGiver>().price[1];
        lSumDem.text = "" + demonList[2].gameObject.GetComponent<PlayerGiver>().price[2];
        foreach (DemonAnchorController demon in demonList)
        {
            if (demon.gameObject.GetComponent<PlayerGiver>() == null) { }
            else if (demon.gameObject.GetComponent<PlayerGiver>().isSummoned)
            {
                demon.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            }
            else
            {
                demon.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.7f);
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    public DemonAnchorController terrain;
    public TerrainPositionGiver currentLevel;
    public Text[] _obj;
    public GameObject[] _check;
    public Image[] _rewardIcon;

    public void GoLevel()
    {
        foreach (DemonAnchorController demon in demonList)
        {
            if (demon.gameObject.GetComponent<PlayerGiver>() != null)
            {
                GameObject player = demon.gameObject.GetComponent<PlayerGiver>().player;
                PlayerPrefs.SetInt(player.name + "_isSummoned", FollowPlayer.ConvertBoolToInt(demon.gameObject.GetComponent<PlayerGiver>().isSummoned));
                PlayerPrefs.SetInt(player.name + "_attlvl", demon.gameObject.GetComponent<PlayerGiver>().attlvl);
                PlayerPrefs.SetInt(player.name + "_hplvl", demon.gameObject.GetComponent<PlayerGiver>().hplvl);
                PlayerPrefs.SetInt(player.name + "_spelvl", demon.gameObject.GetComponent<PlayerGiver>().spelvl);
            }
        }
        PlayerPrefs.SetFloat("totalTime", PlayerPrefs.GetFloat("totalTime") + Time.time - time);
        PlayerPrefs.Save();
        SceneManager.LoadScene(currentLevel.level);
    }

    public void LevelUpdate()
    {
        for(int i = 0; i< 4; i++)
        {
            _check[i].SetActive(currentLevel.check[i]);
            _obj[i].text = currentLevel.obj[i];
            _rewardIcon[i].sprite = currentLevel.rewardIcon[i];
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////
    public DemonAnchorController[] objList;
    private int objcurrent;
    public GameObject getRewardButton;
    public ButtonBlocker up, down;
    public void ObjSwitchUp()
    {
        up.SwitchButtonState(false);
        down.SwitchButtonState(false);
        if (objcurrent == objList.Length-1)
        {
            up.SwitchButtonState(true);
            down.SwitchButtonState(true);
            return;
        }
        objcurrent++;
        DemonAnchorController temp = objList[objList.Length -1];
        for (int i = 1; i < objList.Length; i++)
        {
            objList[i].switchToNewState(objList[i].rectint[0], objList[i - 1].rectint[0],false);
        }
        objList[0].switchToNewState(objList[0].rectint[0], temp.rectint[0],true);

        //switch position in list
        temp = objList[0];
        for (int i = 1; i < objList.Length; i++)
        {
            objList[i - 1] = objList[i];
        }
        objList[objList.Length - 1] = temp;
        if (objList[1].gameObject.GetComponent<ObjGiver>().state == ObjGiver.State.valid)
        {
            getRewardButton.GetComponent<PanelAnchorController>().switchToState(1);
        }
        else
        {
            getRewardButton.GetComponent<PanelAnchorController>().switchToState(0);
        }
    }

    public void ObjSwitchDown()
    {
        up.SwitchButtonState(false);
        down.SwitchButtonState(false);
        if (objcurrent == 1)
        {
            up.SwitchButtonState(true);
            down.SwitchButtonState(true);
            return;
        }
        objcurrent--;
        DemonAnchorController temp = objList[0];
        for (int i = 0; i < objList.Length - 1; i++)
        {
            objList[i].switchToNewState(objList[i].rectint[0], objList[i + 1].rectint[0],false);
        }
        objList[objList.Length - 1].switchToNewState(objList[objList.Length - 1].rectint[0], temp.rectint[0],true);

        temp = objList[objList.Length - 1];
        for (int i = objList.Length - 1; i > 0; i--)
        {
            objList[i] = objList[i - 1];
        }
        objList[0] = temp;
        if (objList[1].gameObject.GetComponent<ObjGiver>().state == ObjGiver.State.valid)
        {
            getRewardButton.GetComponent<PanelAnchorController>().switchToState(1);
        }
        else
        {
            getRewardButton.GetComponent<PanelAnchorController>().switchToState(0);
        }
    }

    public void ObjPanelUpdate()
    {
        foreach(DemonAnchorController obj in objList)
        {
            if(obj.gameObject.GetComponent<ObjGiver>() != null)
            {
                if (obj.gameObject.GetComponent<ObjGiver>().state == ObjGiver.State.notValid)
                {
                    FadeAllObj(1, obj.gameObject);
                    obj.gameObject.GetComponent<ObjGiver>().tick.SetActive(false);
                }
                else if (obj.gameObject.GetComponent<ObjGiver>().state == ObjGiver.State.valid)
                {
                    FadeAllObj(1, obj.gameObject);
                    obj.gameObject.GetComponent<ObjGiver>().tick.SetActive(true);
                }
                else
                {
                    FadeAllObj(0.5f, obj.gameObject);
                    //obj.gameObject.GetComponent<ObjGiver>().tick.SetActive(true);//not necessary i think...
                }
            }

        }
    }

    private void FadeAllObj(float a,GameObject obj)
    {
        Color c = obj.GetComponent<Image>().color;
        obj.GetComponent<Image>().color = new Color(c.r, c.g, c.b, a);
        foreach(Transform child in obj.transform)
        {
            if(child.GetComponent<Image>() != null)
            {
            c = child.GetComponent<Image>().color;
            child.GetComponent<Image>().color = new Color(c.r, c.g, c.b, a);
            }
        }
    }

    public void GetReward()
    {
        getRewardButton.GetComponent<PanelAnchorController>().switchToNextState();
        objList[1].gameObject.GetComponent<ObjGiver>().state = ObjGiver.State.obtained;
        int n = PlayerPrefs.GetInt("NeutralSoul");
        int d = PlayerPrefs.GetInt("DarkSoul");
        int l = PlayerPrefs.GetInt("LightSoul");
        Vector3 reward = objList[1].gameObject.GetComponent<ObjGiver>().reward;
        n += (int)reward[0];
        d += (int)reward[1];
        l += (int)reward[2];
        PlayerPrefs.SetInt("NeutralSoul", n);
        PlayerPrefs.SetInt("DarkSoul", d);
        PlayerPrefs.SetInt("LightSoul", l);
        onmenuscore.GetComponent<Score>().ActualizeOnMenu();
        PlayerPrefs.SetInt(objList[1].gameObject.name, (int)objList[1].gameObject.GetComponent<ObjGiver>().state);
        ObjPanelUpdate();
    }

    //////////////////////////////////////////////
    public Text bloodModeText;

    public void OnBloodModeChange()
    {
        bool bloodMode = PlayerPrefs.GetInt("bloodMode") == 1;
        if (bloodMode)
        {
            bloodModeText.text = "Yes";
        }
        else
        {
            bloodModeText.text = "No";
        }
        bloodMode = !bloodMode;
        print(bloodMode);
        PlayerPrefs.SetInt("bloodMode", FollowPlayer.ConvertBoolToInt(bloodMode));
    }
}
