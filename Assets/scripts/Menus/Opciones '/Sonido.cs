using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sonido : MonoBehaviour
{
    [Header("Sliders")]
    public Slider sliderVolumenGeneral;
    public Slider sliderMusica;
    public Slider sliderSFX;

    [Header("Modo de Sonido")]
    public Button btnEstereo;
    public Button btnMono;
    public Color colorActivo = new Color(0.6f, 0f, 0f, 1f);
    public Color colorInactivo = new Color(0.2f, 0f, 0f, 1f);

    private bool esStereo = true;

    void Start()
    {
        // Cargar valores guardados
        sliderVolumenGeneral.value = PlayerPrefs.GetFloat("VolumenGeneral", 0.75f);
        sliderMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 0.75f);
        sliderSFX.value = PlayerPrefs.GetFloat("VolumenSFX", 0.75f);
        esStereo = PlayerPrefs.GetInt("ModoSonido", 1) == 1;

        // Listeners sliders
        sliderVolumenGeneral.onValueChanged.AddListener(OnVolumenGeneralChanged);
        sliderMusica.onValueChanged.AddListener(OnVolumenMusicaChanged);
        sliderSFX.onValueChanged.AddListener(OnVolumenSFXChanged);

        // Listeners botones
        btnEstereo.onClick.AddListener(() => SetModoSonido(true));
        btnMono.onClick.AddListener(() => SetModoSonido(false));

        // Aplicar valores iniciales
        ActualizarVolumenes();
        ActualizarBotonesModo();
    }

    void OnVolumenGeneralChanged(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("VolumenGeneral", valor);
    }

    void OnVolumenMusicaChanged(float valor)
    {
        // Conectar con AudioMixer cuando lo tengas
        PlayerPrefs.SetFloat("VolumenMusica", valor);
    }

    void OnVolumenSFXChanged(float valor)
    {
        // Conectar con AudioMixer cuando lo tengas
        PlayerPrefs.SetFloat("VolumenSFX", valor);
    }

    void SetModoSonido(bool stereo)
    {
        esStereo = stereo;
        PlayerPrefs.SetInt("ModoSonido", stereo ? 1 : 0);
        ActualizarBotonesModo();
    }

    void ActualizarVolumenes()
    {
        AudioListener.volume = sliderVolumenGeneral.value;
    }

    void ActualizarBotonesModo()
    {
        btnEstereo.GetComponent<Image>().color = esStereo ? colorActivo : colorInactivo;
        btnMono.GetComponent<Image>().color = esStereo ? colorInactivo : colorActivo;
    }
}