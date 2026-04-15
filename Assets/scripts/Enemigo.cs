using System.Collections;
using UnityEngine;
using UnityEngine.AI; //para el nav mesh

public class Enemigo : MonoBehaviour
{
    public NavMeshAgent agent;
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
    
    agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateRotation = false; // Evitamos que Unity lo rote en 3D
        agent.updateUpAxis = false;
    
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
            float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
            if (distanciaJugador >= rangoPerdida)
            {
                persiguiendo = false;
                agent.ResetPath(); // Limpia la ruta al perderlo
            }
        }

        if (persiguiendo)
        {
            // --- 4. MOVIMIENTO CON NAVMESH ---
            agent.SetDestination(jugador.position);
            
            // Calculamos la dirección para la rotación visual
            Vector2 direccion = (jugador.position - transform.position).normalized;
            RotarHacia(direccion);
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
            agent.enabled = false;
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
        if (PuntosDePatrulla.Length == 0 || !isMoving) return;

        Transform target = PuntosDePatrulla[puntoActual];
        
        // --- 5. PATRULLA CON NAVMESH ---
        agent.SetDestination(target.position);

        // Rotación visual basada en la velocidad actual del agente
        if (agent.velocity.magnitude > 0.1f)
        {
            RotarHacia(agent.velocity.normalized);
        }

        // Detectar si llegó al punto
        if (!agent.pathPending && agent.remainingDistance <= 0.2f)
        {
            puntoActual = (puntoActual + 1) % PuntosDePatrulla.Length;
            contadorDescansos++;

            if (contadorDescansos >= 3)
            {
                StartCoroutine(Espera());
            }
        }
    }


        IEnumerator Espera()
    {
        isMoving = false;
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);

        contadorDescansos = 0;
        isMoving = true;
        agent.isStopped = false;
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
