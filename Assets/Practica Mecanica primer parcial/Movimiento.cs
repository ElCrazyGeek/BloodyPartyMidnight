using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Rigidbody2D player;
    public PlayerInput myinput;
    public Vector2 mov;
    public Vector2 dir;
    public float speed = 2;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = myinput.actions["movimiento"].ReadValue<Vector2>() * speed;
        mov.x = dir.x;
        player.linearVelocity = new Vector2(mov.x, player.linearVelocity.y);
    }
}
