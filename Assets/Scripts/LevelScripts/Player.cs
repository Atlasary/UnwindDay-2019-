using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FollowPlayer fp;
    public float life,blife;
    public float attack,battack;
    public float timesp,btimesp;
    public string speAttname;
    public Shooting shooter;
    public float rate, speedproj;

    private Vector3 scale;
    private void Start()
    {
        scale = shooter.proj.transform.localScale;
        fp = FindObjectOfType(typeof(FollowPlayer)) as FollowPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "EndLevel")
        {
            fp.OpenWinPanel(); ;
        }
    }

    public void LaunchSpeAttack()
    {
        StartCoroutine(speAttname);
        fp.isSpeUsed = true;
    }

    private IEnumerator BigBullets()
    {
        float att = attack;
        shooter.proj.transform.localScale = scale*5;
        attack = att*2;
        yield return new WaitForSeconds(timesp);
        shooter.proj.transform.localScale = scale;
        attack = att;
    }

    private IEnumerator FreezeTime()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in enemies)
        {
            enemy.speedproj /= 4;
            enemy.rate *= 2;
        }
        yield return new WaitForSeconds(timesp);
        foreach (Enemy enemy in enemies)
        {
            enemy.speedproj *= 4;
            enemy.rate /= 2;
        }
    }

    private IEnumerator Gatling()
    {
        float ratef = rate;
        rate = ratef/3;
        yield return new WaitForSeconds(timesp);
        rate = ratef;
    }

    private void OnDestroy()
    {
        shooter.proj.transform.localScale = scale;
    }

}
