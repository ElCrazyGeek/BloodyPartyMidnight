using UnityEngine;

public class Bala : MonoBehaviour
{
    public float fuerza = 35;
    public Rigidbody2D bala;
    
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dirBala(Vector2 dir)
    {
        bala.linearVelocity = dir * fuerza;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
                Destroy(collision.gameObject);
                Destroy(gameObject);
        }
        
       if (collision.gameObject.CompareTag("Muro"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("MuroDes"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

   
}
