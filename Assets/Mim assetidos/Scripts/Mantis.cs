using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mantis : MonoBehaviour {
    public int dano;
    public Collider2D maozinha;
    bool playerInRange;
    private bool m_FacingRight = false;
    public float cd;
    float tempoAtaque = 0;
    bool ataque = false;
    private float speed;
    public GameObject target;
    public float aggroRange;
    private float posInicial;
    public float patrulha;
    private SpriteRenderer sprite;
    private Animator anim;
    
    public float vidaAtual; //quantidade de vida atual do objeto
    public float vidaInicial = 5; //quanto de vida o objeto tem no inicio
    public float[] dropRatio;
    public List<GameObject> prefabMorte; //objeto a ser isntanciado no momento da morte

    public AudioSource som;

    public Slider Barravida;

    private Collider2D colisor;

    void Start()
    {
        colisor = GetComponent<BoxCollider2D>();
        if (main.SCENE_NAME == "Fase1")
        {
            vidaAtual = vidaInicial;
            Barravida.value = AtualizaMostrador();
        }            
        if (main.SCENE_NAME == "Fase2")
        {
            vidaAtual = vidaInicial + 10;
            Barravida.value = AtualizaMostrador();
            Barravida.maxValue = vidaAtual;
        }
            
        som = GetComponent<AudioSource>();
        maozinha.enabled = false;
        sprite = GetComponent<SpriteRenderer>();
        posInicial = transform.position.x;
        target = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        speed = Random.Range(-1f, 1f);

    }

    private void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        SeguePlayer();
        if (playerInRange)
        { 
            if (target.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            else if (target.transform.position.x < transform.position.x)
                transform.eulerAngles = new Vector2(0, 0);
        }

        else if (!playerInRange)
        {
            posInicial = transform.position.x;
        }

        if (!ataque)
        {
            anim.Play("Mantis_Atacando");
            ataque = true;
            tempoAtaque = cd;
            maozinha.enabled = true;
        }
        Barravida.value = AtualizaMostrador();
    }

    void LateUpdate()
    {
        if (tempoAtaque > 0)
        {
            tempoAtaque -= Time.deltaTime;
            if (tempoAtaque - Time.deltaTime <= 0.25f)
            {
                maozinha.enabled = false;

            }
        }   

        else
        {
            anim.Play("Mantis_Walking");
            ataque = false;
            maozinha.enabled = false;
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
        if (Mathf.Abs(transform.position.x - target.transform.position.x) < aggroRange && Mathf.Abs(transform.position.y - target.transform.position.y) < aggroRange)
        {

            if (Mathf.Abs(target.transform.position.x - transform.position.x) > 1)
            {
                playerInRange = true;
                speed = -3f;
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

        private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    float AtualizaMostrador()
    {
        return vidaAtual;
    }

    //Instancia o prefab de morte e destroi o objeto morto
    void Morre()
    {
        colisor.enabled = false;
        som.Play();
        anim.Play("Mantis_Death");
        vidaAtual = 0;
        DropaItem();
        Destroy(gameObject, 0.5f);
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
                    Instantiate(prefabMorte[j], transform.position, Quaternion.identity);

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Inimigo"))
            speed = 0;
    }
}
