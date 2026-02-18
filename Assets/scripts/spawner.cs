using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public static spawner instance;
    public Time t;
    public float Timer;
    public int Veneno = 0;
    public GameObject Manzana;
    public GameObject ManzanaMala;
    public int Contador = 0;
    public float limite = 3;
    public int pass = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        
        if(Timer >= limite)
        {

            Instantiate(Manzana, transform.position, quaternion.identity);
            Timer = 0;
            Veneno ++;
            Contador ++;
        }
        
        if(Veneno >= 3)
        {

            Instantiate(ManzanaMala, transform.position, quaternion.identity);
            Timer = 0;
            Veneno = 0;
        }

        if(Contador == 6 && pass < 1)
        {
            Timer = 0;
            limite = limite - 0.5f;
            pass++;
        }
            if(Contador == 12 && pass < 2)
        {
            Timer = 0;
            limite = limite - 1f;
            pass++;
        }
    }

    
}
