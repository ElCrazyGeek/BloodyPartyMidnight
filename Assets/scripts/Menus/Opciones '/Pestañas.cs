using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pestañas : MonoBehaviour
{
    [Header("Pestañas")]
    public Button[] pestañas;

    [Header("Paneles")]
    public GameObject[] paneles;

    [Header("Colores")]
    public Color colorActivo = new Color(0.6f, 0f, 0f, 1f);
    public Color colorInactivo = new Color(0.2f, 0f, 0f, 1f);

    private int indiceActual = 0;

    void Start()
    {
        for (int i = 0; i < pestañas.Length; i++)
        {
            int indice = i;
            pestañas[i].onClick.AddListener(() => CambiarPestaña(indice));
        }

        CambiarPestaña(0);
    }

    void CambiarPestaña(int indice)
    {
        for (int i = 0; i < paneles.Length; i++)
        {
            paneles[i].SetActive(i == indice);
            pestañas[i].GetComponent<Image>().color = 
                i == indice ? colorActivo : colorInactivo;
        }

        indiceActual = indice;
    }
}
