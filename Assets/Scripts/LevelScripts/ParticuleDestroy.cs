using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Destroy());
	}
	
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
