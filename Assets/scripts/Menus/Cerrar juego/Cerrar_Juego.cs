using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Cerrar_Juego : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelConfirmacion;
    public GameObject panelDespedida;

    [Header("Imagen despedida")]
    public RawImage imagenDespedida;
    public Texture2D[] imagenesDespedida;

    [Header("Ajustes")]
    public float tiempoPorImagen = 4.5f;
    public float tiempoFade = 0.5f;

    [Header("Botones")]
    public Button btnSi;
    public Button btnNo;

    void Start()
    {
        btnSi.onClick.AddListener(ConfirmarSalida);
        btnNo.onClick.AddListener(CancelarSalida);
    }

    public void AbrirConfirmacion()
    {
        panelConfirmacion.SetActive(true);
    }

    void CancelarSalida()
    {
        panelConfirmacion.SetActive(false);
    }

    void ConfirmarSalida()
    {
        panelConfirmacion.SetActive(false);
        panelDespedida.SetActive(true);
        StartCoroutine(MostrarDespedidaYSalir());
    }

    IEnumerator MostrarDespedidaYSalir()
    {
        if (imagenesDespedida.Length == 0)
        {
            yield return new WaitForSeconds(tiempoPorImagen);
            CerrarJuego();
            yield break;
        }

        // Imagen aleatoria
        int indice = Random.Range(0, imagenesDespedida.Length);
        imagenDespedida.texture = imagenesDespedida[indice];

        // Fade in
        imagenDespedida.color = new Color(1, 1, 1, 0);
        imagenDespedida.DOColor(Color.white, tiempoFade);

        yield return new WaitForSeconds(tiempoPorImagen);

        // Fade out
        imagenDespedida.DOColor(new Color(1, 1, 1, 0), tiempoFade);

        yield return new WaitForSeconds(tiempoFade);

        CerrarJuego();
    }

    void CerrarJuego()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
