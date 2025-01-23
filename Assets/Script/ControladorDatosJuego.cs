using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public GameObject monedaPrefab;  // Prefab de la moneda
    public Transform[] posicionesMonedas;  // Las posiciones donde deben aparecer las monedas

    public DatosJuego datosJuego = new DatosJuego();
    public List<Vector3> posicionesMonedasRestantes = new List<Vector3>();

    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";
        jugador = GameObject.FindGameObjectWithTag("Player");
        
        // Encuentra las posiciones de las monedas que quedan
        posicionesMonedasRestantes.Clear();  // Limpiar la lista de monedas
        foreach (Transform pos in posicionesMonedas)
        {
            posicionesMonedasRestantes.Add(pos.position);  // Guardar las posiciones de las monedas en el mapa
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))  // Cargar los datos
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.G))  // Guardar los datos
        {
            GuardarDatos();
        }
    }

    public void CargarDatos()
    {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

            jugador.transform.position = datosJuego.posicion;
            jugador.GetComponent<Personaje>().vida = datosJuego.vida;

            // Re-crea las monedas restantes en el mapa
            foreach (Vector3 pos in datosJuego.posicionesMonedas)
            {
                Instantiate(monedaPrefab, pos, Quaternion.identity);
            }

            // Cargar enemigos si es necesario
            // Aquí deberías hacer lo mismo con los enemigos
        }
        else
        {
            Debug.Log("El archivo de guardado no existe.");
        }
    }

    public void GuardarDatos()
    {
        DatosJuego nuevosDatos = new DatosJuego()
        {
            posicion = jugador.transform.position,
            vida = jugador.GetComponent<Personaje>().vida,
            posicionesMonedas = posicionesMonedasRestantes.ToArray(),
        };

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);
        Debug.Log("Datos guardados correctamente.");
    }
}
