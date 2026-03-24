using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Rigidbody2D rbEnemy;
    public float speed;
    public Transform[] PuntosDePatrulla;
    private int puntoActual;

    public bool isMoving;
    public int contadorDescansos;
    private Vector2[] posiciones;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Transform Ene in PuntosDePatrulla)
        {
            Ene.parent = null;
        }
    }

    // Update is called once per frame
        void Update(){
        Transform target = PuntosDePatrulla[puntoActual];

        Vector2 direction = (target.position - transform.position).normalized;

        if (isMoving)
        {
            rbEnemy.linearVelocity = direction * speed;
        }
        else
        {
            rbEnemy.linearVelocity = Vector2.zero;
        }

        if(direction.x > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if(direction.x < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

        if(Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            puntoActual++;
            contadorDescansos++;

            if(puntoActual >= PuntosDePatrulla.Length)
            {
                puntoActual = 0;
            }

            if (contadorDescansos >= 3)
            {
                StartCoroutine(Espera());
            }
        }
    }
    IEnumerator Espera()
    {
        isMoving = false;

        yield return new WaitForSeconds(1f);

        contadorDescansos = 0;
        isMoving = true;
    }
}
