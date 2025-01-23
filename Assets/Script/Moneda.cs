using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public delegate void SumaMoneda(int moneda);
    public static event SumaMoneda sumarMoneda;

    [SerializeField] public int cantidadMonedas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (sumarMoneda != null)
            {
                SumarMoneda();
                Destroy(this.gameObject);  // Destruir la moneda una vez que ha sido recogida
            }
        }
    }

    private void SumarMoneda()
    {
        sumarMoneda(cantidadMonedas);
    }
}
