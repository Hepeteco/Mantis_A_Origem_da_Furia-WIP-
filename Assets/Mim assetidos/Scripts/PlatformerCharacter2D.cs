using System;
using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;

    const float k_GroundedRadius = 0.2143982f; // Radius of the overlap circle to determine if grounded
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    public GameObject balaDireita;
    public GameObject balaEsquerda;
    Vector2 balaPos;

    public GameObject VidaBoss;

    private bool ataque = false;
    private float tempoAtaque = 0;
    public float cdAtaque;

    public AudioSource som;

    public GameObject boss;

    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        som = GetComponent<AudioSource>();
        VidaBoss.SetActive(false);
    }

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();        
        Physics2D.IgnoreLayerCollision(10, 9, true);   
    }

    void Update()
    {
           
        if (Input.GetKeyDown(KeyCode.X) && main.ARMA && !ataque)
        {
            m_Anim.Play("Spencer_Tiro");
            ataque = true;
            tempoAtaque = cdAtaque;
            som.Play();
            balaPos = transform.position;
            if (m_FacingRight)
            {
                balaPos += new Vector2(1f, -0.33f);
                Instantiate(balaDireita, balaPos, Quaternion.identity);
            }
            else
            {
                balaPos += new Vector2(-1f, -0.33f);
                Instantiate(balaEsquerda, balaPos, Quaternion.identity);
            }
        }
        if (transform.position.y <= boss.transform.position.y + 5)
            VidaBoss.SetActive(true);

        m_Anim.SetBool("Ground", main.CHAO);
    }

     void LateUpdate()
    {
        if(ataque)
        {
            if (tempoAtaque > 0)
                tempoAtaque -= Time.deltaTime;

            else
            {
                m_Anim.Play("Walk");
                ataque = false;
            }
        }
        
    }

    private void Fogo()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataforma")
        {
            main.CHAO = true;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlataformasMovH"))
        {
            main.CHAO = true;
            transform.parent = collision.transform;
        }

        else if (collision.gameObject.CompareTag("PlataformasMovV"))
        {
            main.CHAO = true;
            transform.parent = collision.transform;
        }

        else if (collision.gameObject.tag == "Plataforma")
        {
            main.CHAO = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlataformasMovH"))
        {
            main.CHAO = false;
            transform.parent = null;
        }
        else if (collision.gameObject.CompareTag("PlataformasMovV"))
        {
            main.CHAO = false;
            transform.parent = null;
        }
        else if (collision.gameObject.tag == "Plataforma")
        {
            main.CHAO = false;
        }
    }

    public void Move(float move)
    {
        //only control the player if grounded or airControl is turned on
        if (main.CHAO || m_AirControl)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            if (Mathf.Abs(move) > 0)
                m_Anim.SetBool("Walk", true);
            else
            {
                m_Anim.SetBool("Walk", false);
            }                
            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
                // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (main.PULO && main.CHAO)
        {
            // Add a vertical force to the player.
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            main.PULO = false;
            main.CHAO = false;
        }
    }


    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}

