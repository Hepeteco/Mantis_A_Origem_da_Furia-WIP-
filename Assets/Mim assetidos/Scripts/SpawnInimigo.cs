using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigo : MonoBehaviour {
    public GameObject laser;
    public GameObject porta;
    public GameObject prefabGunner;
    public GameObject prefabMantis;
    public float spawnTimecd;
    protected float spawnTime = 0;
    protected bool invoca = false;
    private int invocaCount = 0;
    private bool finalWave1 = false;
    private bool finalWave2 = false;


    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;
    private float cdWavePic = 2;
    private float WavePic = 0;
    public Animator anim;

    public GameObject setaindicadoraFim;

    public float SpawnTimeMantisWave1;
    public float SpawnTimeMantisWave2;
    public float SpawnTimeMantisWave3;
    public float SpawnTimeGunnerWave1;
    public float SpawnTimeGunnerWave2;
    public float SpawnTimeGunnerWave3;

    public AudioSource som;


    private void Start()
    {
        anim = porta.GetComponent<Animator>();
    }
    void Awake()
    {

        wave1.SetActive(false);
        wave2.SetActive(false);
        wave3.SetActive(false);
        setaindicadoraFim.SetActive(false);

    }


    private void LateUpdate()
    {
        if (spawnTime > 0)
            spawnTime -= Time.deltaTime;

        if (spawnTime <= 0 && invocaCount == 1)
        {
            CancelWave1();
            finalWave1 = true;
        }
        else if (spawnTime <= 0 && invocaCount == 2)
        {
            CancelWave2();
            finalWave2 = true;
        }
        else if (spawnTime <= 0 && invocaCount == 3)
        {
            CancelWave3();
            som.Play();
            anim.Play("PortaFechando");
            setaindicadoraFim.SetActive(true);
            laser.SetActive(false);
        }

        if (invoca && invocaCount == 0)
        {
            som.Play();
            anim.Play("PortaAbrindo");
            spawnTime = spawnTimecd;
            Invoke("Wave1", 0);
            invocaCount++;
        }
        else if (invoca && invocaCount == 1 && finalWave1)
        {
            som.Stop();
            spawnTime = spawnTimecd;
            Invoke("Wave2", 0);
            invocaCount++;
        }
        else if (invoca && invocaCount == 2 && finalWave2)
        {
            spawnTime = spawnTimecd;
            Invoke("Wave3", 0);
            invocaCount++;
            invoca = false;
        }

        if (WavePic > 0)
        {
            WavePic -= Time.deltaTime;
            if (WavePic - Time.deltaTime <= 0.5f)
            {
                wave1.SetActive(false);
                wave2.SetActive(false);
                wave3.SetActive(false);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            invoca = true;

    }

    private void SpawnGunner()
    {
        Instantiate(prefabGunner, porta.transform.position, Quaternion.identity);
    }

    private void SpawnMantis()
    {
        Instantiate(prefabMantis, porta.transform.position, Quaternion.identity);
    }

    void Wave1()
    {
        WavePic = cdWavePic;
        wave1.SetActive(true);
        Debug.Log("Wave1");
        InvokeRepeating("SpawnMantis", 0, SpawnTimeMantisWave1); ;
        Invoke("SpawnGunner", SpawnTimeGunnerWave1);
    }

    void CancelWave1()
    {
        CancelInvoke("SpawnGunner");
        CancelInvoke("SpawnMantis");
    }

    void Wave2()
    {
        WavePic = cdWavePic;
        wave2.SetActive(true);
        Debug.Log("Wave2");
        InvokeRepeating("SpawnGunner", 2, SpawnTimeGunnerWave2); ;
        InvokeRepeating("SpawnMantis", 2, SpawnTimeMantisWave2);
    }

    void CancelWave2()
    {
        CancelInvoke("SpawnGunner");
        CancelInvoke("SpawnMantis");
    }

    void Wave3()
    {
        WavePic = cdWavePic;
        wave3.SetActive(true);
        Debug.Log("Wave3");
        InvokeRepeating("SpawnGunner", 2, SpawnTimeGunnerWave3); ;
        InvokeRepeating("SpawnMantis", 2, SpawnTimeMantisWave3);
    }

    void CancelWave3()
    {
        CancelInvoke("SpawnGunner");
        CancelInvoke("SpawnMantis");
    }
}

