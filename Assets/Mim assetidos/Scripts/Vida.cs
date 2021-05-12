using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Vida : MonoBehaviour {
    public float vidaAtual;
    public GameObject gameover;
    private bool morreu = false;

    public Slider Barravida;

    void Start()
    {
        vidaAtual = main.VIDA;
        Barravida.value = AtualizaMostrador();
        gameover.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("GodMode"))
        {
            if(!main.GOD_MODE)
            {
                main.GOD_MODE = true;
            }           
            else
            main.GOD_MODE = false;
        }

        if (main.MORREU && main.ACAO)
        {
            gameover.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            main.MORREU = false;
            main.ACAO = false;
        }

        if(main.GOD_MODE)
        {
            vidaAtual = main.VIDA;
        }
    }

    void HealthRegen(float hpup)
    {
        vidaAtual += hpup;
        Barravida.value = AtualizaMostrador();
        if (vidaAtual > 100 && !main.GOD_MODE)
            vidaAtual = main.VIDA;
    }

    public void TomaDano(float danoParaAplicar)
    {
        vidaAtual -= danoParaAplicar;
        Barravida.value = AtualizaMostrador();
        if (vidaAtual <= 0)
        {
            Morre();
        }
    }


    float AtualizaMostrador()
    {
        return vidaAtual / main.VIDA;
    }

    void Morre()
    {
        main.MORREU = true;
        vidaAtual = 0;
        gameover.SetActive(true);
    }
}
