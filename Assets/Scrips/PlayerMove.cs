using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CapsuleCollider2D colider;
    Rigidbody2D rb2D;
    SpriteRenderer sprite;
    public float speed = 2.5f;
    public float fuerza = 2.5f;
    GroundDetector_Raycast ground;
    public GameObject Pause;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public bool SecretoActivado;
    bool pausaActiva;
    bool muerto;
    public bool TextoMuerte;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        colider = GetComponent<CapsuleCollider2D>();
        SecretoActivado = false;
        pausaActiva = false;
        TextoMuerte = false;
        rb2D = GetComponent<Rigidbody2D>();
        ground = GetComponent<GroundDetector_Raycast>();
    }

    // Update is called once per frame
    void Update()
    {
        if (muerto == true)
        {

        }
        else
        {
            //movimiento horizonal
            float horizontal = Input.GetAxis("Horizontal");
            transform.position = transform.position + new Vector3(horizontal * Time.deltaTime * speed, 0);

            //salto 
            if (Input.GetButtonDown("Jump") && ground.grounded == true)
            {

                rb2D.AddForce(new Vector2(0, fuerza));
            }


            //animacion
            if (Input.GetKey("a"))
            {
                spriteRenderer.flipX = true;

                if (ground.grounded == false)
                {
                    animator.SetBool("Jump", true);
                    animator.SetBool("Run", false);
                }
                else if (ground.grounded == true)
                {
                    animator.SetBool("Jump", false);
                    animator.SetBool("Run", true);
                }
            }
            else if (Input.GetKey("d"))
            {
                spriteRenderer.flipX = false;

                if (ground.grounded == false)
                {
                    animator.SetBool("Jump", true);
                    animator.SetBool("Run", false);
                }
                else if (ground.grounded == true)
                {
                    animator.SetBool("Jump", false);
                    animator.SetBool("Run", true);
                }
            }
            else
            {
                if (ground.grounded == false)
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Jump", true);

                }
                else if (ground.grounded == true)
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Jump", false);
                }

            }

            //pausa
            if(pausaActiva == false)
            {
                if (Input.GetKeyDown("q"))
                {
                    pausaActiva = true;
                    Pause.SetActive(true);
                }
            }
            else
            {
                if (Input.GetKeyDown("q"))
                {
                    pausaActiva = false;
                    Pause.SetActive(false);
                }
            }

        }
        
    }

    //Para activar el desbloqueable del mapa 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LaPiña"))
        {
            SecretoActivado = true;
        }
    }

    //funcion de muerte del personaje
    public void Muerte()
    {
        if (muerto == true)
        {

        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

            if (GameManager.instance.vidas <= 0)
            {
                Destroy(gameObject, 0.5f);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GameManager.instance.vidas -= 1;
                TextoMuerte = true;
            }
            else
            {
                StartCoroutine(Respawn_Corrutina());
            }
        }
    }



    //Para conservar el movimiento dentro del elevador o plataforma movil
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Elevator")
        {
            transform.parent = collision.transform;
        }
    }
    //Para salir del elevador oplataforma movil sin conservar el movimiento
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Elevator")
        {
            transform.parent = null;
        }
    }

    IEnumerator Respawn_Corrutina()
    {
        muerto = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        sprite.enabled = false;
        yield return new WaitForSeconds(3);

        GameManager.instance.vidas -= 1;
        transform.position = new Vector3(-2.014f, 0.626f, 0);
        rb2D.velocity = new Vector2(0, 0);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        sprite.enabled = true;
        muerto = false;
    }
}
