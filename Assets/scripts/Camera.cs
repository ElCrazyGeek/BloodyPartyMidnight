using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform jugador;
    public float distanciaMaxima = 3f;
    public float suavizado = 8f;

    void Update()
{
    Vector3 objetivo = jugador.position;

    if (Input.GetKey(KeyCode.LeftShift))
    {
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;

        Vector3 mouseMundo = Camera.main.ScreenToWorldPoint(mousePos);
        mouseMundo.z = 0f;

        Vector3 direccion = mouseMundo - jugador.position;
        
        Vector3 offset = Vector3.ClampMagnitude(direccion * 0.5f, distanciaMaxima);

        objetivo = jugador.position + offset;
    }

    transform.position = Vector3.Lerp(transform.position, objetivo, suavizado * Time.deltaTime);
}
}