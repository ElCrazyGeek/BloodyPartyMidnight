using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class movimientopelota : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D Pelota;
    public PlayerInput myinput;
    public Vector2 dir;
    public Vector2 mov;
    public float speed;
    public float trowforce;
    public bool cantrow;
    public SpriteRenderer Play;
    public Color normal;
    public Color Rebote;
    public Color Anotacion;
    public Color Autoanotacion;
    public float time;
    public Time tiempo;

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

  
      
       aciones();

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("suelo")){
        Play.color = Rebote;
        cantrow = true;
        GameManager.instance.vida--;
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.gameObject.CompareTag("rival")){
        GameManager.instance.subirPuntos();
        Play.color = Anotacion; 
            }
        if(collision.gameObject.CompareTag("Propia")){
        GameManager.instance.bajarPuntos();  
        Play.color = Autoanotacion; 

            }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
       
    }

    void aciones()
    {
        if(GameManager.instance.vida == 0)
        {
            speed = 0;
            trowforce = 0;
            time += Time.deltaTime;
        }
         if(time >= 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            time= 0;
            
        } 
    }

 }
