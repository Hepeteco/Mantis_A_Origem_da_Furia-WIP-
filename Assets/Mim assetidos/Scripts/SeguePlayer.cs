using UnityEngine;
using UnityEngine.AI;

public class SeguePlayer : MonoBehaviour
{
    public NavMeshAgent agente; //agente responsavel por receber um destino e navegar pelo cenário até ele
    Transform trPlayer; //Componente Transform do Player. usao para o agente saber onde o Player está

    // Use this for initialization
    void Start ()
    {
        //procuramos pelo Player na cena e pegamos seu Transform
        trPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //Durante todo frame, atualiza o destino para corresponder à posição do Player
    void Update ()
    {
        agente.SetDestination(trPlayer.position);
    }
}
