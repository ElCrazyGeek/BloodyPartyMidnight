using UnityEngine;

public class ControlManzanas : MonoBehaviour
{
    public static ControlManzanas instance;
    public int Puntos = 0;

    void Awake()
    {
        instance = this;
    }

    public void sumarPuntos()
    {
        Puntos++;
        Debug.Log("Puntos actuales: " + Puntos);
    }

    public void restarPuntos()
    {
        Puntos--;
        if (Puntos < 0) Puntos = 0;
        Debug.Log("Puntos actuales (RESTA): " + Puntos);
    }
}