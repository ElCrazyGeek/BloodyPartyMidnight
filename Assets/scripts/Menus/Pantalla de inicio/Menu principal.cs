using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Menuprincipal : MonoBehaviour
{

    [Header("Botones de Navegacion")]
    public Button Izquierda;
    public Button Derecha;

    [Header("Boton Activo")]

    public Button Activo;  
    public TextMeshProUGUI TextoActivo; 

    public CanvasGroup CGActivo;

    [Header("Botones de fondo")]
    public TextMeshProUGUI TextoIzquierdo;
    public CanvasGroup CGIzquierdo;
    public TextMeshProUGUI TextoDerecho;
    public CanvasGroup CGDerecho;   
    
    [Header("Opciones")]

    public string [] opciones = {"INICIAR PARTIDA", "CARGAR PARTIDA", "SELECTOR DE NIVELES", "Galeria", "Opciones", "Salir del Juego" };
    public int indiceActual = 0;

    [Header("Ajustes de animacion")]
    public float duracionFade = 0.15f;
    public float escalaFantasma = 0.85f;
    public float glitchOffset = 4f;
    void Start()
    {
        Izquierda.onClick.AddListener(Anterior);
        Derecha.onClick.AddListener(Siguiente);
        
        CGIzquierdo.alpha = 0.4f;
        CGDerecho.alpha = 0.4f;
        CGActivo.alpha = 1f;

        TextoIzquierdo.transform.localScale = Vector3.one * escalaFantasma;
        TextoDerecho.transform.localScale = Vector3.one * escalaFantasma;

        ActualizarTexto();
    }

    void Update()
{
    float scroll = Input.GetAxis("Mouse ScrollWheel");
    
    if (scroll > 0f)
        Anterior();
    else if (scroll < 0f)
        Siguiente();
}

    void Siguiente(){
        indiceActual = (indiceActual + 1 ) % opciones.Length;
        AnimarCambio();
    }

    void Anterior()
    {
        indiceActual =(indiceActual - 1 + opciones.Length) % opciones.Length;
        AnimarCambio();
    }

    void AnimarCambio()
    {
         CGActivo.DOFade(0f, duracionFade);
        Activo.transform.DOScale(0.85f, duracionFade).OnComplete(() =>
        {
            ActualizarTexto();

            // Glitch: offset rapido en X antes del fade in
            RectTransform rt = Activo.GetComponent<RectTransform>();
            rt.anchoredPosition += new Vector2(glitchOffset, 0f);
            rt.DOAnchorPosX(rt.anchoredPosition.x - glitchOffset, 0.05f).OnComplete(() =>
            {
 
                CGActivo.DOFade(1f, duracionFade);
                Activo.transform.DOScale(1f, duracionFade);
            });
        });

        // --- Fantasmas: fade out + scale down luego fade in ---
        CGIzquierdo.DOFade(0f, duracionFade).OnComplete(() =>
        {
            CGIzquierdo.DOFade(0.4f, duracionFade);
        });
        TextoIzquierdo.transform.DOScale(0.8f, duracionFade).OnComplete(() =>
        {
            TextoIzquierdo.transform.DOScale(escalaFantasma, duracionFade);
        });

        CGDerecho.DOFade(0f, duracionFade).OnComplete(() =>
        {
            CGDerecho.DOFade(0.4f, duracionFade);
        });
        TextoDerecho.transform.DOScale(0.8f, duracionFade).OnComplete(() =>
        {
            TextoDerecho.transform.DOScale(escalaFantasma, duracionFade);
        });
    
    }

    void ActualizarTexto()
    {
        int anterior = (indiceActual - 1 + opciones.Length) % opciones.Length;
        int siguiente = (indiceActual + 1) % opciones.Length;
        TextoActivo.text = opciones[indiceActual];
        TextoIzquierdo.text = opciones[anterior];
        TextoDerecho.text = opciones[siguiente];
    }
}
