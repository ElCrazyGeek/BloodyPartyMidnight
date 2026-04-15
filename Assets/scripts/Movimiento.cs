using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movimiento : MonoBehaviour
{  
    /// <summary>
    /// aqui cree un header porque esa madre ya estaba creando un pinche desmadre que
    /// al principio no mas Dios y yo voy a entender y luego solo Dios
    /// asi que mejor voy a organizar todo de una vez
    /// </summary>
    [Header("Movimiento del jugador")]
    public Rigidbody2D Player;
    public Vector2 dir;
    public Vector2 mov;
    public PlayerInput input; 
    public float speed;
    public float vida = 20;
    /// <summary>
    /// aqui van ir los valores del arma cuando sean necesarios
    /// mas que nada solo que el jugador la pueda tomar o no, y lo demas
    /// mejor se lo voy a dejar a cada arma
    /// </summary>
    [Header("Vaiables Arma")]
    public Arma ArmaJugador;
    public Arma ArmaRecoletable;
    
    public GameObject pivote;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = mouse - transform.position;
        float angulo = Mathf.Atan2(direccion.y,direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angulo);
        dir = input.actions["Movimiento"].ReadValue<Vector2>() * speed;
        mov.x = dir.x; 
        mov.y = dir.y;

        Player.linearVelocity = new Vector2(mov.x, mov.y);
        

        DisparoJugador();
        RecogerArma();
        CheckLanzamiento();

        
    }

void DisparoJugador()
{
    if (input.actions["Disparo"].WasPressedThisFrame())
    {
        // Usamos la referencia local del arma que tiene el jugador, NO el static instance
        if (ArmaJugador != null && ArmaJugador.Equipada)
        {
            ArmaJugador.Disparo();
        }
    }
}

    void RecogerArma()
    {
        if (input.actions["Recoger objeto"].WasPressedThisFrame() && ArmaRecoletable )
        {
            ArmaEquipada(ArmaRecoletable);
        }
    }
    void CheckLanzamiento()
{
    // Usualmente en Hotline Miami se lanza con Click Derecho
    if (input.actions["Lanzar objeto"].WasPressedThisFrame() && ArmaJugador != null)
    {
        ArmaJugador.LanzarArma(); // Ejecuta la lógica física del arma
        ArmaJugador = null;       // El jugador ya no tiene arma en la mano
    }
}

    
   void OnTriggerEnter2D(Collider2D collision)
    {
     
            if (collision.CompareTag("Arma"))
            {
                ArmaRecoletable = collision.GetComponent<Arma>(); 
            }
    }
    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Arma"))
    {
        ArmaRecoletable = collision.gameObject.GetComponent<Arma>();
    }
}

public void ArmaEquipada(Arma nuevaArma)
{
    ArmaJugador = nuevaArma;
    ArmaJugador.Equipada = true;
    ArmaJugador.transform.SetParent(pivote.transform);

    ArmaJugador.transform.localPosition = Vector3.zero;
    ArmaJugador.transform.localRotation = Quaternion.Euler(0, 0, 90);

    // Usamos la referencia que ya existe en el arma
    if (ArmaJugador.rbPistola != null)
    {
        ArmaJugador.rbPistola.simulated = false;
    }
}
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arma"))
        {
            ArmaRecoletable = null;
        }
    }
    void Morir() {
    GameManager.instance.JugadorMurio();
    // Desactivar movimiento del jugador, etc.
    }


}
