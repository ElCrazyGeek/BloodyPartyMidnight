using Unity.Mathematics;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public static spawner intance;
    public Time t;
    public float Timer;
    public GameObject Manzana;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        
        if(Timer >= 5)
        {

            Instantiate(Manzana, transform.position, quaternion.identity);
            Timer = 0;
        }
    }
}
