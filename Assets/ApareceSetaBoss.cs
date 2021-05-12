using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApareceSetaBoss : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (main.BOSS_MORTO)
            gameObject.SetActive(true);
	}
}
