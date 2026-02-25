
using UnityEngine;
using UnityEngine.InputSystem;

public class warrior : MonoBehaviour
{

    public Animator Walk;
    public PlayerInput PInput; 
    public Rigidbody2D RBPlayer;

    public float speed = 2;

    public Vector2 dir;
    public Vector2 input;

    public bool LookRight;

    public bool canJump;

    public float JumpForce = 5;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {

         input = PInput.actions["Caminata"].ReadValue<Vector2>() * speed;
       dir.x = input.x * speed;
        dir.y = input.y * JumpForce;

       RBPlayer.linearVelocity = new Vector2(dir.x, RBPlayer.linearVelocity.y);    


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



   

        if (RBPlayer.linearVelocity.x != 0)
        {
            Walk.SetBool("Is Mooving", true);
        }
        else
        {
            Walk.SetBool("Is Mooving",false);
        }

        if(PInput.actions["Salto"].WasPressedThisFrame() && canJump)
        {
            RBPlayer.linearVelocity = new Vector2(RBPlayer.linearVelocity.x,JumpForce);
            canJump = false;
            Walk.SetBool("IsJumping", true);
        }



/*
        if (Input.GetKeyDown(KeyCode.D))
        {
            Walk.SetBool("Is Mooving", true);

            
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Walk.SetBool("Is Mooving", false);

        }
*/

         if (Input.GetKeyDown(KeyCode.F))
        {
            Walk.SetBool("Ataque", true);
        }
    }

    public void finishiAttack()
    {
        Walk.SetBool("Ataque", false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("suelo")){
        canJump = true;
         Walk.SetBool("IsJumping", false);
        }
    }


}
