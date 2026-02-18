using System;
using UnityEngine;

public class ControlManzanas : MonoBehaviour
{
    public static ControlManzanas instance;
    public int Puntos = 0;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sumarPuntos()
    {
        Puntos ++;
    }

    public void restarPuntos()
    {
        Puntos --; 
    }
}
