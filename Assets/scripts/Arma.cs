using System;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public static Arma instance;
    public Rigidbody2D rbPistola; // El Rigidbody de la propia pistola

    public GameObject PrefabBala;
    public Transform Cañon;

    public bool Equipada; // Cambiado a bool (minúscula es el estándar de C#)
    public int municion;
    public float fuerzaLanzamiento = 20f;

    void Awake()
    {
        instance = this;
        // Si la empezamos equipada, el Rigidbody debe estar quieto (Kinematic)
        if(Equipada) PrepararParaMano();
    }

    public void Disparo()
    {
        if(Equipada && municion > 0)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0f;
            Vector2 direccion = (mouse - Cañon.position).normalized;

            GameObject bala = Instantiate(PrefabBala, Cañon.position, Cañon.rotation);
            bala.GetComponent<Bala>().dirBala(direccion);
            municion -= 1; 
        }       
    }

    // --- NUEVA FUNCIÓN DE LANZAMIENTO ---
    public void LanzarArma()
    {
        if (!Equipada) return;

        // 1. Liberamos el arma (deja de ser hija del jugador)
        Equipada = false;
        transform.parent = null; 

        // 2. Activamos la física para que vuele
        rbPistola.bodyType = RigidbodyType2D.Dynamic;
        rbPistola.simulated = true;

        // 3. Calculamos dirección al mouse
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        Vector2 direccionLanzamiento = (mouse - transform.position).normalized;

        // 4. Aplicamos fuerza y rotación
        rbPistola.linearVelocity = direccionLanzamiento * fuerzaLanzamiento;
        rbPistola.angularVelocity = -500f; // Para que gire en el aire como en Hotline Miami
        
        if (Arma.instance == this) 
        {
        Arma.instance = null;
        }
    }

    private void PrepararParaMano()
    {
        rbPistola.bodyType = RigidbodyType2D.Kinematic;
        rbPistola.linearVelocity = Vector2.zero;
        rbPistola.angularVelocity = 0;
    }

    

    // Detectar si el arma lanzada golpea a un enemigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Solo hace daño si NO está equipada (es decir, va volando)
        if (!Equipada && collision.gameObject.CompareTag("Enemigo"))
        {
            // Accedemos al script del enemigo que ya tienes
            Enemigo scriptEnemigo = collision.gameObject.GetComponent<Enemigo>();
            if(scriptEnemigo != null)
            {
                scriptEnemigo.RecibirDaño(3); // Daño por impacto de arma
                Debug.Log("¡Enemigo golpeado por arma lanzada!");
            }
            
            // Al chocar, el arma pierde fuerza y cae
            rbPistola.angularVelocity = 0;
        }
    }

    
}