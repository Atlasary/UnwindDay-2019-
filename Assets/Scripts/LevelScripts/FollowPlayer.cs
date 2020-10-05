using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FollowPlayer : MonoBehaviour {
    public int nbEnemyKilled;
    public GameObject player;
    public GameObject winPanel, losePanel;
    public GameObject joy1, joy2, life,spebutton;
    private ChoiceOfPlayer choiceplayer;
    public Text[] objText;
    public GameObject[] objtick;
    public Image[] objsprite;
    public Text winntext, winltext, windtext, losentext, loselttext, losedtext;
    public Button OkButton;
    public Button tears;
    public Score score;

    private delegate bool Functions(float seconds);
    private static Dictionary<string, Functions> dicoFunction;
    public int nbEnemy;
    private float timer;
    public bool isDamaged,isSpeUsed;
    public int nbshoot;
    private float time;
    // Use this for initialization
    private void Awake()
    {
        time = Time.time;
        nbEnemyKilled = PlayerPrefs.GetInt("nbEnemyKilled");
        nbshoot = 0;
        isSpeUsed = false;
        //Functions endLevel = new Functions(EndLevel);
        dicoFunction = new Dictionary<string, Functions>();
        dicoFunction.Add("endLevel", EndLevel);
        dicoFunction.Add("timeLimit", TimeLimit);
        dicoFunction.Add("noDamage", NoDamage);
        dicoFunction.Add("killEveryEnemy", KillEveryEnemy);
        dicoFunction.Add("shootLimit", ShootLimit);
        dicoFunction.Add("noSpeAttack", NoSpeAttack);
        dicoFunction.Add("killBoss", KillBoss);
        choiceplayer = FindObjectOfType<ChoiceOfPlayer>() as ChoiceOfPlayer;
        player = choiceplayer.player.gameObject;
        player = Instantiate(player.gameObject,new Vector3(0,0,0), Quaternion.identity);
        player.name = "Player";
        timer = Time.time;
    }
    void Start() {
        OkButton.interactable = false;
        nbEnemy = FindObjectsOfType<Enemy>().Length;
    }
	
	void FixedUpdate ()
    {
        if(player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 22, player.transform.position.z-1);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevelAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenWinPanel()
    {
        joy1.SetActive(false);
        joy2.SetActive(false);
        life.SetActive(false);
        spebutton.SetActive(false);
        foreach(Enemy enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }
        Destroy(player);
        WinUpdate();
    }

    public void WinUpdate()
    {
        winntext.text = ""+score.n;
        windtext.text = ""+score.d;
        winltext.text = ""+score.l;
        for (int i = 0; i < 4; i++)
        {
            objText[i].text = choiceplayer.objLevel[i];
            objsprite[i].sprite = choiceplayer.spriteLevel[i];
            objtick[i].SetActive(choiceplayer.tickLevel[i]);//those already ok
        }
        winPanel.GetComponent<PanelAnchorController>().switchToNextState();
        StartCoroutine(WinAnimation());
    }

    private IEnumerator WinAnimation()
    {
        for(int i = 0; i < 4; i++)
        {
            if(!objtick[i].activeSelf && dicoFunction[choiceplayer.funcobj[i]](choiceplayer.arguments[i]))
            {
                objtick[i].SetActive(true);
                Vector3 tempScale = objtick[i].transform.localScale;
                objtick[i].transform.localScale = Vector3.zero;
                for(int j = 0; j < 10; j++)
                {
                    objtick[i].transform.localScale += tempScale / 10;
                    yield return new WaitForSeconds(0.1f);
                }
                for (int j = 0; j < 10; j++)
                {
                    winntext.text = ""+(int)(int.Parse(winntext.text)+choiceplayer.reward[i].x/10);
                    windtext.text = "" + (int)(int.Parse(windtext.text) + choiceplayer.reward[i].y / 10);
                    winltext.text = "" + (int)(int.Parse(winltext.text) + choiceplayer.reward[i].z / 10);
                    yield return new WaitForSeconds(0.1f);
                }
                score.n = int.Parse(winntext.text);
                score.d = int.Parse(windtext.text);
                score.l = int.Parse(winltext.text);
                print(SceneManager.GetActiveScene().name);
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_check_0", ConvertBoolToInt(objtick[0].activeSelf));
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_check_1", ConvertBoolToInt(objtick[1].activeSelf));
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_check_2", ConvertBoolToInt(objtick[2].activeSelf));
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_check_3", ConvertBoolToInt(objtick[3].activeSelf));
            }
        }
        OkButton.interactable = true;
    }

    public bool EndLevel(float none)
    {
        return true;
    }

    public bool TimeLimit(float seconds)
    {
        if (Time.time-timer<seconds)
        {
            return true;
        }
        return false;
    }

    public bool NoDamage(float none)
    {
        return !isDamaged;
    }

    public bool KillEveryEnemy(float none)
    {
        if(nbEnemy <= 0)
        {
            return true;
        }
        return false;
    }

    public bool ShootLimit(float limit)
    {
        if (nbshoot < limit)
        {
            return true;
        }
        return false;
    }

    public bool NoSpeAttack(float none)
    {
        if (!isSpeUsed)
        {
            return true;
        }
        return false;
    }

    public bool KillBoss(float none)
    {
        if (GameObject.FindGameObjectsWithTag("Enemy_boss").Length == 0)
        {
            return true;
        }
        return false;
    }

    public void OpenLosePanel()
    {
        losentext.text = "+"+score.n ;
        losedtext.text = "+"+score.d;
        loselttext.text = "+"+score.l;
        joy1.SetActive(false);
        joy2.SetActive(false);
        life.SetActive(false);
        spebutton.SetActive(false);
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }
        Destroy(player);
        losePanel.GetComponent<PanelAnchorController>().switchToNextState();
    }

    public static int ConvertBoolToInt(bool b)
    {
        if (b)
        {
            return 1;
        }
        return 0;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("nbEnemyKilled", nbEnemyKilled);
        PlayerPrefs.SetFloat("totalTime", PlayerPrefs.GetFloat("totalTime") + Time.time - time);
        PlayerPrefs.Save();
    }
}

