using UnityEditor.SpeedTree.Importer;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    public Rigidbody2D Player;

    public Vector2 dir;

    public Vector2 mov;

    public PlayerInput input; 

    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = input.actions["Movimiento"].ReadValue<Vector2>() * speed;
        mov.x = dir.x; 
        mov.y = dir.y;

        Player.linearVelocity = new Vector2(mov.x, mov.y);
    }
}
