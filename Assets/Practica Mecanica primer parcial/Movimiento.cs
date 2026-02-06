using UnityEngine;

public class Movimiento : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Rigidbody2D player;
    public float moverhor;
    public float moverver;

    public float speed = 2;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moverhor = Input.GetAxisRaw("Horizontal") * speed;
        moverver = Input.GetAxisRaw("Vertical") * speed;
        player.linearVelocity = new Vector2(moverhor, moverver);
    }
}
