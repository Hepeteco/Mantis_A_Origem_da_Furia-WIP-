using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrativa : MonoBehaviour {

    private int cont = 0;
    public GameObject NarrativaImagem;

	// Use this for initialization
	void Start () {
        NarrativaImagem.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(cont > 0)
        {
            NarrativaImagem.SetActive(false);
        }
	}

    public void ProxCut()
    {
        cont++;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            NarrativaImagem.SetActive(true);
    }
}
