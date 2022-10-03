using System;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed = 8;

    public float jumpForce = 10;
    public float gravity = -20;
  

    [SerializeField] private GroundChecker _groundChecker;
//    public bool ableToMakeADoubleJump = true;

    public Animator animator;
    public Transform model;


    private bool _isActive = true;

    public void Enable(bool on)
    {
        if (!on)
        { _isActive = false;
            direction = Vector3.zero;
          controller.Move(direction);
           

        }
        else
        {
            _isActive = true;
        }
    }




    void Update()
    {
        if (!_isActive)
        {
            return;
        }

        if (PlayerManager.gameOver)
        {
            //play death animation
            animator.SetTrigger("die");

            //disable the script
            this.enabled = false;
        }

        //Take the horizontal input to move the player
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;
        animator.SetFloat("speed", Mathf.Abs(hInput));

        //Check if the player is on the ground
        bool isGrounded = _groundChecker.IsGraunded;
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            direction.y = -1;
          //  ableToMakeADoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

             
        }
        else
        {
            direction.y += gravity * Time.deltaTime;//Add Gravity
           
            /*if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            {
                DoubleJump();
            }*/
        }

        

        //Flip the player
        if(hInput != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(hInput, 0, 0));
            model.rotation = newRotation;
        }

        //Move the player using the character controller
        controller.Move(direction * Time.deltaTime);

        //Reset Z Position
        if (transform.position.z != 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        //win level
        if (PlayerManager.winLevel)
        {
            animator.SetTrigger("win");
            this.enabled = false;
        }
        
        animator.SetFloat("VerticalVelocity", controller.velocity.y);
    }

    /*private void DoubleJump()
    {
        //Double Jump
        animator.SetTrigger("doubleJump");
        direction.y = jumpForce;
        ableToMakeADoubleJump = false;
    }*/
    private void Jump()
    {
        //Jump
        direction.y = jumpForce;
    }
}
