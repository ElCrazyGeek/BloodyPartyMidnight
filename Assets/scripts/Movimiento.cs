using UnityEngine;
using UnityEngine.InputSystem;

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
    /// <summary>
    /// aqui van ir los valores del arma cuando sean necesarios
    /// mas que nada solo que el jugador la pueda tomar o no, y lo demas
    /// mejor se lo voy a dejar a cada arma
    /// </summary>
    [Header("Vaiables Arma")]
    public Arma ArmaJugador;
    public Arma ArmaRecoletable;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = input.actions["Movimiento"].ReadValue<Vector2>() * speed;
        mov.x = dir.x; 
        mov.y = dir.y;

        Player.linearVelocity = new Vector2(mov.x, mov.y);

        DisparoJugador();
    }

    void DisparoJugador()
    {
        if (input.actions["Disparo"].WasPressedThisFrame())
        {
            
            if (Arma.instance.Equipada == true)
            {
                Arma.instance.Disparo();
            }
        }
    }
   void OnTriggerEnter2D(Collider2D collision)
    {
        if (input.actions["Recoger objeto"].WasPressedThisFrame())
        {
            if (collision.CompareTag("Arma"))
            {
                ArmaRecoletable = collision.GetComponent<Arma>(); 
            }
        }
    }


}
