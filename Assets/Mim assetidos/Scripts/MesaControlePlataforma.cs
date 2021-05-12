using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaControlePlataforma : MonoBehaviour {
    public GameObject laser;
    public GameObject porta;
    public GameObject prefabInimigo;
    public float spawnTime;
    private bool acao = false;
    public GameObject plataforma;
    public Vector2 speedPlataforma;

	void Update () {
        if (Input.GetButtonDown("Ação"))
        {
            Destroy(laser);
            acao = true;
            if (spawnTime > 0)
            {
                InvokeRepeating("Spawn", 0, 0.7f);
            }
        }

        if(acao)
        {
            plataforma.SendMessageUpwards("SetSpeed", speedPlataforma);
            if (spawnTime > 0)
                spawnTime -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (spawnTime <= 0)
        {
            CancelInvoke("Spawn");
        }
    }

    private void Spawn()
    {
        Instantiate(prefabInimigo, porta.transform.position, Quaternion.identity);
    }
}
