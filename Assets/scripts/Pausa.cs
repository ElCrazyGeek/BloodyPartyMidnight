using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Pausa : MonoBehaviour
{
    public GameObject PausaMenu;
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    public void PausarJuego()
    {
        Time.timeScale = 0;
        PausaMenu.SetActive(true);
        juegoPausado = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1;
        PausaMenu.SetActive(false);
        juegoPausado = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReiniciarJuego()
{
    DOTween.KillAll();
    Time.timeScale = 1;
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

   public void SalirAlMenu()
{
    DOTween.KillAll(); // mata todos los tweens activos
    Time.timeScale = 1;
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;
    SceneManager.LoadScene("Pantalla de inicio");
}
}