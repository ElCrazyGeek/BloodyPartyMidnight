using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar o salir al menú

public class Pausa : MonoBehaviour
{
    public GameObject PausaMenu;
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        Time.timeScale = 0;        
        PausaMenu.SetActive(true); 
        juegoPausado = true;
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1;       
        PausaMenu.SetActive(false);
        juegoPausado = false;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); 
    }
}