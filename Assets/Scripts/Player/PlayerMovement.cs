using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public enum State
    {
        Idle,
        Walking,
        Running,
        Jumping
    }

    public State state;

    public static PlayerMovement Instance;

    public bool ableToMove = true;

    [Header("Movement")]
    public float moveSpeed;
    public float increasedSpeed;
    private float defaultSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround; 
    public LayerMask altWhatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        defaultSpeed = moveSpeed;

        ableToMove = true;

        state = State.Idle;

    }

    private void Update()
    {
        // ground check
        grounded = IsGrounded();

        MyInput();
        SpeedControl();
        ChangeSpeed();

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

    }

    private void FixedUpdate()
    {

        if (!ableToMove)
            return;

        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            if (horizontalInput == 0 && verticalInput == 0)
            {
                state = State.Idle;
            }
            else if (IsShiftPressed())
            {
                state = State.Running;
            }
            else
            {
                state = State.Walking;
            }
            

        }

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        state = State.Jumping;
    }
    private void ResetJump()
    {
        readyToJump = true;

    }

    private void ChangeSpeed()
    {

        if(IsShiftPressed())
        {
            moveSpeed = increasedSpeed;
        }
        else
        {
            moveSpeed = defaultSpeed;
        }

    }

    private bool IsShiftPressed() => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);


    public bool IsGrounded() => Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround) ||
            Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, altWhatIsGround);

 

}