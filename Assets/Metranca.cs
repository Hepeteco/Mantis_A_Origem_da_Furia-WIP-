using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Metranca : MonoBehaviour {

    bool playerInRange;
    public GameObject balaInimigoDireita;
    public GameObject balaInimigoEsquerda;
    Vector2 balaPos;
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


    public Slider Barravida;

    private Collider2D colisor;

    private int tiroCont = 0;


    void Start()
    {
        colisor = GetComponent<BoxCollider2D>();
        vidaAtual = vidaInicial + 5;
        Barravida.value = AtualizaMostrador();
        sprite = GetComponent<SpriteRenderer>();
        posInicial = transform.position.x;
        target = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        speed = Random.Range(-1f, 1f);

    }

    private void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        inverteMovimento();
        SeguePlayer();
        if (playerInRange && !ataque)
        {
            anim.Play("Metranca_Atacando");
            Fogo();
            if (speed > 0)
            {
                sprite.flipX = true;
            }
            else if (speed < 0)
                sprite.flipX = false;
        }

        else if (!playerInRange)
        {
            posInicial = transform.position.x;
        }

        AtualizaMostrador();
        Debug.Log(vidaAtual);
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

    private void Fogo()
    {
        balaPos = transform.position;

        if (speed > 0 && tempoAtaque <= 0)
        {
            balaPos += new Vector2(2.5f, -0.13f);
            Instantiate(balaInimigoDireita, balaPos, Quaternion.identity);
            tempoAtaque = cd;
            ataque = true;


        }
        else if (speed < 0 && tempoAtaque <= 0)
        {
            balaPos += new Vector2(-2.5f, -0.13f);
            Instantiate(balaInimigoEsquerda, balaPos, Quaternion.identity);
            tempoAtaque = cd;
            ataque = true;
        }
    }
    void LateUpdate()
    {
        if (ataque)
        {

            if (tempoAtaque > 0)
            {
                tempoAtaque -= Time.deltaTime;
            }
            else
                ataque = false;
        }
    }

    private void SeguePlayer()
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) < aggroRange && Mathf.Abs(transform.position.y - target.transform.position.y) < aggroRange)
        {
            if (target.transform.position.x < transform.position.x && Mathf.Abs(target.transform.position.x - transform.position.x) > 2)
            {
                // anim.Play("Gunner_Atirando");
                playerInRange = true;
                speed = -3f;
            }
            else if (target.transform.position.x > transform.position.x && Mathf.Abs(target.transform.position.x - transform.position.x) > 2)
            {
                //anim.Play("Gunner_Atirando");
                playerInRange = true;
                speed = 3f;
            }
            else
                speed = 0;
        }
        else
        {
            //anim.Play("Gunner_Walking");
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

    void inverteMovimento()
    {
        if (Mathf.Abs(posInicial - transform.position.x) > patrulha)
        {
            if (speed != 0)
                speed *= -1;
        }
    }

    float AtualizaMostrador()
    {
        return vidaAtual / vidaInicial;
    }

    //Instancia o prefab de morte e destroi o objeto morto
    void Morre()
    {
        colisor.enabled = false;
        vidaAtual = 0;
        DropaItem();
        Destroy(gameObject);
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
}
