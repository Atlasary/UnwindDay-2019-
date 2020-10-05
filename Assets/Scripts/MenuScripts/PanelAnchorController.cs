using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAnchorController : MonoBehaviour
{
    public RectTransform rect;
    public int step;
    public List<Quaternion> rectint;
    private int currentstep;
    // Start is called before the first frame update

    void Start()
    {
        this.gameObject.transform.localScale = Vector3.zero;
        rect.anchorMin = new Vector2(rectint[0].x, rectint[0].y);
        rect.anchorMax = new Vector2(rectint[0].z, rectint[0].w);
        currentstep = 0;
    }
    
    public void switchToNextState()
    {
        if (currentstep == 0)
            this.gameObject.transform.localScale = Vector3.one;
        StartCoroutine(Switch(rectint[currentstep],rectint[1- currentstep]));
    }

    private IEnumerator Switch(Quaternion prev, Quaternion next)
    {
        float x = prev.x;
        float y = prev.y;
        float z = prev.z;
        float w = prev.w;
        for(int i = 0; i < step; i++)
        {
            x += (next.x - prev.x) / step;
            y += (next.y - prev.y) / step;
            z += (next.z - prev.z) / step;
            w += (next.w - prev.w) / step;
            rect.anchorMin = new Vector2(x, y);
            rect.anchorMax = new Vector2(z, w);
            yield return new WaitForSeconds(1/step);
        }
        rect.anchorMin = new Vector2(next.x, next.y);
        rect.anchorMax = new Vector2(next.z, next.w);
        if (currentstep == 1)
            this.gameObject.transform.localScale = Vector3.zero;
        currentstep = 1 - currentstep;
    }

    public void switchToState(int state)
    {
        currentstep = 1 - state;
        switchToNextState();
    }
}
