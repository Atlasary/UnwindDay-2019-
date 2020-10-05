using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeAttackTrigger : MonoBehaviour
{
    public FollowPlayer fp;
    public bool istriggered;
    // Start is called before the first frame update
    void Start()
    {
        istriggered = false;
        transform.localScale = Vector3.one * 0.7f;
        GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!istriggered)
        {
            if (transform.localScale.x >= 1)
            {
                GetComponent<Button>().interactable = true;
                return;
            }
            transform.localScale += Time.deltaTime/10 * Vector3.one;
        }
    }

    public void TriggerAtt()
    {
        istriggered = true;
        GetComponent<Button>().interactable = false;
        StartCoroutine(ReduceAccToTime());
    }

    private IEnumerator ReduceAccToTime()
    {
        float speTime = fp.player.GetComponent<Player>().timesp;
        fp.player.GetComponent<Player>().LaunchSpeAttack();
        for (int i = 0; i < 100; i++)
        {
            transform.localScale -= Vector3.one*1f/100f;
            yield return new WaitForSeconds(speTime/100f);
        }
        istriggered = false;
    }
}
