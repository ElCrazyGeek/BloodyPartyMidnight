using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public int puntos;
    public int vida = 5;
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        texto.text = "Puntos: " + puntos;
    }

    // Update is called once per frame
    void Update()
    {
        if(puntos >= 8)
        {
             SceneManager.LoadScene("Examen_Primer_parcial_Mazanas");
        }
    }
    public void  subirPuntos()
    {
        puntos ++;
        texto.text = "Puntos: " + puntos;
        if(movimientopelota.instance.Multi == true)
        {
            puntos += 4;
        }
    }

    public void bajarPuntos()
    {
        puntos --;
        texto.text = "Puntos: " + puntos;
    }

    public void bajarvida()
    {
        vida --;
    }
}
