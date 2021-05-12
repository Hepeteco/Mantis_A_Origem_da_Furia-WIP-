using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contagem : MonoBehaviour {

    private int qtd = 0;

    public PegoColetivel pego;

	// Update is called once per frame
	void Update () {
        if (pego)
            qtd += 1;
        Debug.Log(qtd);
	}
}
