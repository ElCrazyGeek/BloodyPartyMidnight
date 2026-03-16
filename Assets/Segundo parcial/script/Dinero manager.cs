using UnityEngine;

public class Dineromanager : MonoBehaviour
{
    public static Dineromanager instance;
    public int dineroTotal = 0;
   private void Awake()
    {
        instance = this;
    }

    public void obtenerDinero(int reward)
    {
        dineroTotal += reward;
    }
}
