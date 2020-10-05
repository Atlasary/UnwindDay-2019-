using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjHabits : MonoBehaviour {
    public GameObject caracter;
    private bool isenemy;
    private FollowPlayer fp;
    private Vector3 inipos;
    public GameObject part;
    private bool bloodMode;

    // Use this for initialization
    void Start () {
        bloodMode = PlayerPrefs.GetInt("bloodMode")==1;
        fp = FindObjectOfType(typeof(FollowPlayer)) as FollowPlayer;
        inipos = transform.position;
        if(caracter.tag == "Player")
        {
            isenemy = false;
        }
        else
        {
            isenemy = true;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isenemy)
        {
            transform.Translate(Vector3.up * Time.deltaTime * caracter.GetComponent<Enemy>().speedproj, Space.Self);
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * caracter.GetComponent<Player>().speedproj, Space.Self);
        }
        if ((transform.position - inipos).magnitude>60)     //destruction du proj si trop loin de l'emetteur
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && isenemy)
        {
            if (!bloodMode)
            {
                Instantiate(part, transform.position, transform.rotation);
            }
            other.GetComponent<Player>().life -= (int)caracter.GetComponent<Enemy>().attack;
            if (fp.player.GetComponent<Player>().life <= 0)
            {
                fp.OpenLosePanel();
            }
            fp.isDamaged = true;
            Destroy(this.gameObject);
        }
        if(other.tag == "Enemy" || other.tag == "Enemy_boss")
        {
            Instantiate(part, transform.position, transform.rotation);
            if (isenemy)
            {
                other.GetComponent<Enemy>().life -= caracter.GetComponent<Enemy>().attack;
                other.GetComponent<Enemy>().lifeEnemy.transform.localScale = Vector3.right * other.GetComponent<Enemy>().life * other.GetComponent<Enemy>().lifeScale / other.GetComponent<Enemy>().blife  + Vector3.up*0.01f;
            }
            else
            {
                other.GetComponent<Enemy>().life -= caracter.GetComponent<Player>().attack;
                other.GetComponent<Enemy>().lifeEnemy.transform.localScale = Vector3.right * other.GetComponent<Enemy>().life * other.GetComponent<Enemy>().lifeScale / other.GetComponent<Enemy>().blife+Vector3.up*0.01f;
            }
            Destroy(this.gameObject);
        }
        if(other.name == "Wall")
        {
            Destroy(this.gameObject);
        }        
    }
}
