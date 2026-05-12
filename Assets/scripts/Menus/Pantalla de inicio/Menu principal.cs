using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Button Opcionizquierda;
    public TextMeshProUGUI TextoIzquierdo;
    public CanvasGroup CGIzquierdo;
    public Button OpcionDerecha;
    public TextMeshProUGUI TextoDerecho;
    public CanvasGroup CGDerecho;   
    public Cerrar_Juego gestorSalida;
    
    [Header("Opciones")]

    public string [] opciones = {"INICIAR PARTIDA", "CARGAR PARTIDA", "SELECTOR DE NIVELES", "Galeria", "Opciones", "Salir del Juego" };
    public int indiceActual = 0;

    [Header("Ajustes de animacion")]
    public float duracionFade = 0.15f;
    public float escalaFantasma = 0.85f;
    public float glitchOffset = 4f;

    [Header("Panel Opciones")]
public GameObject panelOpciones;
    void Start()
    {
        Izquierda.onClick.AddListener(Anterior);
        Derecha.onClick.AddListener(Siguiente);
        OpcionDerecha.onClick.AddListener(Siguiente);
        Opcionizquierda.onClick.AddListener(Anterior);
        
        CGIzquierdo.alpha = 0.4f;
        CGDerecho.alpha = 0.4f;
        CGActivo.alpha = 1f;

        Opcionizquierda.transform.localScale = Vector3.one * escalaFantasma;
        OpcionDerecha.transform.localScale = Vector3.one * escalaFantasma;

        ActualizarTexto();
        Activo.onClick.AddListener(() =>
        {
             if (opciones[indiceActual] == "Iniciar partida")
            SceneManager.LoadScene("Nivel demo");
            else if (opciones[indiceActual] == "Opciones")
             AbrirOpciones();
        });

        Activo.onClick.AddListener(() =>
        {
        if (opciones[indiceActual] == "Opciones")
        AbrirOpciones();
        });

        Activo.onClick.AddListener(() =>
        {
    if (opciones[indiceActual] == "Iniciar partida")
        SceneManager.LoadScene("Nivel demo");
    else if (opciones[indiceActual] == "Opciones")
        AbrirOpciones();
    else if (opciones[indiceActual] == "Salir del Juego")
        gestorSalida.AbrirConfirmacion();
    });

    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape) && panelOpciones.activeSelf)
        CerrarOpciones();
    

    float scroll = Input.GetAxis("Mouse ScrollWheel");
    if (scroll > 0f) Anterior();
    else if (scroll < 0f) Siguiente();
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
        // Boton activo: fade out + scale down
        CGActivo.DOFade(0f, duracionFade);
        Activo.transform.DOScale(0.85f, duracionFade).OnComplete(() =>
        {
            ActualizarTexto();

            // Glitch
            RectTransform rt = Activo.GetComponent<RectTransform>();
            rt.anchoredPosition += new Vector2(glitchOffset, 0f);
            rt.DOAnchorPosX(rt.anchoredPosition.x - glitchOffset, 0.05f).OnComplete(() =>
            {
                CGActivo.DOFade(1f, duracionFade);
                Activo.transform.DOScale(1f, duracionFade);
            });
        });

        // Fantasma izquierdo
        CGIzquierdo.DOFade(0f, duracionFade).OnComplete(() =>
            CGIzquierdo.DOFade(0.4f, duracionFade));
        Opcionizquierda.transform.DOScale(0.8f,duracionFade).OnComplete(() =>
        Opcionizquierda.transform.DOScale(escalaFantasma,duracionFade));

        // Fantasma derecho
        CGDerecho.DOFade(0f, duracionFade).OnComplete(() =>
            CGDerecho.DOFade(0.4f, duracionFade));
        OpcionDerecha.transform.DOScale(0.8f, duracionFade).OnComplete(() =>
            OpcionDerecha.transform.DOScale(escalaFantasma, duracionFade));
    }

    void ActualizarTexto()
    {
        int anterior = (indiceActual - 1 + opciones.Length) % opciones.Length;
        int siguiente = (indiceActual + 1) % opciones.Length;
        TextoActivo.text = opciones[indiceActual];
        TextoIzquierdo.text = opciones[anterior];
        TextoDerecho.text = opciones[siguiente];
    }


    public void AbrirOpciones()
{
    panelOpciones.SetActive(true);
}

public void CerrarOpciones()
{
    panelOpciones.SetActive(false);
}
}
