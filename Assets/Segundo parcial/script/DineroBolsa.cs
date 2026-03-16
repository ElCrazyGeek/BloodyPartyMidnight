using UnityEngine;

public class DineroBolsa : MonoBehaviour
{
    public int dinero = 0;
    public bool small;
    public bool medium;
    public bool big;

    public bool recolectada = false;
    void Start()
    {
        if (small)
        {
            dinero = Random.Range(5,10);
        }
         if (medium)
        {
            dinero = Random.Range(10,20);

        }
        if (big)
        {
            dinero = Random.Range(20,50);
        }
    }
}
