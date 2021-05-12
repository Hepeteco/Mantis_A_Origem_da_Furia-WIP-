using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScriptInimigo : MonoBehaviour
{

    public float velX = 5f;
    private float velY = 0;
    Rigidbody2D rb;
    public int dano;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(velX, velY);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Object.Destroy(gameObject, 0);
            col.SendMessageUpwards("TomaDano", dano);
        }
    }
}
