using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject Ui;
    public GameObject Controles;

	// Use this for initialization
	void Start () {
        Ui.SetActive(true);
        Controles.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && main.CONTROLES && !main.PAUSE)
            FechaControls();

        if(!main.CONTROLES)
        {
            Controles.SetActive(false);
        }
	}

    public void Controls()
    {
        main.CONTROLES = true;
        Controles.SetActive(true);
    }

    public void FechaControls()
    {
        Controles.SetActive(false);
    }

    public void CloseJogo()
    {
        Application.Quit();
    }
}
