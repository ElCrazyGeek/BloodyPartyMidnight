using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class warrior : MonoBehaviour
{

    public const string PlayerIdle  = "Idle"; 
    public const string Playerwalk = "Correr";
    public const string Playerjump = "Salto"; 
    public const string PlayerAttack = "Atacar";
    public const string Playerdead = "MUERTE";
    private string currentState;

    public Animator aniPlayer;
    public PlayerInput PInput; 
    public Rigidbody2D RBPlayer;

    public float speed = 2;
    public float JumpForce = 5;

    public Vector2 dir;
    public Vector2 input;

    public bool LookRight;
    public bool canJump;

    public bool isMoving;
    public bool isIdle;
    public bool isGround;
    public bool isAttacking;
    public bool IsJumping;

    public Vector2 checkGround;

    public Transform Checador_de_Piso;
    public float groundDistance;
    public LayerMask groundMask;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {

        isGround = Physics2D.Raycast(Checador_de_Piso.position, Vector2.down, groundDistance, groundMask);
        
        
        if(!isAttacking){

         input = PInput.actions["Caminata"].ReadValue<Vector2>() * speed;
       dir.x = input.x * speed;
        dir.y = input.y * JumpForce;
        RBPlayer.linearVelocity = new Vector2(dir.x, RBPlayer.linearVelocity.y);
        }
      
        
        if(RBPlayer.linearVelocity.x != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }



        if (RBPlayer.linearVelocity.x > 0)
        {
            LookRight = true;
        }
        else if(RBPlayer.linearVelocity.x < 0)
        {
            LookRight = false;
        }


        if (LookRight)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
         else{
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

        if(PInput.actions["Salto"].WasPressedThisFrame() && isGround)
        {
            RBPlayer.linearVelocity = new Vector2(RBPlayer.linearVelocity.x,JumpForce);
            IsJumping = true;
        }

         if (isGround)
        {
           
            if(PInput.actions["Ataque"].WasPressedThisFrame() && !isAttacking)
            {
                isAttacking = true;
                RBPlayer.linearVelocity = Vector2.zero;
                IsJumping = false;
            }
        }
        Animation();
    }

    public void Animation()
    {
        if(isGround){

       if (isMoving)
            {
                ChangeAnimation(Playerwalk);
            }
            else if (isAttacking)
            {
                ChangeAnimation(PlayerAttack);
            }
            
            else
            {
                ChangeAnimation(PlayerIdle);
            }
            
        }


        else{

        if (IsJumping)
        {
            ChangeAnimation(Playerjump);
        }
        }
    }


    public void ChangeAnimation(string newState)
    {
        if(newState == currentState) return;
        currentState = newState;
        aniPlayer.Play(currentState);
    }

    public void FinishAttack()
    {
        isAttacking = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(Checador_de_Piso.position, Vector2.down * groundDistance);
    }
} 



