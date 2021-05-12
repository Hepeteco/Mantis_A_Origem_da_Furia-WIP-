using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour {

    public float velX;
    public float velY;


    private void FixedUpdate()
    {
        transform.Translate(velX, velY, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Limite"))
        {
            if (velX != 0)
                velX *= -1;
            else if (velY != 0)
                velY *= -1;

        }
    }

    void SetSpeed(Vector2 speed)
    {
        transform.Translate(speed);
        velX = speed.x;
        velY = speed.y;
        Debug.Log(velY);
    }
}
