using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joagor : MonoBehaviour
{

    public float velx;
    Vector2 bordaMin;
    Vector2 bordaMax; 
	void Start ()
    {

       // bordaMin = new Vector2(-4.2f, 0);
       // bordaMin = new Vector2(4.2f, 0);

    }
	
	// Update is called once per frame
	void Update ()
    {

        float x = transform.position.x;

        x += velx;

        Mover();

        ColisaoParedes();
	}



    void Mover()
    {

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(velx, 0, 0);
        }

       else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-velx, 0, 0);
        }
    }


    void ColisaoParedes()
    {

        float x = Mathf.Clamp(transform.position.x, -3.9244f, 4.0346f);
      
            transform.position = new Vector2(x, 0);   

    }




    
}
