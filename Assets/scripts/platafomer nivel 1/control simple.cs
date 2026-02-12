using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{

    /// <summary>
    /// variables
    /// </summary>

    public Rigidbody2D player;
    public PlayerInput input;
    private Vector2 mov;
    private Vector2 dir;
    public float jumpforce;
    public SpriteRenderer Sprite;
    public Color Status1;
    public Color Status2;

    public bool canjump = true;

    public int speed = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           // siempre que son vectores o movimiento "readvalue" 
           //exception con juegos moviles touch y acelerometro
        dir = input.actions["Movement"].ReadValue<Vector2>();
        mov.x = dir.x * speed;
        mov.y = dir.y * jumpforce;

        player.linearVelocity = new Vector2(mov.x, player.linearVelocity.y);

        
        if (input.actions["jump"].WasPressedThisFrame() && canjump)
        {
            player.linearVelocity = new Vector2(player.linearVelocity.x,jumpforce);
            canjump = false;
        }
        // siempre que sean botones "waspressedthisframe" 
        if (input.actions["Reset"].WasPressedThisFrame())
        {
            ReiniciarNivel();
        }
       
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("suelo"))
        {
            canjump = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    //void | tipo de interaccion | componente | parametro (nombre del componente)
    {
        // revisar el nombre del parametro | gameobject | tipo de accion a hacer
        if (collision.gameObject.CompareTag("coin"))
        {
            DineroManager.instance.AddMoney();
            //las amarillas son componente llevan "()" y dentro van 
            //referenciados los objetos que afectaremos 
            DesignarColor();
            Destroy(collision.gameObject);
            
        }
        
        if (collision.gameObject.CompareTag("CoinN"))
        {
            DineroManager.instance.LessMoney();
            //las amarillas son componente llevan "()" y dentro van 
            //referenciados los objetos que afectaremos 
            DesignarColor();
            Destroy(collision.gameObject);
            
        }
    }

    public void DesignarColor()
    {
        if(DineroManager.instance.dinero > 0)
        {
            Sprite.color = Status1;
        }

        if(DineroManager.instance.dinero < 0)
        {
            Sprite.color = Status2;
        }
    }
    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

