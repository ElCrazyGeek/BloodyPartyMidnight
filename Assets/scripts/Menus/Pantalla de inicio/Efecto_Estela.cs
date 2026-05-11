using UnityEngine;
using DG.Tweening;

public class Efecto_Estela : MonoBehaviour
{
    [Header("Referencias")]
    public RectTransform botonRect;
    public RectTransform punto;
    public RectTransform colitaH;
    public RectTransform colitaV;

    [Header("Configuracion")]
    public float velocidad = 300f;
    public float largoMaxColita = 60f;

    private float ancho;
    private float alto;
    private CanvasGroup cgH;
    private CanvasGroup cgV;

    void Start()
    {
        ancho = botonRect.rect.width;
        alto = botonRect.rect.height;

        cgH = colitaH.GetComponent<CanvasGroup>() ?? colitaH.gameObject.AddComponent<CanvasGroup>();
        cgV = colitaV.GetComponent<CanvasGroup>() ?? colitaV.gameObject.AddComponent<CanvasGroup>();

        cgH.alpha = 0f;
        cgV.alpha = 0f;

        IniciarRecorrido();
    }

    void IniciarRecorrido()
    {
        float mx = ancho / 2f;
        float my = alto / 2f - 5f;

        Vector2 TL = new Vector2(-mx, my);
        Vector2 TR = new Vector2(mx, my);
        Vector2 BR = new Vector2(mx, -my);
        Vector2 BL = new Vector2(-mx, -my);

        float tH = ancho / velocidad;
        float tV = alto / velocidad;

        punto.anchoredPosition = TL;

        Sequence s = DOTween.Sequence();

        // ARRIBA: izq → der
        s.AppendCallback(() =>
        {
            cgH.alpha = 1f; cgV.alpha = 0f;
            punto.localRotation = Quaternion.identity;
            colitaH.pivot = new Vector2(1f, 0.5f); // pivot derecho, crece hacia izquierda
            colitaH.sizeDelta = new Vector2(0f, colitaH.sizeDelta.y);
        });
        s.Append(punto.DOAnchorPos(TR, tH).SetEase(Ease.Linear));
        s.Join(DOTween.To(
            () => colitaH.sizeDelta.x,
            x =>
            {
                colitaH.sizeDelta = new Vector2(x, colitaH.sizeDelta.y);
                colitaH.anchoredPosition = punto.anchoredPosition;
            },
            largoMaxColita, tH).SetEase(Ease.Linear));

        // DERECHA: arr → abj
        s.AppendCallback(() =>
        {
            cgH.alpha = 0f; cgV.alpha = 1f;
            punto.localRotation = Quaternion.Euler(0, 0, 90);
            colitaV.pivot = new Vector2(0.5f, 0f); // pivot abajo, crece hacia arriba
            colitaV.sizeDelta = new Vector2(colitaV.sizeDelta.x, 0f);
        });
        s.Append(punto.DOAnchorPos(BR, tV).SetEase(Ease.Linear));
        s.Join(DOTween.To(
            () => colitaV.sizeDelta.y,
            y =>
            {
                colitaV.sizeDelta = new Vector2(colitaV.sizeDelta.x, y);
                colitaV.anchoredPosition = punto.anchoredPosition;
            },
            largoMaxColita, tV).SetEase(Ease.Linear));

        // ABAJO: der → izq
        s.AppendCallback(() =>
        {
            cgH.alpha = 1f; cgV.alpha = 0f;
            punto.localRotation = Quaternion.identity;
            colitaH.pivot = new Vector2(0f, 0.5f); // pivot izquierdo, crece hacia derecha
            colitaH.sizeDelta = new Vector2(0f, colitaH.sizeDelta.y);
        });
        s.Append(punto.DOAnchorPos(BL, tH).SetEase(Ease.Linear));
        s.Join(DOTween.To(
            () => colitaH.sizeDelta.x,
            x =>
            {
                colitaH.sizeDelta = new Vector2(x, colitaH.sizeDelta.y);
                colitaH.anchoredPosition = punto.anchoredPosition;
            },
            largoMaxColita, tH).SetEase(Ease.Linear));

        // IZQUIERDA: abj → arr
        s.AppendCallback(() =>
        {
            cgH.alpha = 0f; cgV.alpha = 1f;
            punto.localRotation = Quaternion.Euler(0, 0, 90);
            colitaV.pivot = new Vector2(0.5f, 1f); // pivot arriba, crece hacia abajo
            colitaV.sizeDelta = new Vector2(colitaV.sizeDelta.x, 0f);
        });
        s.Append(punto.DOAnchorPos(TL, tV).SetEase(Ease.Linear));
        s.Join(DOTween.To(
            () => colitaV.sizeDelta.y,
            y =>
            {
                colitaV.sizeDelta = new Vector2(colitaV.sizeDelta.x, y);
                colitaV.anchoredPosition = punto.anchoredPosition;
            },
            largoMaxColita, tV).SetEase(Ease.Linear));

        s.SetLoops(-1, LoopType.Restart);
    }
}