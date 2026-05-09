using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menuprincipal : MonoBehaviour
{

    [Header("Botones de Navegacion")]
    public Button Izquierda;
    public Button Derecha;

    [Header("Boton Activo")]

    public Button Activo;  
    public TextMeshProUGUI TextoActivo;      
    
    [Header("Opciones")]

    public string [] opciones = {"INICIAR PARTIDA", "CARGAR PARTIDA", "SELECTOR DE NIVELES", "Galeria", "Opciones", "Salir del Juego" };
    public int indiceActual = 0;

    void Start()
    {
        Izquierda.onClick.AddListener(Anterior);
        Derecha.onClick.AddListener(Siguiente);
        ActualizarTexto();
    }

    void Siguiente(){
        indiceActual = (indiceActual + 1 ) % opciones.Length;
        ActualizarTexto();
    }

    void Anterior()
    {
        indiceActual =(indiceActual - 1 + opciones.Length) % opciones.Length;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        TextoActivo.text = opciones[indiceActual];
    }
}
