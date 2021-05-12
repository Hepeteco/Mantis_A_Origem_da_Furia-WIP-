using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour {
    static public bool NAVE = false;
    static public bool CRAFT = false;
    static public bool CONTROLES = false;
    static public bool PAUSE = false;
    static public float VIDA = 100;
    static public int CRISTAL = 0;
    static public bool BOSS_MORTO = false;
    static public bool DRILL_SHOT = false;
    static public bool DRILL_PUNCH = false;
    static public bool ACAO = false;
    static public bool PULO = false;
    static public int PULO_CONTADOR = 0;
    static public bool CHAO = true;
    static public bool MORREU = false;
    static public bool MOVE = false;
    static public bool ARMA = false;
    static public bool GOD_MODE = false;
    static public string SCENE_NAME;
    Scene cena;

    void Update()
    {
        if (Input.GetButtonDown("Ação"))
            ACAO = true;
        else if (Input.GetButtonUp("Ação"))
            ACAO = false;
        cena = SceneManager.GetActiveScene();
        SCENE_NAME = cena.name;
        Debug.Log(SCENE_NAME);
        DontDestroyOnLoad(gameObject);
        
    }
}
