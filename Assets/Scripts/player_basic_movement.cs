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
    private InputActionAsset InputAction;
    private InputAction Movement;
    private InputAction Jump;
    private float move;
    private bool jumpress;
    private bool jumprelease;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
       Movement = InputSystem.actions.FindAction("Horizontal");
       Jump = InputSystem.actions.FindAction("Jump");
    }
    // Update is called once per frame
    void Update()
    {
        move=Movement.ReadValue<float>();
        jumpress = Jump.triggered;
        jumprelease = Jump.WasReleasedThisFrame();
        Jumping();
        Move();
    }

    public void Move()
    {
        rb.linearVelocity = new Vector2(move*base_speed, rb.linearVelocity.y);
    }
    public void Jumping()
    {
        if (jumpress && Groundcheck())
        {
            rb.AddForce(Vector2.up * base_jumpforce, ForceMode2D.Impulse);
        }
        if (jumprelease && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
        }
    }
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


