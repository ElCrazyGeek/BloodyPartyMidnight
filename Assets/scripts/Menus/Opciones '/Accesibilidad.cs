using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestorAccesibilidad : MonoBehaviour
{
    [Header("Subtítulos")]
    public Button btnSubtitulosSi;
    public Button btnSubtitulosNo;

    [Header("Tamaño de Letra")]
    public Button btnChica;
    public Button btnMediana;
    public Button btnGrande;

    [Header("Colores")]
    public Color colorActivo = new Color(0.6f, 0f, 0f, 1f);
    public Color colorInactivo = new Color(0.2f, 0f, 0f, 1f);

    private bool subtitulosActivos;
    private int tamañoLetra; // 0=Chica, 1=Mediana, 2=Grande

    void Start()
    {
        subtitulosActivos = PlayerPrefs.GetInt("Subtitulos", 1) == 1;
        tamañoLetra = PlayerPrefs.GetInt("TamañoLetra", 1);

        btnSubtitulosSi.onClick.AddListener(() => SetSubtitulos(true));
        btnSubtitulosNo.onClick.AddListener(() => SetSubtitulos(false));

        btnChica.onClick.AddListener(() => SetTamañoLetra(0));
        btnMediana.onClick.AddListener(() => SetTamañoLetra(1));
        btnGrande.onClick.AddListener(() => SetTamañoLetra(2));

        ActualizarBotones();
    }

    void SetSubtitulos(bool valor)
    {
        subtitulosActivos = valor;
        PlayerPrefs.SetInt("Subtitulos", valor ? 1 : 0);
        ActualizarBotones();
    }

    void SetTamañoLetra(int tamaño)
    {
        tamañoLetra = tamaño;
        PlayerPrefs.SetInt("TamañoLetra", tamaño);
        ActualizarBotones();

        // Aplicar tamaño a los textos TMP de la UI
        float[] tamaños = { 15f, 35f, 45f };
       var textos = FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include);
        foreach (var texto in textos)
            texto.fontSize = tamaños[tamaño];
    }

    void ActualizarBotones()
    {
        btnSubtitulosSi.GetComponent<Image>().color = subtitulosActivos ? colorActivo : colorInactivo;
        btnSubtitulosNo.GetComponent<Image>().color = subtitulosActivos ? colorInactivo : colorActivo;

        btnChica.GetComponent<Image>().color = tamañoLetra == 0 ? colorActivo : colorInactivo;
        btnMediana.GetComponent<Image>().color = tamañoLetra == 1 ? colorActivo : colorInactivo;
        btnGrande.GetComponent<Image>().color = tamañoLetra == 2 ? colorActivo : colorInactivo;
    }
}