using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject panelBackground;   
    [SerializeField] private GameObject panelPausa;  
     private ControladorDatosJuego controladorDatosJuego;
    public void menuPrincipal(string nombre){
    Time.timeScale = 1f;     
    SceneManager.LoadScene(nombre);
    
   }

   private void Start()
    {
        controladorDatosJuego = FindObjectOfType<ControladorDatosJuego>();
    }

   public void Salir(){
    UnityEditor.EditorApplication.isPlaying = false;
    Application.Quit();
   }

    public void ReanudarJuego()
    {
        if (panelBackground != null)
        {
            panelBackground.SetActive(true);
        }

        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }

        Time.timeScale = 1f;  
    }

    public void GuardarDatos()
    {
        if (controladorDatosJuego != null)
        {
            controladorDatosJuego.GuardarDatos();  
        }
        else
        {
            Debug.LogError("ControladorDatosJuego no encontrado.");
        }
    }

    public void CargarDatos()
    {
        if (controladorDatosJuego != null)
        {
            controladorDatosJuego.CargarDatos();  
        }
        else
        {
            Debug.LogError("Error al cargar Datos");
        }
    }
}
