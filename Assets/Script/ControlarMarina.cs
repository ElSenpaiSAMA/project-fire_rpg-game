using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlarMarina : MonoBehaviour
{
    [SerializeField] private float velocidad;
    public Transform personaje;
    private NavMeshAgent agente;
    [SerializeField] private int ataque;
    private bool objetivoDetec;
    private bool personajeMuerto = false;

    private Animator anim;

    [SerializeField] private int vida = 100;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

    }

    private void Start()
    {
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }

    private void Update()
    {
        if (personajeMuerto) return;

        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        float distancia = Vector3.Distance(personaje.position, this.transform.position);

        float horizontal = transform.position.x;
        float vertical = transform.position.y;

        

        if (distancia <= 4)
        {
            objetivoDetec = true;
        }

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("velocidad", velocidad);

        MovimientoEnemigo(objetivoDetec);
    }

    void MovimientoEnemigo(bool esDetectado)
    {
        if (esDetectado)
        {
            agente.SetDestination(personaje.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Personaje personajeScript = collision.gameObject.GetComponent<Personaje>();

            if (personajeScript != null)
            {
                Vector2 direccionDanio = (personaje.position - this.transform.position).normalized;
                personajeScript.recibiendoDanio(direccionDanio, ataque); 
            }
        }
    }

    public void TomarDanio(int danio)
    {
        vida -= danio; 

        if (vida <= 0)
        {
            DestruirEnemigo(); 
        }
    }

    private void DestruirEnemigo()
    {
        Destroy(gameObject); 
    }

    public void DetenerPersecucion()
    {
        personajeMuerto = true; 
        agente.ResetPath(); 
    }
}
