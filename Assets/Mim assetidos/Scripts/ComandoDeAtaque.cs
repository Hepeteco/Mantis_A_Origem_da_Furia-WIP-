using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComandoDeAtaque : MonoBehaviour { 

    private bool ataque = false;

    private float tempoAtaque = 0;
    private float cdAtaque = 0.20f;

    public int dano;

    public Collider2D ativaAtaque;
    public Animator anim;
    public AudioSource swing;
    public AudioClip[] hits;

    // Use this for initialization
    void Start () {
        ativaAtaque.enabled = false;
        swing = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !ataque)
        {
            anim.Play("Club_Ataque");
            ataque = true;
            tempoAtaque = cdAtaque;
            swing.Play();
            ativaAtaque.enabled = true;
        }

    }

    void LateUpdate()
    {

        if (ataque)
        {

            if (tempoAtaque > 0)
                tempoAtaque -= Time.deltaTime;

            else
            {
                anim.Play("Walk");
                ataque = false;
                ativaAtaque.enabled = false;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Inimigo"))
        { 
            col.SendMessageUpwards("TomaDano", dano);
            GameObject som = new GameObject();
            som.transform.position = transform.position;
            int randomClip = Random.Range(0, hits.Length);
            AudioSource audioSource = som.AddComponent<AudioSource>();
            audioSource.clip = hits[randomClip];
            audioSource.volume = 0.2f;
            audioSource.Play();
            Destroy(som, hits[randomClip].length);
        }

        else if (col.gameObject.tag == "Boss")
        { 
            col.SendMessageUpwards("TomaDano", dano);
            GameObject som = new GameObject();
            som.transform.position = transform.position;
            int randomClip = Random.Range(0, hits.Length);
            AudioSource audioSource = som.AddComponent<AudioSource>();
            audioSource.clip = hits[randomClip];
            audioSource.volume = 0.2f;
            audioSource.Play();
            Destroy(som, hits[randomClip].length);
        }
    }
}
