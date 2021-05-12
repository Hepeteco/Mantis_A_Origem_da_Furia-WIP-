using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevador : MonoBehaviour {

    public GameObject seta;

    void Start()
    {
        seta.SetActive(false);
    }

    void Update()
    {
        if (main.BOSS_MORTO)
            seta.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && main.BOSS_MORTO)
        {
            SceneManager.UnloadSceneAsync(main.SCENE_NAME);
            SceneManager.LoadScene("Fase2", LoadSceneMode.Single);
        }
            
    }
}
