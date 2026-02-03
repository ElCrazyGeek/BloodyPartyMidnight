using UnityEngine;


public class movimiento : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

       private Rigidbody2D player;
       private Transform playertrans;
       public Color playercolor;
       public SpriteRenderer SR;

       public float movedir;
       public float movever;

       public float speed;
       public bool izq;
       public bool der;
       public float posx;
       public float posy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playertrans = GetComponent<Transform>();
        SR = GetComponent<SpriteRenderer>();
    }

    
    // Update is called once per frame
    void Update()
    {
        if(izq) /// ponerlo asi activa una casilla en el inspector que si ponemos true o false
        {
            player.linearVelocity = new Vector2(-speed, player.linearVelocity.x);
        } 
        if(der)
        {
            player.linearVelocity = new Vector2(speed, player.linearVelocity.x);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.gravityScale = 1f;
            playertrans.localScale = new Vector3(0.3f, 0.2f,1);
            SR.color = playercolor;

       //     SpriteRenderer 

        }

        movedir = Input.GetAxisRaw("Horizontal") * speed; 
        movever = Input.GetAxisRaw("Vertical") * speed;
        player.linearVelocity = new Vector2(movedir, movever);


        //guardar la posicion cuando dejes de presiona F
        //cambiar de color



    }
}
