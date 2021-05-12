using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegoColetivel : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coletível"))
            collision.gameObject.SetActive(false);
    }
}
