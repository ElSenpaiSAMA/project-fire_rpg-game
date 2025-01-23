using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float velocidad;
    public Transform personaje;
    private NavMeshAgent agente;
    [SerializeField] private int ataque;
    private bool objetivoDetec;
    private bool personajeMuerto = false;

    [SerializeField] public int vida = 100;

    [SerializeField] private GameObject panelVictoria;
    [SerializeField] private GameObject panelBackground;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
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

        if (distancia <= 4)
        {
            objetivoDetec = true;
        }

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
        if (gameObject.CompareTag("marina"))
        {
            Destroy(gameObject);
            panelBackground.SetActive(false);
            panelVictoria.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DetenerPersecucion()
    {
        personajeMuerto = true;
        agente.ResetPath();
    }
}
