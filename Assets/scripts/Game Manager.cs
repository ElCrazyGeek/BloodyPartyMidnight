using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Estancia estática para que otros scripts puedan acceder a él fácilmente
    public static GameManager instance;

    void Awake()
    {
        // Configuración de Singleton (solo puede haber un GameManager)
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // El Game Manager siempre escucha la R
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarNivel();
        }

    }

    public void ReiniciarNivel()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Esta función la llamarás desde el script de vida del jugador cuando muera
    public void JugadorMurio()
    {
        Debug.Log("El jugador ha muerto. Presiona R para reintentar.");
        // Aquí podrías activar un texto en pantalla de "HAS MUERTO"
    }
}