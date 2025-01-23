using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
   public void menuPrincipal(string nombre){
    SceneManager.LoadScene(nombre);
   }

   public void Salir(){
    UnityEditor.EditorApplication.isPlaying = false;
    Application.Quit();
   }
}
