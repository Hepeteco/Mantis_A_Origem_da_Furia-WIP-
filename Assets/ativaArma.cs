using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ativaArma : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            main.ARMA = true;
            Destroy(gameObject);
        }            
    }
}
