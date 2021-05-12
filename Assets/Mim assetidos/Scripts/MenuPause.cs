using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour {

    public GameObject Ui;
    Scene cenaAtual;
    string nomeCena;

    private void Start()
    {
        Ui.SetActive(false);
        cenaAtual = SceneManager.GetActiveScene();
        nomeCena = cenaAtual.name;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !main.PAUSE)
        {
            main.PAUSE = true;
        }

       else  if (Input.GetKeyDown(KeyCode.Escape) && main.PAUSE)
        {
            main.PAUSE = false;
        }

        if (main.PAUSE)
        {
            Ui.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            Resume();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Ui.SetActive(false);
        main.PAUSE = false;

    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync(cenaAtual);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Ui.SetActive(false);
    }
}
