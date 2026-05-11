using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Botones_Efecto1 : MonoBehaviour
{
    [Header("Ajustes escala")]
    public float escalaMax = 1.05f;
    public float escalaMin = 0.95f;
    public float duracion = 1f;
    public float delayInicio = 0f;

    [Header("Ajustes opacidad")]
    public float alphaMax = 1f;
    public float alphaMin = 0.5f;

    private CanvasGroup cg;

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();

        // Pulso solo en Y para no afectar el Scale X negativo
        DOTween.To(
            () => transform.localScale.y,
            y => transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z),
            escalaMax, duracion)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetDelay(delayInicio)
            .From(escalaMin);

        // Pulso de opacidad
        cg.DOFade(alphaMin, duracion)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetDelay(delayInicio)
            .From(alphaMax);
    }
}