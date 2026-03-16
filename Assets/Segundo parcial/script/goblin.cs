using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblin : MonoBehaviour
{
    public const string IdleGoblin = "IdleGoblin";
    public const string RunGoblin = "Goblin Corriendo";
    public const string MuerteGoblin = "Muerte Goblin";

    public Rigidbody2D rbEnemy;
    public Transform[] PatrolPoint;
    private int PuntoActual;
    public bool isMoving = true;
    public int contadorDescansos;  
    public float speed = 3f;

    public SpriteRenderer Sprite;
    private bool wasDamagedColor;
    public Animator aniGoblin;
    private string currentState;

    public bool vivo = true;
    public int vida = 10;
     private int puntoActual;

    void Start()
    {
        foreach(Transform p in PatrolPoint) { 
            p.parent = null; 
            }
    }

    void Update()
    {
    // 1. PRIORIDAD: MUERTE
    if (!vivo)
    {
        rbEnemy.linearVelocity = Vector2.zero; 
        ChangeAnimation(MuerteGoblin);
        return; 
    }

    Transform target = PatrolPoint[puntoActual];


    if (isMoving)
    {

        float dirX = (target.position.x - transform.position.x) > 0 ? 1 : -1;
        

        rbEnemy.linearVelocity = new Vector2(dirX * speed, rbEnemy.linearVelocity.y);
        
        ChangeAnimation(RunGoblin); 
        transform.localScale = new Vector2(dirX, 1);
    }
    else 
    {

        rbEnemy.linearVelocity = new Vector2(0, rbEnemy.linearVelocity.y);
        ChangeAnimation(IdleGoblin); 
    }


    float distanciaX = Mathf.Abs(target.position.x - transform.position.x);
    if (distanciaX <= 0.2f)
    {
        puntoActual++;
        contadorDescansos++;

        if (puntoActual >= PatrolPoint.Length)
        {
            puntoActual = 0;
        }

        if (contadorDescansos >= 4)
        {
            StartCoroutine(CooldownEspera());
        }
    }
    }
    
    IEnumerator CooldownEspera(){
        isMoving = false;
        yield return new WaitForSeconds(2f);
        contadorDescansos = 0;
        isMoving = true;   
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (vivo && collision.gameObject.CompareTag("sword"))
        {
            if (!wasDamagedColor) StartCoroutine(Damage_Rotine());
        }
            
            vida -= 5;
            if (vida <= 0)
            {
                Morir();
            }
    }

    void Morir()
    {
        vivo = false;
        if (GetComponent<Collider2D>()) GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1.5f);
    }

    IEnumerator Damage_Rotine()
    {
        wasDamagedColor = true;
        Sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f); 
        Sprite.color = Color.white;
        wasDamagedColor = false;
    }

    public void ChangeAnimation(string newState)
    {
        if (newState == currentState) return;
        currentState = newState;
        aniGoblin.Play(currentState);
    }
}