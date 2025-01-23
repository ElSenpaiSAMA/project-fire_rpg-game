using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    private BoxCollider2D ColEspada;
    private BoxCollider2D colEspadaD;
    private BoxCollider2D colEspadaIz;
    private BoxCollider2D colEspadaArri;

    private int danio;  
    private Personaje personajeScript; 

    public float fuerzaEmpuje = 30f; 
    public float tiempoEntreAtaques = 0.002f; 
    private bool puedeAtacar = true; 

    private void Awake()
    {
        ColEspada = GetComponent<BoxCollider2D>();
        colEspadaD = GetComponent<BoxCollider2D>();
        colEspadaIz = GetComponent<BoxCollider2D>();
        colEspadaArri = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        personajeScript = GameObject.FindWithTag("Player").GetComponent<Personaje>();

        if (personajeScript != null)
        {
            danio = personajeScript.danio;
        }
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (puedeAtacar)  
        {
            if (otro.CompareTag("Goblin") || otro.CompareTag("slime") || otro.CompareTag("esqueleto")|| otro.CompareTag("marina"))
            {
                EnemyController enemyController = otro.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.TomarDanio(danio);

                    EmpujarEnemigo(otro);

                    StartCoroutine(CooldownAtaque());
                }
            }
        }
    }

    private void EmpujarEnemigo(Collider2D enemigo)
    {
        Vector2 direccionEmpuje = (enemigo.transform.position - transform.position).normalized;

        enemigo.transform.position = new Vector3(
            enemigo.transform.position.x + direccionEmpuje.x * fuerzaEmpuje,
            enemigo.transform.position.y + direccionEmpuje.y * fuerzaEmpuje,
            enemigo.transform.position.z
        );
    }

    private IEnumerator CooldownAtaque()
    {
        puedeAtacar = false;  
        yield return new WaitForSeconds(tiempoEntreAtaques);  
        puedeAtacar = true;  
    }
}
