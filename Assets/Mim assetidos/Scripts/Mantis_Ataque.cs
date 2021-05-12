using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantis_Ataque : MonoBehaviour {
    public int dano;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            col.SendMessageUpwards("TomaDano", dano);
    }
}
