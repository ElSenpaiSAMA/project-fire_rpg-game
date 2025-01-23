using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatosJuego
{
    public Vector3 posicion;
    public int vida;
    public List<DatosMoneda> monedas;
    public List<DatosEnemigo> enemigos;
    public Vector3[] posicionesMonedas;
}

[System.Serializable]
public class DatosMoneda
{
    public string nombre;
    public Vector3 posicion;
    public bool estaActiva;
}

[System.Serializable]
public class DatosEnemigo
{
    public string nombre;
    public Vector3 posicion;
    public int vida;
    public bool estaActivo;
}
