using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    private bool laser;
	// Use this for initialization
	void Start () {
        laser = true;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && laser)
        {
            col.gameObject.SendMessageUpwards("TomaDano", main.VIDA);
        }
    }
}
