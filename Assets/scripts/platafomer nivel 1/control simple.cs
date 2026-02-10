using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{

    /// <summary>
    /// variables
    /// </summary>

    public Rigidbody2D player;
    public PlayerInput input;
    private Vector2 mov;
    private Vector2 dir;

    public int speed = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = input.actions["Movement"].ReadValue<Vector2>();
        mov.x = dir.x * speed;
        mov.y = dir.y * speed;

        player.linearVelocity = new Vector2(mov.x, player.linearVelocity.y); 


    }
}
