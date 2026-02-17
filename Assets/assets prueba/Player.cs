using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody2D player;
    public PlayerInput myinput;

    public Vector2 mov;
    public Vector2 input;

    public int speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    ///movimiento izquierda derecha 
     input = myinput.actions["movimiento"].ReadValue<Vector2>();
     ///asignamos cual sera el la 
     mov.x = input.x;
     /// le decimo al Rigibody.velocidad es igual a un nuevo vector donde la posicion 
     /// va ser la del movimiento y el y va ser la que asignemos en el inpsector
     player.linearVelocity = new Vector2(mov.x * speed, player.linearVelocity.y);
    }
}
