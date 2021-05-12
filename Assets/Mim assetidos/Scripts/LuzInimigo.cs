using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzInimigo : MonoBehaviour {

    public GameObject mantis;
    Vector2 mantisPos;

    void Start()
    {
        mantis = GetComponent<GameObject>();
        mantisPos = new Vector2(mantis.transform.position.x, mantis.transform.position.y);
    }

    void FixedUpdate () {
        transform.position =mantisPos;
		
	}
}
