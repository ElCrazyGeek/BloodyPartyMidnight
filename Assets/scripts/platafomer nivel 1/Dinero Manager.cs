using UnityEngine;

public class DineroManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static DineroManager instance;
    public int dinero;

     public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void AddMoney()
    {
        dinero += 6;
    }
    public void LessMoney()
    {
        dinero -= 6;
    }
   
}
