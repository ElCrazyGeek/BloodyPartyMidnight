using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class movimientopelota : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D Pelota;
    public PlayerInput myinput;
    public Vector2 dir;
    public Vector2 mov;
    public float speed;
    public float trowforce;
    public Vector2 tiro;
    public bool cantrow;

    public SpriteRenderer Play;

    public Color normal;

    public Color Rebote;
    public Color Anotacion;
    public Color Autoanotacion;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       dir = myinput.actions["Movimiento"].ReadValue<Vector2>();
       mov.x = dir.x * speed;
       mov.y = dir.y * trowforce;

       Pelota.linearVelocity = new Vector2(mov.x, Pelota.linearVelocity.y); 
       
       if(myinput.actions["Tiro"].WasPressedThisFrame() && cantrow){

       Pelota.linearVelocity = new Vector2(Pelota.linearVelocity.x, trowforce);
       cantrow = false;
       }
  
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("suelo")){
          Play.color = Rebote;
            cantrow = true;
        }
        else
        {
            Play.color = normal;
        }
        
        if(collision.gameObject.CompareTag("rival")){
            Play.color = Anotacion; 
            }
        if(collision.gameObject.CompareTag("Propia")){
            Play.color = Autoanotacion; 
            }

    }

}
