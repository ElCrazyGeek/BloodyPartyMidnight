using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Pausa : MonoBehaviour
{
    public GameObject PausaMenu;
    public CanvasGroup canvasGroup;
    public RectTransform panelCentral;
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

        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, 0.3f).SetUpdate(true);

        panelCentral.anchoredPosition = new Vector2(0, 300f);
        panelCentral.DOAnchorPosY(0f, 0.35f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    public void ReanudarJuego()
    {
        panelCentral.DOAnchorPosY(300f, 0.2f)
            .SetEase(Ease.InBack)
            .SetUpdate(true);

        canvasGroup.DOFade(0f, 0.25f)
            .SetUpdate(true)
            .OnComplete(() => {
                Time.timeScale = 1;
                PausaMenu.SetActive(false);
                juegoPausado = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            });
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
        DOTween.KillAll();
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Pantalla de inicio");
    }
}