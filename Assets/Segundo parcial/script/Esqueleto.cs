using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Transform rightPoint;
    public Transform leftPoint;
    public Transform objetoRaro;

    public Rigidbody2D rbEnemy;
    public float speed;
    public bool isRight;

    private Vector2 leftDir;
    private Vector2 rightDir;

    void Start()
    {
      

        leftDir = leftPoint.position;
        rightDir = rightPoint.position;

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
        }else
        {
            rbEnemy.linearVelocity = new Vector2(-speed, rbEnemy.linearVelocity.y);
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
    }
}