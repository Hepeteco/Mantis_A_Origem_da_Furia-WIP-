using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Recover : MonoBehaviour {
    public float vida;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 9)
        {
            Physics2D.IgnoreLayerCollision(8, 9);
        }

        else if (col.gameObject.layer == 10 && gameObject.tag.Equals("HealthRegen"))
        {
                Destroy(gameObject);
            col.SendMessageUpwards("HealthRegen", vida);
        }
    }
}
