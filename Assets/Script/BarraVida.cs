using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarraVida : MonoBehaviour
{
    public Image Barra_vida;
    private Personaje personaje;
    public float vidaMaxima;
    void Start()
    {
       personaje = GameObject.Find("Personaje").GetComponent<Personaje>(); 
       vidaMaxima = personaje.vida;
    }

    void Update()
    {
      Barra_vida.fillAmount = personaje.vida/vidaMaxima;

    }
}
