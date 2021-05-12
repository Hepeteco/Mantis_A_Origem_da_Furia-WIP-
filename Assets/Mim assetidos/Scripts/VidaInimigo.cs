using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class VidaInimigo : MonoBehaviour {
    public RectTransform barraVida; //mostrador de mida
    public float vidaAtual; //quantidade de vida atual do objeto
    public float vidaInicial = 5; //quanto de vida o objeto tem no inicio
    public float[] dropRatio;
    public List<GameObject> prefabMorte; //objeto a ser isntanciado no momento da morte
    private Animator anim;
    public GameObject cristal;
    public AudioSource som;

    //inicializa a vida atual
    void Start()
    {
        som = GetComponent<AudioSource>();
        vidaAtual = vidaInicial;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        AtualizaMostrador();
    }
    /// <summary>
    /// Função chamada para aplicar dano ao objeto. Deve passar a quantidade de dano a ser aplicada
    /// </summary>
    /// <param name="danoParaAplicar></param>
    public void TomaDano(float danoParaAplicar)
    {
        //diminui a quantidade de vida
        vidaAtual -= danoParaAplicar;
        //verifica se a vida chegou a zero e chama a função de morte em caso positivo
        if (vidaAtual <= 0)
        {
            Morre();
        }
        //do contrário atualiza o mostrador de vida
        else
            AtualizaMostrador();
            
        barraVida.sizeDelta = new Vector2(vidaAtual, barraVida.sizeDelta.y);
    }


    float AtualizaMostrador()
    {
        return vidaInicial / vidaAtual;
    }

    //Instancia o prefab de morte e destroi o objeto morto
    void Morre()
    {
        som.Play();
        anim.SetBool("Death", true);
        vidaAtual = 0;
        DropaItem();
        Destroy(gameObject, 5f);
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
