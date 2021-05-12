using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    private bool playerInRange;
    private float speed;
    public GameObject target;
    public float aggroRange;
    private SpriteRenderer sprite;
    private Animator anim;

    private float vidaAtual; //quantidade de vida atual do objeto
    public float vidaInicial; //quanto de vida o objeto tem no inicio
    public float[] dropRatio;
    public List<GameObject> prefabMorte; //objeto a ser isntanciado no momento da morte
    public float dano;
    public float cdAtaqueBasico;
    private float ataqueBasico = 0;

    public Slider Barravida;

    void Start()
    {
        vidaAtual = vidaInicial;
        Barravida.value = AtualizaMostrador();
        sprite = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        speed = Random.Range(-1f, 1f);
        
    }

    private void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        SeguePlayer();
        if (playerInRange && !main.BOSS_MORTO)
        {
            if (target.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector2(0,0);
            }
            else if (target.transform.position.x < transform.position.x)
                transform.eulerAngles = new Vector2(0, 180);
        }
    }

    public void TomaDano(float danoParaAplicar)
    {
        //diminui a quantidade de vida
        vidaAtual -= danoParaAplicar;
        Barravida.value = AtualizaMostrador();
        //verifica se a vida chegou a zero e chama a função de morte em caso positivo
        if (vidaAtual <= 0)
        {           
            Morre();
        }
    }

    private void SeguePlayer()
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) < aggroRange && Mathf.Abs(transform.position.y - target.transform.position.y) < aggroRange && !main.BOSS_MORTO)
        {
            
            if (Mathf.Abs(target.transform.position.x - transform.position.x) > 2)
            {
                playerInRange = true;
                speed = 3f;
            }

            else
            {
                speed = 0;
            }
        }
        else
        {
            playerInRange = false;
            speed = Random.Range(-1f, 1f);
        }

    } 

    float AtualizaMostrador()
    {
        return vidaAtual / vidaInicial;
    }

    //Instancia o prefab de morte e destroi o objeto morto
    void Morre()
    {
        speed = 0;
        main.DRILL_PUNCH = false;
        anim.Play("DrillBot_Explosao");
        anim.SetBool("Atacando", false);
        anim.SetBool("Death", true);    
        vidaAtual = 0;
        DropaItem();
        Destroy(gameObject, 1.25f);
        main.BOSS_MORTO = true;
    }

    void DropaItem()
    {
        GameObject[] prefab = prefabMorte.ToArray();

        float drop = Random.Range(0, 100);

        for (int i = 0; i < dropRatio.Length; i++)
        {
            for (int j = 0; j < prefab.Length; j++)
            {
                if (drop <= dropRatio[i])
                {
                    Instantiate(prefabMorte[j], new Vector2(transform.position.x,transform.position.y - 1.8f), Quaternion.identity);

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Limite"))
            speed *= -1;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Melee")
            speed = 0;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Melee")
            speed = 1;
    }
}
