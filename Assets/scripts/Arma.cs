using System;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public static Arma instance;
    public Rigidbody2D Pistola;

    public GameObject Bala;

    public Transform Cañon;

    public Boolean Equipada = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }
    public void Disparo()
    {
        Instantiate(Bala, Cañon.position, Cañon.rotation);
        
    }
}
