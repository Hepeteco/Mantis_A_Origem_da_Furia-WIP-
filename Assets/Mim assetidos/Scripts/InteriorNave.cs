using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorNave : MonoBehaviour {

    public GameObject Ui;
    public GameObject Popaye;
    public GameObject tela;
    private bool verificaContato = false;

	// Use this for initialization
	void Start () {
        Ui.SetActive(false);
        Popaye.SetActive(false);
        tela.SetActive(false);
	}

    public void EntraNave()
    {
        Ui.SetActive(true);
        main.NAVE = true;
    }

    public void SaiNave()
    {
        Ui.SetActive(false);
        main.NAVE = false;
    }

    public void Craft()
    {
        tela.SetActive(true);
        Popaye.SetActive(true);
        main.CRAFT = true;
    }
    public void FechaCraft()
    {
        tela.SetActive(false);
        Popaye.SetActive(false);
        main.CRAFT = false;
    }


    public void Update()
    {
        if(verificaContato && main.ACAO)
        {
            Debug.Log("foi");
            Ui.SetActive(true);
            main.NAVE = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            verificaContato = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            verificaContato = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            verificaContato = false;
        }
    }
}
