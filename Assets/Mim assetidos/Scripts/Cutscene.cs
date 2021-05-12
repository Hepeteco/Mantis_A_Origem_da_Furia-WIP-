using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour {

    public GameObject Menu;
    public GameObject CutsceneUi;
    public GameObject imagem0;
    public GameObject imagem1;
    public GameObject imagem2;

    private int cont;

    private void Start()
    {
        CutsceneUi.SetActive(false);
        imagem0.SetActive(false);
        imagem1.SetActive(false);
        imagem2.SetActive(false);
    }

    private void Update()
    {
        if (cont == 1)
        {
            imagem0.SetActive(false);
            imagem1.SetActive(true);
        }
        if (cont == 2)
        {
            imagem1.SetActive(false);
            imagem2.SetActive(true);
        }
        if (cont == 3)
        {
            imagem2.SetActive(false);
            SceneManager.LoadScene("Fase1", LoadSceneMode.Single);
        }
    }

    public void StartGame()
    {
        Menu.SetActive(false);
        CutsceneUi.SetActive(true);
        imagem0.SetActive(true);
    }

    public void ProxCut()
    {
        cont++;
    }
}
