using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;


public class player_basic_movement : MonoBehaviour
{
    //variables
    public float base_speed = 2;
    public float base_jumpforce = 10;
    public Transform groundcheck;
    public LayerMask groundchecklayer;
    private Rigidbody2D rb; 
    //input Variables 
    // To modify the keybinds go to edit>proyect settings>Input System Package 
    private InputActionAsset InputAction;
    private InputAction Movement;   //left and right arrow keys
    private InputAction Jump;   //Z key
    private float move;
    private bool jumpress;
    private bool jumprelease;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Getting the components and keybinds into the variables
        rb = GetComponent<Rigidbody2D>(); 
        Movement = InputSystem.actions.FindAction("Horizontal");
        Jump = InputSystem.actions.FindAction("Jump");
    }
    // Update is called once per frame
    void Update()
    {
        //making the inputs into actual readable variables
        move=Movement.ReadValue<float>();
        jumpress = Jump.triggered;
        jumprelease = Jump.WasReleasedThisFrame();
        //functions, we shoud make state machines
        Jumping();
        Move();
    }
    //moves when the keybinds ate trigered
    public void Move()
    {
        rb.linearVelocity = new Vector2(move*base_speed, rb.linearVelocity.y);
    }
    //jumps when the keybind is trigerred
    public void Jumping()
    {
        //jumping
        if (jumpress && Groundcheck())
        {
            rb.AddForce(Vector2.up * base_jumpforce, ForceMode2D.Impulse);
        }
        //variable jumping
        if (jumprelease && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
        }
    }
    //Checks if in ground, returns true or false
    private bool Groundcheck()
    {
        if (Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundchecklayer))
        {
            Debug.Log("Grounded");
            return true;
        }
        return false;
    }

}


