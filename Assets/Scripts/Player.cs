using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //This script is from tutorial! https://www.youtube.com/watch?v=f473C43s8nE

    #region Global Variables
    [SerializeField] private HUD hud;
    private PlayerAttributes attributes;
    private Rigidbody rb;
    private Transform orientation;
    private Gun gun;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float groundDrag;

    [Header("Sprint")]
    [SerializeField] private float sprintSpeed;
    private bool sprint = false;

    [Header("Ground Check")]
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float jumpCooldown;
    private bool readyToJump = true;
    [SerializeField] private bool dive = false; //diving / falling faster


    private float horizontalInput;
    private float verticalInput;

    private Vector3 direction;
    #endregion

    private void Awake()
    {
        attributes = new PlayerAttributes(hud); //object that holds health + score of player (can be expanded with stamina etc...)
        rb = GetComponent<Rigidbody>();
        orientation = transform.GetChild(1).GetComponent<Transform>();
        gun = transform.GetChild(3).GetChild(0).GetComponent<Gun>();
    }

    private void Start()
    {
        attributes.UpdateHUD(); //Must be updated manually because in constructor there is no reference yet (unspecified execution order of Awake methods)
        rb.freezeRotation = true;
    }

    void Update()
    {
        Grounding();    //Is player standing on ground?
        MyInput();    //Movement input
        LimitSpeed();   // Goes over speed limit?
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void MyInput()
    {
        dive = false;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && grounded && readyToJump)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);  //Can hold Space to jump automatically once player hits ground :)
        }
        else if (Input.GetKey(KeyCode.Mouse1) && !grounded)
        {
            dive = true;    //faster falling
        }


        sprint = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            gun.Shoot();
    }

    public PlayerAttributes GetPlayerAttributes()
    {
        return attributes;
    }

    #region Movement Logic
    private void Grounding()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, this.transform.localScale.y + .2f, whatIsGround);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void Movement()
    {
        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;

        float maxSpeed = GetMaxSpeed();

        if (grounded)
        {
            rb.AddForce(direction.normalized * maxSpeed * 10f, ForceMode.Force);

            return; //Jump out of this function, we are on the ground don't need to do any other actions here.
        }

        rb.AddForce(direction.normalized * speed * 10f * airMultiplier, ForceMode.Force);   //If player is in the air he should move slower horizontaly

        if (dive)
            rb.AddForce(-transform.up * 15, ForceMode.Force);
    }

    private void LimitSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        float maxSpeed = GetMaxSpeed();

        //Is velocity bigger than speed set in inspector? limit it!
        if (flatVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * speed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private float GetMaxSpeed()
    {
        //Chooses if max speed is only speed or speed + sprint speed.
        float maxSpeed = speed;
        if (sprint)
            maxSpeed += sprintSpeed;

        return maxSpeed;
    }
    #endregion

    #region Jump Logic
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    #endregion
}