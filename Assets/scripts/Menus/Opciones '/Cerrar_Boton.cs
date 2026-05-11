using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Cerrar_Boton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Ajustes")]
    public float escalaHover = 1.2f;
    public float duracion = 0.15f;
    public float escalaClick = 0.9f;

    void Start()
    {
        transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(escalaHover, duracion).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, duracion).SetEase(Ease.OutBack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOScale(escalaClick, 0.05f)
            .SetEase(Ease.InBack)
            .OnComplete(() => transform.DOScale(1f, 0.1f));
    }
}
