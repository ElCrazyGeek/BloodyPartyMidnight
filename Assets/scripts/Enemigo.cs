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
    public float vida = 10;

    public bool vivo;
    public Transform jugador;
    public float rangoVision = 6f;
    public float rangoPerdida = 8f;
    public float anguloVision = 90f;
    public LayerMask capaParedes;
    public float distanciaPared = 0.5f;
    public bool persiguiendo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
{
    vivo = true;
    isMoving = true;

    foreach (Transform Ene in PuntosDePatrulla)
    {
        Ene.parent = null;
    }
}

    // Update is called once per frame
void Update()
{
    if (!vivo) return;

    if (PuedeVerJugador())
    {
        persiguiendo = true;
    }
    else if (persiguiendo)
    {
        // Si lo pierde de vista, lo sigue buscando hasta que se aleje demasiado
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        if (distanciaJugador >= rangoPerdida)
        {
            persiguiendo = false;
        }
    }

    if (persiguiendo)
    {
        Vector2 direccionJugador = (jugador.position - transform.position).normalized;

RaycastHit2D pared = Physics2D.Raycast(
    transform.position,
    direccionJugador,
    distanciaPared,
    capaParedes
);

if (pared.collider == null)
{
    rbEnemy.linearVelocity = direccionJugador * speed;
}
else
{
    rbEnemy.linearVelocity = Vector2.zero;
}

        // --- MIRA DIRECTO AL JUGADOR ---
        RotarHacia(direccionJugador);
    }
    else
    {
        Patrulla();
    }

    VerificarMuerte();
}


    public void RecibirDaño(float cantidad)
    {
        vida -= cantidad;
    }
    void VerificarMuerte()
    {
        if (vida <= 0 && vivo)
        {
            vivo = false;
            isMoving = false;
            rbEnemy.linearVelocity = Vector2.zero;
        }
    }

  /*  public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Golpe"))
        {
            RecibirDaño(5);
        }
    }*/

    void Patrulla()
{
    if (PuntosDePatrulla.Length == 0) return;

    Transform target = PuntosDePatrulla[puntoActual];
    Vector2 direction = (target.position - transform.position).normalized;

    if (isMoving)
    {
        rbEnemy.linearVelocity = direction * speed;
        RotarHacia(direction); // Mira hacia el punto de patrulla
    }

    if (direction.x > 0)
    {
        transform.localScale = new Vector2(1, transform.localScale.y);
    }
    else if (direction.x < 0)
    {
        transform.localScale = new Vector2(-1, transform.localScale.y);
    }

    if (Vector2.Distance(transform.position, target.position) <= 0.1f)
    {
        puntoActual++;
        contadorDescansos++;

        if (puntoActual >= PuntosDePatrulla.Length)
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
   bool PuedeVerJugador()
{
    Vector2 direccionJugador = jugador.position - transform.position;
    float distancia = direccionJugador.magnitude;

    if (distancia > rangoVision) return false;

    // Como el objeto rota, su "frente" siempre es transform.right
    Vector2 frente = transform.right; 

    float angulo = Vector2.Angle(frente, direccionJugador);

    // Si el jugador está fuera del cono de visión (ej. detrás del enemigo)
    if (angulo > anguloVision / 2f) return false;

    // Lanzamos el Raycast
    // Nota: Le sumamos un pequeño offset para que el rayo no choque con el propio enemigo
RaycastHit2D hit = Physics2D.Raycast(
    (Vector2)transform.position + (direccionJugador.normalized * 0.5f),
    direccionJugador.normalized,
    distancia);

    if (hit.collider != null)
{
    if (hit.collider.CompareTag("Jugador"))
    {
        return true;
    }
}

    return false;
}


void RotarHacia(Vector2 direccion)
{
    if (direccion != Vector2.zero)
    {
        // Calcula el ángulo para que el eje X (derecha) apunte a la dirección
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        
        // Aplicamos la rotación al Rigidbody para que las físicas sean estables
        rbEnemy.rotation = angulo; 
    }
}

void OnDrawGizmosSelected()
{
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, rangoVision);

    // El frente ahora es la rotación real del objeto
    Vector3 frente = transform.right; 

    // Los cálculos de los bordes del cono de visión
    Vector3 limiteIzquierdo = Quaternion.Euler(0, 0, anguloVision / 2) * frente;
    Vector3 limiteDerecho = Quaternion.Euler(0, 0, -anguloVision / 2) * frente;

    Gizmos.color = Color.red;
    Gizmos.DrawLine(transform.position, transform.position + limiteIzquierdo * rangoVision);
    Gizmos.DrawLine(transform.position, transform.position + limiteDerecho * rangoVision);
}
   

}
