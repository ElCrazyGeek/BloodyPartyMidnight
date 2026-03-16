using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public const string CaminataEsqueleto = "enemigo caminata";
     public const string MuerteEsqueleto = "Muerte ESQ";
    public Transform rightPoint;
    public Transform leftPoint;
  
    public Rigidbody2D rbEnemy;
    public float speed;
    public bool isRight;
    public bool vivo = true;

    public int vida = 20;
    public bool muerto;
    private string currentState;
    public Animator aniEsqueleto;

    private Vector2 leftDir;
    private Vector2 rightDir;
     public bool wasDamagedColor;
    public SpriteRenderer Sprite;
    public GameObject recompensa;
    public Transform spawnPoint;

    void Start()
    {
      

        leftDir.x = leftPoint.position.x;
        rightDir.x = rightPoint.position.x;

        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);

        rbEnemy = GetComponent<Rigidbody2D>();  
    }

  
    void Update()
    {

        if(transform.position.x >= rightDir.x)
        { 
            isRight = false;
        }
        else if(transform.position.x <= leftDir.x)
        {
            isRight = true;
        }         
    
        if (isRight)
        {
            rbEnemy.linearVelocity = new Vector2(speed, rbEnemy.linearVelocity.y) ;
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else
        {
            rbEnemy.linearVelocity = new Vector2(-speed, rbEnemy.linearVelocity.y);
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }        
    }
        IEnumerator Damage_Rotine()
    {
        wasDamagedColor = true;
        Sprite.color = Color.red;

        yield return new WaitForSeconds(0.15f); 
              wasDamagedColor = false;

        Sprite.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
{
    if (!vivo) return;

    if (collision.gameObject.CompareTag("sword"))
    {
        if (!wasDamagedColor)
        {
            StartCoroutine(Damage_Rotine());
        }

        vida -= 5;

        if (vida <= 0)
        {
            morir();
        }
    }
}

void morir()
{
    vivo = false;
    muerto = true;

    Instantiate(recompensa, spawnPoint.position, Quaternion.identity);

    speed = 0;
    muerte();
}


    void muerte()
    {
        ChangeAnimation(MuerteEsqueleto);
        Destroy(gameObject,1.5F);
        
    }
    

   public void ChangeAnimation(string newState)
    {
        if(newState == currentState) return;
        currentState = newState;
        aniEsqueleto.Play(currentState);
    }
}