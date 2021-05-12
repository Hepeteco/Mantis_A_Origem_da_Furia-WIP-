using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmaCraft : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.SendMessageUpwards("Criar");

        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.SendMessageUpwards("Criar");

        }
    }
}
