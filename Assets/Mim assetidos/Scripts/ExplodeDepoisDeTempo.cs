using UnityEngine;

public class ExplodeDepoisDeTempo : MonoBehaviour {
    public GameObject prefabExplosao; //prefab que será instanciado após a explosao
    public float tempoParaExplodir = 3; //tempo até a explosão acontecer
    public float raioDeAlcance = 3;
    public LayerMask layersAfetadas;
    public int danoParaAplicar = 1;

    void Start ()
    {
        //invocamos a função Explode após o tempo definido na variavel tempoParaExplodir
        Invoke("Explode", tempoParaExplodir);
    }

    void Explode ()
    {
        //Instanciaos a explosão no mesmo local e orientação que o objeto que está explodindo
        Instantiate(prefabExplosao, transform.position, transform.rotation);

        Collider[] collidersProximos = Physics.OverlapSphere(transform.position, raioDeAlcance, layersAfetadas);
        Vida vidaQuemColidiu;
        
        for(int i = 0; i < collidersProximos.Length; i++)
        {
            vidaQuemColidiu = collidersProximos[i].GetComponent<Vida>();
            if(vidaQuemColidiu)
                vidaQuemColidiu.TomaDano(danoParaAplicar);
        }

        //destruimos o objeto que extá explodindo
        Destroy(gameObject);
    }
}
