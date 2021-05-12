using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boleta : MonoBehaviour
{

    public float velInicial = 300f;
    private Rigidbody2D rigidBola;
    public GameObject respawnPos;
  




    void Start()
    {

        rigidBola = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D hit)
    {

        if (hit.gameObject.CompareTag("Player"))
        {
            rigidBola.AddForce(new Vector3(velInicial, velInicial, 0));
            print("bateu");
        }


        if (hit.gameObject.CompareTag("Respawn"))

        {

            transform.position = respawnPos.transform.position;
            print("morreu");
            rigidBola.velocity = Vector3.zero;
        }

      

    }


    


}
