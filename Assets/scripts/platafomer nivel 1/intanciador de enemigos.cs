using Unity.Mathematics;
using UnityEngine;

public class intanciadordeenemigos : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float timer;
    public GameObject Enemigo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 5)
        {
            timer = 0;
            Instantiate(Enemigo, transform.position, quaternion.identity);

        }
    }
}
