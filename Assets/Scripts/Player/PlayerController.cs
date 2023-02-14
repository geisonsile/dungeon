using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

    // Public Properties
    public float movementSpeed = 10;
    public float jumpPower = 10;
    public float jumpMovementFactor = 1f;

    // State Machine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState;
    [HideInInspector] public Jump jumpState;
    [HideInInspector] public Dead deadState;

    // Internal Properties
    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public bool hasJumpInput;

    

    [HideInInspector] public Rigidbody thisRigidbody;
    [HideInInspector] public Collider thisCollider;
    [HideInInspector] public Animator thisAnimator;

    //Slope
    [Header("Slope")]
    public float maxSplopeAngle = 45;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isOnSlope;
    [HideInInspector] public Vector3 slopeNormal;
    


    void Awake() 
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisCollider = GetComponent<Collider>();
        thisAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() 
    {
        // StateMachine and its states
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        walkingState = new Walking(this);
        jumpState = new Jump(this);
        deadState = new Dead(this);
        stateMachine.ChangeState(idleState);
    }

    // Update is called once per frame
    void Update() 
    {
        // Create input vector
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        float inputY = isUp ? 1 : isDown ? -1 : 0;
        movementVector = new Vector2(inputX, inputY);
        hasJumpInput = Input.GetKey(KeyCode.Space);

        // Update Animator
        float velocity = thisRigidbody.velocity.magnitude;
        float velocityRate = velocity / movementSpeed;
        thisAnimator.SetFloat("fVelocity", velocityRate);


        // Physic Updates
        DetectGround();
        DetectSlope();

        // StateMachine
        stateMachine.Update();
    }

    void LateUpdate() 
    {
        // StateMachine
        stateMachine.LateUpdate();
    }

    void FixedUpdate() 
    {
        //Apply gravity
        Vector3 gravityForce = Physics.gravity * (isOnSlope ? 0.25f : 1f);
        thisRigidbody.AddForce(gravityForce, ForceMode.Acceleration);

        // StateMachine
        stateMachine.FixedUpdate();
    }

    public Quaternion GetForward() 
    {
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        return Quaternion.Euler(0, eulerY, 0);
    }

    public void RotateBodyToFaceInput() 
    {
        if(movementVector.IsZero()) return;

        // Calculate rotation
        Camera camera = Camera.main;
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up);
        Quaternion q2 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Quaternion toRotation = q1 * q2;
        Quaternion newRotation = Quaternion.LerpUnclamped(transform.rotation, toRotation, 0.15f);

        // Apply rotation
        thisRigidbody.MoveRotation(newRotation);
    }

    private void DetectGround() 
    {
        // Reset flag
        isGrounded = false;

        // Detect ground
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        float maxDistance = 0.1f;
        LayerMask groundLayer = GameManager.Instance.groundLayer;
        
        if(Physics.Raycast(origin, direction, maxDistance, groundLayer))
        {
            isGrounded = true;
        } 
    }

    private void DetectSlope()
    {
        // Reset flag
        isOnSlope = false;
        slopeNormal = Vector3.zero;

        // Detect ground
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        float maxDistance = 0.2f;
        
        if (Physics.Raycast(origin, direction, out var slopeHitInfo, maxDistance))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHitInfo.normal);
            isOnSlope = angle < maxSplopeAngle && angle != 0;
            slopeNormal = isOnSlope ? slopeHitInfo.normal : Vector3.zero;
        }
    }
}
