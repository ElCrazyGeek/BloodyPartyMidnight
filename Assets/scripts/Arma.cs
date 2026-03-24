using System;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public static Arma instance;
    public Rigidbody2D Pistola;

    public GameObject PrefabBala;

    public Transform Cañon;

    public Boolean Equipada;

    public int municion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }
    public void Disparo()
    {
        if(Equipada && municion > 0){
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = mouse - Cañon.position;

        GameObject bala = Instantiate(PrefabBala, Cañon.position, Cañon.rotation);
        bala.GetComponent<Bala>().dirBala(direccion);
        municion -= 1; 
        }       
    }
   

}
