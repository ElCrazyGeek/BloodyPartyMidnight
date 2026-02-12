using UnityEngine;

public class Recolectordedinero : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static Recolectordedinero instance;
    public int recompensa;

    public bool gasto;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            
        }
       
    }
    

}
