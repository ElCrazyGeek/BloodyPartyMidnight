using UnityEngine;
using UnityEngine.UI;

public class GestorControles : MonoBehaviour
{
    [Header("Selector")]
    public Button btnMando;
    public Button btnTeclado;

    [Header("Paneles")]
    public GameObject panelMando;
    public GameObject panelTeclado;

    [Header("Colores")]
    public Color colorActivo = new Color(0.6f, 0f, 0f, 1f);
    public Color colorInactivo = new Color(0.2f, 0f, 0f, 1f);

    private bool seleccionManual = false;

    void Start()
    {
        btnMando.onClick.AddListener(() => {
            seleccionManual = true;
            MostrarPanel(false);
        });
        btnTeclado.onClick.AddListener(() => {
            seleccionManual = true;
            MostrarPanel(true);
        });

        bool eraTeclado = PlayerPrefs.GetInt("UltimoControl", 1) == 1;
        MostrarPanel(eraTeclado);
    }

    void Update()
    {
        if (seleccionManual) return;

        string[] joysticks = Input.GetJoystickNames();
        bool hayMando = joysticks.Length > 0 && joysticks[0] != "";

        if (hayMando && !panelMando.activeSelf)
            MostrarPanel(false);
        else if (!hayMando && !panelTeclado.activeSelf)
            MostrarPanel(true);
    }

    void MostrarPanel(bool teclado)
    {
        panelTeclado.SetActive(teclado);
        panelMando.SetActive(!teclado);

        btnTeclado.GetComponent<Image>().color = teclado ? colorActivo : colorInactivo;
        btnMando.GetComponent<Image>().color = teclado ? colorInactivo : colorActivo;

        PlayerPrefs.SetInt("UltimoControl", teclado ? 1 : 0);
    }
}