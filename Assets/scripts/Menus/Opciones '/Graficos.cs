using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestorGraficos : MonoBehaviour
{
    [Header("Resolución")]
    public TMP_Dropdown dropdownResolucion;

    [Header("Pantalla Completa")]
    public Button btnPantallaCompletaSi;
    public Button btnPantallaCompletaNo;

    [Header("Calidad")]
    public TMP_Dropdown dropdownCalidad;

    [Header("VSync")]
    public Button btnVSyncSi;
    public Button btnVSyncNo;

    [Header("Colores")]
    public Color colorActivo = new Color(0.6f, 0f, 0f, 1f);
    public Color colorInactivo = new Color(0.2f, 0f, 0f, 1f);

    private Resolution[] resoluciones;
    private bool pantallaCompleta;
    private bool vsync;

    void Start()
    {
        ConfigurarResolucion();
        ConfigurarCalidad();
        CargarValores();
        ConectarBotones();
    }

    void ConfigurarResolucion()
    {
        resoluciones = Screen.resolutions;
        dropdownResolucion.ClearOptions();

        var opciones = new System.Collections.Generic.List<string>();
        int indiceActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            if (!opciones.Contains(opcion))
                opciones.Add(opcion);

            if (resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
                indiceActual = opciones.Count - 1;
        }

        dropdownResolucion.AddOptions(opciones);
        dropdownResolucion.value = PlayerPrefs.GetInt("Resolucion", indiceActual);
        dropdownResolucion.onValueChanged.AddListener(SetResolucion);
    }

    void ConfigurarCalidad()
    {
        dropdownCalidad.ClearOptions();
        var niveles = new System.Collections.Generic.List<string> 
            { "Baja", "Media", "Alta", "Ultra" };
        dropdownCalidad.AddOptions(niveles);
        dropdownCalidad.value = PlayerPrefs.GetInt("Calidad", 2);
        dropdownCalidad.onValueChanged.AddListener(SetCalidad);
    }

    void CargarValores()
    {
        pantallaCompleta = PlayerPrefs.GetInt("PantallaCompleta", 1) == 1;
        vsync = PlayerPrefs.GetInt("VSync", 1) == 1;

        Screen.fullScreen = pantallaCompleta;
        QualitySettings.vSyncCount = vsync ? 1 : 0;

        ActualizarBotones();
    }

    void ConectarBotones()
    {
        btnPantallaCompletaSi.onClick.AddListener(() => SetPantallaCompleta(true));
        btnPantallaCompletaNo.onClick.AddListener(() => SetPantallaCompleta(false));
        btnVSyncSi.onClick.AddListener(() => SetVSync(true));
        btnVSyncNo.onClick.AddListener(() => SetVSync(false));
    }

    void SetResolucion(int indice)
    {
        // Buscar resolución única
        var opciones = dropdownResolucion.options;
        string[] partes = opciones[indice].text.Split('x');
        int w = int.Parse(partes[0].Trim());
        int h = int.Parse(partes[1].Trim());
        Screen.SetResolution(w, h, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolucion", indice);
    }

    void SetCalidad(int indice)
    {
        QualitySettings.SetQualityLevel(indice);
        PlayerPrefs.SetInt("Calidad", indice);
    }

    void SetPantallaCompleta(bool valor)
    {
        pantallaCompleta = valor;
        Screen.fullScreen = valor;
        PlayerPrefs.SetInt("PantallaCompleta", valor ? 1 : 0);
        ActualizarBotones();
    }

    void SetVSync(bool valor)
    {
        vsync = valor;
        QualitySettings.vSyncCount = valor ? 1 : 0;
        PlayerPrefs.SetInt("VSync", valor ? 1 : 0);
        ActualizarBotones();
    }

    void ActualizarBotones()
    {
        btnPantallaCompletaSi.GetComponent<Image>().color = pantallaCompleta ? colorActivo : colorInactivo;
        btnPantallaCompletaNo.GetComponent<Image>().color = pantallaCompleta ? colorInactivo : colorActivo;
        btnVSyncSi.GetComponent<Image>().color = vsync ? colorActivo : colorInactivo;
        btnVSyncNo.GetComponent<Image>().color = vsync ? colorInactivo : colorActivo;
    }
}