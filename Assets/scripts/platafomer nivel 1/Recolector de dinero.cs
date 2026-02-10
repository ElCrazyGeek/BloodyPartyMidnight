using UnityEngine;

public class Recolectordedinero : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int recompensa;

    public int gasto;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DineroManager.instance.dinero += recompensa;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            DineroManager.instance.dinero += recompensa;
            Destroy(gameObject);
        }

        
    }
    

}
