using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoomBot : MonoBehaviour {
    public int dano;
    public Collider2D area;
    bool playerInRange;
    private bool m_FacingRight = false;
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

    private int explosionCount = 0;

    public Slider Barravida;

    private Collider2D colisor;

    void Start()
    {
        colisor = GetComponent<BoxCollider2D>();
        vidaAtual = vidaInicial;
        Barravida.value = AtualizaMostrador();
        som = GetComponent<AudioSource>();
        area.enabled = false;
        sprite = GetComponent<SpriteRenderer>();
        posInicial = transform.position.x;
        target = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        speed = 0;

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

        if (Mathf.Abs(target.transform.position.x - transform.position.x) > 1)
        {
            area.enabled = true;  
        }
            AtualizaMostrador();
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
                speed = -4f;
            }

            else
            {
                speed = 0;
            }
        }
        else
        {
            playerInRange = false;
            speed = 0;
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
        return vidaAtual / vidaInicial;
    }

    //Instancia o prefab de morte e destroi o objeto morto
    void Morre()
    {
        colisor.enabled = false;
        som.Play();
        anim.Play("BoomBot_Death");
        vidaAtual = 0;
        DropaItem();
        Destroy(gameObject, 1);
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
        else if(col.gameObject.tag=="Player")
        {
            if(explosionCount <= 0)
            {
                target.SendMessageUpwards("TomaDano", dano);
                gameObject.SendMessageUpwards("Morre");
                explosionCount++;
            }            
        }
    }
}
