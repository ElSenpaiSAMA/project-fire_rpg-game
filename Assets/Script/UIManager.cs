using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton

    [SerializeField] private TMP_Text textoMonedas;
    [SerializeField] private TMP_Text textoVidaActual;  // Referencia al texto de vida
    public int totalMonedas;
    private int totalVidaActual;

    private void Awake()
    {
        // Implementación Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Para mantener este objeto entre escenas
        }
        else
        {
            Destroy(gameObject);  // Destruir cualquier duplicado
        }
    }

    private void Start()
    {
        Moneda.sumarMoneda += SumarMonedas;
        Personaje.OnCambioVida += ActualizarVida;  // Escuchar el evento de cambio de vida
    }

    public void SumarMonedas(int moneda)
    {  
        totalMonedas += moneda;
        textoMonedas.text = totalMonedas.ToString();
    }

    public int GetTotalMonedas()
    {
        return totalMonedas;
    }

    public void SetTotalMonedas(int monedas)
    {
        totalMonedas = monedas;
        textoMonedas.text = totalMonedas.ToString();
    }

    private void ActualizarVida(int vidaActual)
    {
        totalVidaActual = vidaActual;
        textoVidaActual.text = totalVidaActual.ToString();  // Actualizar el texto con la vida actual
    }
}
