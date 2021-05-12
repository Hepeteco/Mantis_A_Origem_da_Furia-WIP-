using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComandoDeAtaqueBoss : MonoBehaviour
{

    private float ataqueBasico = 0;
    public float cdAtaqueBasico;

    public int dano;

    public Collider2D ativaAtaque;
    public GameObject boss;

    public Animator anim;

    private int counter = 0;

    // Use this for initialization
    void Start()
    {
        ativaAtaque.enabled = false;
        anim = boss.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
            if (ataqueBasico > 0)
            {
            ativaAtaque.enabled = true;
            ataqueBasico -= Time.deltaTime;
            if(ataqueBasico -Time.deltaTime >=.25f)
            {
                ativaAtaque.enabled = false;
               
            }                    
        }
            else
            {             
                main.DRILL_PUNCH = false;
            if (!main.BOSS_MORTO)
                anim.Play("DrillBot_Idle");
            else
                anim.Play("DrillBot_Explosao");
        }
    }

    void Update()
    {
        if (!main.DRILL_PUNCH && !main.BOSS_MORTO)
        {
            anim.Play("DrillBot_Melee");
            anim.SetBool("Atacando", true);
            anim.SetBool("Death", false);
            anim.SetBool("Atirando", false);
            main.DRILL_PUNCH = true;
            ataqueBasico = cdAtaqueBasico;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && counter == 0)
        {
            col.SendMessageUpwards("TomaDano", dano);
            counter++;
        }           
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && counter > 0)
            counter = 0;
    }
}
