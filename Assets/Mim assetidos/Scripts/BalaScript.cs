using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript : MonoBehaviour {

    public float velX;
    private float velY = 0;
    Rigidbody2D rb;
    public int dano;

    public AudioClip[] hits;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(12, 11, true);
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(velX, velY);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Inimigo" || col.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
            col.SendMessageUpwards("TomaDano", dano);
            GameObject som = new GameObject();
            som.transform.position = transform.position;
            int randomClip = Random.Range(0, hits.Length);
            AudioSource audioSource = som.AddComponent<AudioSource>();
            audioSource.clip = hits[randomClip];
            audioSource.volume = 0.4f;
            audioSource.Play();
            Destroy(som, hits[randomClip].length);
        }
    }
}
