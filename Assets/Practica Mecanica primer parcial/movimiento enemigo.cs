using UnityEngine;

public class movimientoenemigo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D RBEnemigo;
    public float speed;
    public float movy = 1f;
    public float movx = 1f;

    public float LimDer = 8.5f;
    public float LimIzq = -8.5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movimiento();
    }

    void movimiento()
    {
        RBEnemigo.linearVelocity = new Vector2(movx * speed, RBEnemigo.linearVelocity.y);

        if (RBEnemigo.transform.position.x >= LimDer && movx > 0|| RBEnemigo.transform.position.x <= LimIzq && movx < 0)
        {

            movx *= -1f;
            desenso();
            
        }
    }

    void desenso()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - movy);
    }

}
