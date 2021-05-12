using UnityEngine;

public class Projetil : MonoBehaviour {
    public float velocidadeDaBala = 1000; //forca com que a bala será disparada
    

	void Start ()
    {
        //acessa o rigidbody da bala e aplica uma força em direção à frente da bala com intensidade igual à forcaDaBala
        GetComponent<Rigidbody>().AddForce(transform.forward * velocidadeDaBala);
	}
}
