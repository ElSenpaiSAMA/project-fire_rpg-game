using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private bool recibeDanio;
    public float frebote = 10f;
    public int vida = 500;
    [SerializeField] public int danio;
    private Rigidbody2D rig;
    private Animator anim;
    private Vector2 moveInput;

    private bool estaMuerto = false;  
    private bool puedeAtacar = true;  

    public float tiempoEntreAtaques = 0.002f;  

    [SerializeField] private GameObject panelMuerte;       
    [SerializeField] private GameObject panelBackground;   
    [SerializeField] private GameObject panelPausa;       
    public delegate void CambiarVida(int vidaActual);
    public static event CambiarVida OnCambioVida;

    public delegate void MuerteDelPersonaje(); 
    public static event MuerteDelPersonaje OnMuerte;  

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        if (panelMuerte != null)
        {
            panelMuerte.SetActive(false);
        }
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (!estaMuerto)  
        {
            movimiento();
        }
    }

    void Update()
    {
        if (estaMuerto) return;  

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontal, vertical).normalized;

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("velocidad", moveInput.sqrMagnitude);

        if (Input.GetMouseButtonDown(0) && puedeAtacar)
        {
            anim.SetTrigger("Ataca");
            StartCoroutine(CooldownAtaque());  
        }

        anim.SetBool("recibeDanio", recibeDanio);
    }

    public void botonPausarJuego()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausarJuego();
        }
    }



    private IEnumerator CooldownAtaque()
    {
        puedeAtacar = false;  
        yield return new WaitForSeconds(tiempoEntreAtaques);  
        puedeAtacar = true;  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, transform.position.y);
            collision.gameObject.GetComponent<Personaje>().recibiendoDanio(direccionDanio, 1);
        }
    }

    public void recibiendoDanio(Vector2 direccion, int cantDanio)
    {
        if (!recibeDanio && !estaMuerto)
        {
            recibeDanio = true;

            Vector3 rebote = new Vector2(transform.position.x - direccion.x, transform.position.y - direccion.y).normalized;
            rig.AddForce(rebote * frebote, ForceMode2D.Impulse);

            vida -= cantDanio;
            Debug.Log("Vida actual: " + vida);

            OnCambioVida?.Invoke(vida);

            if (vida <= 0)
            {
                Muerte();
            }
            else
            {
                StartCoroutine(DesactivarRecibeDanio());
            }
        }
    }

    private IEnumerator DesactivarRecibeDanio()
    {
        yield return new WaitForSeconds(0.5f);
        recibeDanio = false;
        anim.SetBool("recibeDano", false);
    }

    private void movimiento()
    {
        rig.MovePosition(rig.position + moveInput * velocidad * Time.fixedDeltaTime);
    }

    private void Muerte()
    {

        estaMuerto = true; 
        Debug.Log("El personaje ha muerto");
        anim.SetBool("muerto", true); 

        OnMuerte?.Invoke();  

        rig.velocity = Vector2.zero;
        moveInput = Vector2.zero;

        if (panelMuerte != null)
        {
            panelMuerte.SetActive(true);
        }

        if (panelBackground != null)
        {
            panelBackground.SetActive(false);
        }
    }
    public void PausarJuego()
    {
        if (panelBackground != null)
        {
            panelBackground.SetActive(false);
        }

        if (panelPausa != null)
        {
            panelPausa.SetActive(true);
        }

        Time.timeScale = 0f;  
        Debug.Log("Juego pausado.");
    }
}
