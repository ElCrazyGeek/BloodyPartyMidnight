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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        
        if(Timer >= 3)
        {

            Instantiate(Manzana, transform.position, quaternion.identity);
            Timer = 0;
            Veneno ++;
        }
        
        if(Veneno >= 3)
        {

            Instantiate(ManzanaMala, transform.position, quaternion.identity);
            Timer = 0;
            Veneno = 0;
        }
    }

    
}
