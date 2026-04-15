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

        Equipada = false;
        transform.parent = null; 
     // 1. Liberamos el arma (deja de ser hija del jugador)
    // IMPORTANTE: Dejamos de ser Trigger para poder chocar con muros
        GetComponent<Collider2D>().isTrigger = false; 
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
    if (Equipada) return;

    // 1. Lógica de rebote en Muros
    if (collision.gameObject.layer == LayerMask.NameToLayer("Muro"))
    {
        rbPistola.angularVelocity *= 0.5f;
    }

    // 2. Lógica de daño a Enemigos
    if (collision.gameObject.CompareTag("Enemigo"))
    {
        Enemigo scriptEnemigo = collision.gameObject.GetComponent<Enemigo>();
        if(scriptEnemigo != null)
        {
            scriptEnemigo.RecibirDaño(3);
            Debug.Log("¡Armazo en la cara!");
        }
        rbPistola.angularVelocity *= 0.2f;
    }

    // 3. ¿Debe detenerse y ser recolectable? 
    // Es mejor checar esto después de que la física haga lo suyo
    if (rbPistola.linearVelocity.magnitude < 1.5f) 
    {
        GetComponent<Collider2D>().isTrigger = true;
        // No pongas velocity a cero aquí abruptamente para que el rebote termine natural
    }
}

private void FrenarArma()
{
    // Reduce la rotación drásticamente al chocar para que se vea pesado
    rbPistola.angularVelocity = rbPistola.angularVelocity * 0.2f;
    // Opcional: podrías añadir un pequeño rebote si usas un Physics Material 2D
}
    
}