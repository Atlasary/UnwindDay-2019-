using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAnchorController : MonoBehaviour
{
    public RectTransform rect;
    private int step;
    public List<Quaternion> rectint;
    private int currentstep;
    public ButtonBlocker left, right;
    // Start is called before the first frame update

    void Start()
    {
        step = 10;
    }

    public void switchToNewState(Quaternion prev, Quaternion next, bool cond)
    {
       StartCoroutine(DemonSwitch(prev, next,cond));
    }


    private IEnumerator DemonSwitch(Quaternion prev, Quaternion next,bool cond)
    {
        float x = prev.x;
        float y = prev.y;
        float z = prev.z;
        float w = prev.w;
        for (int i = 0; i < step; i++)
        {
            x += (next.x - prev.x) / step;
            y += (next.y - prev.y) / step;
            z += (next.z - prev.z) / step;
            w += (next.w - prev.w) / step;
            rect.anchorMin = new Vector2(x, y);
            rect.anchorMax = new Vector2(z, w);
            yield return new WaitForSeconds(1 / step);
        }
        rect.anchorMin = new Vector2(next.x, next.y);
        rect.anchorMax = new Vector2(next.z, next.w);
        rectint[0] = next;
        if (cond)
        {
            left.SwitchButtonState(true);
            right.SwitchButtonState(true);
        }
    }
}
