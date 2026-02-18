using UnityEngine;
using UnityEngine.InputSystem;

public class movimientocanasta : MonoBehaviour
{
    public static movimientocanasta intance;
    public Rigidbody2D canasta;
    public Vector2 dir;
    public Vector2 mov;
    public float speed;
    public PlayerInput Canasta;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void awake()
    {
        intance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Awake()
    {
        intance = this;
    }
    void Update()
    {
        dir = Canasta.actions["Movimiento"].ReadValue<Vector2>();
        mov.x = dir.x * speed;
        canasta.linearVelocity = new Vector2(mov.x,canasta.linearVelocity.y);
    }
  
}
