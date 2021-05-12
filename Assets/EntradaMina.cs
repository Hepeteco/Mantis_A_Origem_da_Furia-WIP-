using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaMina : MonoBehaviour {
    public GameObject InicioFase;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            col.transform.position = new Vector2(InicioFase.transform.position.x, InicioFase.transform.position.y);
    }
}
