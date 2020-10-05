using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLvl : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private float alpha;
    public int nblvl;
    public float radius;
    public GameObject bouton;
    private GameObject bt;
    public Canvas cnv;
    public Font font;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < nblvl; i++)
        {
            bt = Instantiate(bouton,transform.position +  new Vector3(radius*Mathf.Sin(2*3.1415f*i/nblvl), radius * Mathf.Cos(2 * 3.1415f * i / nblvl),0), Quaternion.identity);
            bt.name = "" + i;
            bt.transform.SetParent(cnv.transform);
            GameObject onbt = Instantiate(new GameObject(),bt.transform.position,bt.transform.rotation);
            onbt.name = "" + i + "nb";
            onbt.transform.SetParent(bt.transform);
            Text txt = onbt.AddComponent<Text>();
            txt.font = font;
            txt.fontSize = 50;
            txt.transform.position += new Vector3(35, -20, 0);
            txt.text = "" + i;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnDrag(PointerEventData eventData)
    {
        if((eventData.position.y - transform.position.y) > 0)
        {
            alpha = 180*Mathf.Atan((eventData.position.x - transform.position.x) / (eventData.position.y - transform.position.y))/3.1415f;
        }
        else
        {
            alpha = (eventData.position.x - transform.position.x)/Mathf.Abs((eventData.position.x - transform.position.x)) 
                *(90+180/3.1415f*Mathf.Atan((eventData.position.y - transform.position.y) 
                / -Mathf.Abs((eventData.position.x - transform.position.x))));
        }
        transform.eulerAngles = new Vector3(0, 0, -(Mathf.Round(alpha/(360/nblvl)))* (360 / nblvl));
    }

    public void OnEndDrag(PointerEventData eventData)           
    {
                                  
    }
    public void GoScene()
    {
        if(Mathf.Round(alpha / (360 / nblvl)) < 0)
        {
            SceneManager.LoadScene("Level"+ (nblvl + Mathf.Round(alpha / (360 / nblvl))));
        }
        else
        {
            SceneManager.LoadScene("Level" + Mathf.Round(alpha / (360 / nblvl)));
        }
    }

}
