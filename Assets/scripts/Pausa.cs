using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Pausa : MonoBehaviour
{
    public GameObject PausaMenu;
    public CanvasGroup canvasGroup;
    private bool juegoPausado = false;

    public RectTransform panelCentral; 

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

    // Mostrar y liberar el cursor
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;

    canvasGroup.alpha = 0f;
    canvasGroup.DOFade(1f, 0.3f).SetUpdate(true);

    panelCentral.anchoredPosition = new Vector2(0, 300f);
    panelCentral.DOAnchorPosY(0f, 0.4f)
        .SetEase(Ease.OutBack)
        .SetUpdate(true);
}

public void ReanudarJuego()
{
    // Ocultar y bloquear el cursor al reanudar
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;

    panelCentral.DOAnchorPosY(300f, 0.2f)
        .SetEase(Ease.InBack)
        .SetUpdate(true);

    canvasGroup.DOFade(0f, 0.3f)
        .SetUpdate(true)
        .OnComplete(() => {
            Time.timeScale = 1;
            PausaMenu.SetActive(false);
            juegoPausado = false;
        });
}
    public void ReiniciarJuego()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirAlMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Pantalla de inicio");
    }
}