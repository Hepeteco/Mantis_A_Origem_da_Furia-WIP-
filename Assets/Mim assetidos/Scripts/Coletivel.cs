using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletivel : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 9)
        {
            Physics2D.IgnoreLayerCollision(8, 9);
        }

        else if (col.gameObject.layer == 10 && gameObject.tag.Equals("Cristal"))
        {
            main.CRISTAL += 1;
            gameObject.SetActive(false);
        }
    }
}
