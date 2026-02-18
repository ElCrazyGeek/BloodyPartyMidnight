using System.Data.Common;
using UnityEngine;

public class Manzana : MonoBehaviour
{
    private bool procesada = false;

    bool EsMala() => CompareTag("ManzanaMala");

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (procesada) return;

        if (collision.CompareTag("Canasta"))
        {
            procesada = true;

            if (EsMala()) ControlManzanas.instance.restarPuntos();
            else          ControlManzanas.instance.sumarPuntos();

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Suelo"))
        {
            procesada = true;

            if (!EsMala()) ControlManzanas.instance.restarPuntos();

            Destroy(gameObject);
        }
    }
}