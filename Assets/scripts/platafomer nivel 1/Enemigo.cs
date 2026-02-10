using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D enemigo;
    public float mov;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemigo.linearVelocity = new Vector2(-mov, enemigo.linearVelocity.y);
    }
}
