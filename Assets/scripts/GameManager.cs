using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
        
    }
    public void  subirPuntos()
    {
        puntos ++;
        texto.text = "Puntos: " + puntos;
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


 /*   void restarVida()
    {
        texto.text = "vida: " + vida;
    }
 */
}
