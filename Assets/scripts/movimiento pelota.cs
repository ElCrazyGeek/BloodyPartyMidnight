using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class movimientopelota : MonoBehaviour
{
    public static movimientopelota instance;
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
    public bool Vivo = true;
    public bool Muerto = false;

    public float tiempovida;
    public bool Multi = false; 

    void Awake()
{
    instance = this;
}

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
       
       if(myinput.actions["Tiro"].WasPressedThisFrame()){

       Pelota.linearVelocity = new Vector2(Pelota.linearVelocity.x, trowforce);
       }
       time += Time.deltaTime;

       condiciones();
        aciones();

    }
    IEnumerator RutinaColor(Color colorFlash, float dur)
{
    Color ColOrg = normal; 
    Play.color = colorFlash;
    yield return new WaitForSeconds(1.5F);
    Play.color = ColOrg;
}

IEnumerator RutinaMuerte()
{

    Vivo = false;
    Muerto = true;

    Play.color = Rebote; 
    Pelota.linearVelocity = Vector2.zero;
    Pelota.angularVelocity = 0f;
    Pelota.bodyType = RigidbodyType2D.Static;
    yield return new WaitForSeconds(2f);
    Play.enabled = false;
    yield return new WaitForSeconds(2f);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}


    public void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag("suelo")){
        Play.color = Rebote;

        GameManager.instance.vida--;
        Pelota.linearVelocity = new Vector2(Pelota.linearVelocity.x, trowforce);
        if(Vivo == true)
            {
                StartCoroutine(RutinaColor(Rebote, 0.12f));
                trowforce = 30;
            } 
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.gameObject.CompareTag("rival")){
        GameManager.instance.subirPuntos();
        StartCoroutine(RutinaColor(Anotacion, 0.12f));  
            }
        if(collision.gameObject.CompareTag("Propia")){
        GameManager.instance.bajarPuntos();
        StartCoroutine(RutinaColor(Autoanotacion, 0.12f));  

            }
    }
    void aciones()
    {
        if(GameManager.instance.vida == 0)
        {
           Vivo = false;
           time = 0;
            speed = 0;
            trowforce = 0;
            time += Time.deltaTime;
            Muerto = true;
            StartCoroutine(RutinaMuerte());
        }
        if(Muerto && time >= 3  )
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            time= 0;
            Vivo = true;
            Muerto = false; 
            
            }
    }

    void condiciones()
    {
        if(time >= 10)
        {
            Pelota.gravityScale = 10;
            Pelota.mass = 2;
            Multi = true;
        }
        if(time >= 20)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                   time= 0;
        }
    }

 }
