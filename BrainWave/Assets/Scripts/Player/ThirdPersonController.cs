using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class ThirdPersonController : MonoBehaviour {

    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;
    public AnimationClip runAnimation;
    public AnimationClip jumpPoseAnimation;

    public float walkMaxAnimationSpeed = 0.75f;
    public float trotMaxAnimationSpeed = 1.0f;
    public float runMaxAnimationSpeed = 1.0f;
    public float jumpAnimationSpeed = 1.15f;
    public float landAnimationSpeed = 1.0f;

    private Animation _animation;

    enum CharacterState {
        Idle = 0,
        Walking = 1,
        Trotting = 2,
        Running = 3,
        Jumping = 4,
    }

    private CharacterState _characterState;

    // The speed when walking
    public float walkSpeed = 1.0f;
    // after trotAfterSeconds of walking we trot with trotSpeed
    public float trotSpeed = 4.0f;
    // when pressing "Fire3" button (cmd) we start running
    public float runSpeed = 1.0f;

    public float inAirControlAcceleration = 3.0f;

    // How high do we jump when pressing jump and letting go immediately
    public float jumpHeight = 0.5f;

    // The gravity for the character
    public float gravity = 20.0f;
    // The gravity in controlled descent mode
    public float speedSmoothing = 10.0f;
    public float rotateSpeed = 500.0f;
    public float trotAfterSeconds = 3.0f;

    public bool canJump = true;

    private float jumpRepeatTime = 0.05f;
    private float jumpTimeout = 0.15f;
    private float groundedTimeout = 0.25f;

    // The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
    private float lockCameraTimer = 0.0f;

    // The current move direction in x-z
    private Vector3 moveDirection = Vector3.zero;
    // The current vertical speed
    private float verticalSpeed = 0.0f;
    // The current x-z move speed
    private float moveSpeed = 0.0f;

    // The last collision flags returned from controller.Move
    private CollisionFlags collisionFlags;

    // Are we jumping? (Initiated with jump button and not grounded yet)
    private bool jumping = false;
    private bool jumpingReachedApex = false;

    // Are we moving backwards (This locks the camera to not do a 180 degree spin)
    private bool movingBack = false;
    // Is the user pressing any keys?
    private bool isMoving = false;
    // When did the user start walking (Used for going into trot after a while)
    private float walkTimeStart = 0.0f;
    // Last time the jump button was clicked down
    private float lastJumpButtonTime = -10.0f;
    // Last time we performed a jump
    private float lastJumpTime = -1.0f;

    // the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
    private float lastJumpStartHeight = 0.0f;

    private Vector3 inAirVelocity = Vector3.zero;

    private float lastGroundedTime = 0.0f;

    private bool isControllable = true;

    void Awake() {
        moveDirection = transform.TransformDirection(Vector3.forward);

        _animation = GetComponent<Animation>();
        if(!_animation)

        if(!idleAnimation) {
            _animation = null;
            Debug.Log("No idle animation found. Turning off animations.");
        }
        if(!walkAnimation) {
            _animation = null;
            Debug.Log("No walk animation found. Turning off animations.");
        }
        if(!runAnimation) {
            _animation = null;
            Debug.Log("No run animation found. Turning off animations.");
        }
        if(!jumpPoseAnimation && canJump) {
            _animation = null;
            Debug.Log("No jump animation found and the character has canJump enabled. Turning off animations.");
        }

    }

    void UpdateSmoothedMovementDirection() {
        Transform cameraTransform = Camera.main.transform;
        bool grounded = IsGrounded();

        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if(v < -0.2f)
            movingBack = true;
        else
            movingBack = false;

        bool wasMoving = isMoving;
        isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;

        Vector3 targetDirection = h * right + v * forward;

        if(grounded) {
            lockCameraTimer += Time.deltaTime;
            if(isMoving != wasMoving)
                lockCameraTimer = 0.0f;

            if(targetDirection != Vector3.zero) {
                if(moveSpeed < walkSpeed * 0.9f && grounded) {
                    moveDirection = targetDirection.normalized;
                }
                else {
                    moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);

                    moveDirection = moveDirection.normalized;
                }
            }

            float curSmooth = speedSmoothing * Time.deltaTime;

            float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);

            _characterState = CharacterState.Idle;

            if(Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift)) {
                targetSpeed *= runSpeed;
                _characterState = CharacterState.Running;
            }
            else if(Time.time - trotAfterSeconds > walkTimeStart) {
                targetSpeed *= trotSpeed;
                _characterState = CharacterState.Trotting;
            }
            else {
                targetSpeed *= walkSpeed;
                _characterState = CharacterState.Walking;
            }

            moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);

            if(moveSpeed < walkSpeed * 0.3f)
                walkTimeStart = Time.time;
        }
        else {
            if(jumping)
                lockCameraTimer = 0.0f;

            if(isMoving)
                inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;
        }

    }

    void ApplyJumping() {
        if(lastJumpTime + jumpRepeatTime > Time.time)
            return;

        if(IsGrounded()) {
            if(canJump && Time.time < lastJumpButtonTime + jumpTimeout) {
                verticalSpeed = CalculateJumpVerticalSpeed(jumpHeight);
                SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void ApplyGravity() {
        if(isControllable) {
            bool jumpButton = Input.GetButton("Jump");

            if(jumping && !jumpingReachedApex && verticalSpeed <= 0.0f) {
                jumpingReachedApex = true;
                SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
            }

            if(IsGrounded())
                verticalSpeed = 0.0f;
            else
                verticalSpeed -= gravity * Time.deltaTime;
        }
    }

    float CalculateJumpVerticalSpeed(float targetJumpHeight) {
        return Mathf.Sqrt(2 * targetJumpHeight * gravity);
    }

    void DidJump() {
        jumping = true;
        jumpingReachedApex = false;
        lastJumpTime = Time.time;
        lastJumpStartHeight = transform.position.y;
        lastJumpButtonTime = -10;

        _characterState = CharacterState.Jumping;
    }

    void Update() {
        if(!isControllable) {
            Input.ResetInputAxes();
        }
        if(Input.GetButtonDown("Jump")) {
            lastJumpButtonTime = Time.time;
        }
        UpdateSmoothedMovementDirection();
        ApplyGravity();
        ApplyJumping();
        Vector3 movement = moveDirection * moveSpeed + new Vector3(0, verticalSpeed, 0) + inAirVelocity;
        movement *= Time.deltaTime;
        CharacterController controller = GetComponent<CharacterController>();
        collisionFlags = controller.Move(movement);

        if(_animation) {
            if(_characterState == CharacterState.Jumping) {
                if(!jumpingReachedApex) {
                    _animation[jumpPoseAnimation.name].speed = jumpAnimationSpeed;
                    _animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
                    _animation.CrossFade(jumpPoseAnimation.name);
                }
                else {
                    _animation[jumpPoseAnimation.name].speed = -landAnimationSpeed;
                    _animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
                    _animation.CrossFade(jumpPoseAnimation.name);
                }
            }
            else {
                if(controller.velocity.sqrMagnitude < 0.1f) {
                    _animation.CrossFade(idleAnimation.name);
                }
                else {
                    if(_characterState == CharacterState.Running) {
                        _animation[runAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0f, runMaxAnimationSpeed);
                        _animation.CrossFade(runAnimation.name);
                    }
                    else if(_characterState == CharacterState.Trotting) {
                        _animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0f, trotMaxAnimationSpeed);
                        _animation.CrossFade(walkAnimation.name);
                    }
                    else if(_characterState == CharacterState.Walking) {
                        _animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0f, walkMaxAnimationSpeed);
                        _animation.CrossFade(walkAnimation.name);
                    }

                }
            }
        }
        if(IsGrounded()) {

            transform.rotation = Quaternion.LookRotation(moveDirection);

        }
        else {
            Vector3 xzMove = movement;
            xzMove.y = 0;
            if(xzMove.sqrMagnitude > 0.001f) {
                transform.rotation = Quaternion.LookRotation(xzMove);
            }
        }
        if(IsGrounded()) {
            lastGroundedTime = Time.time;
            inAirVelocity = Vector3.zero;
            if(jumping) {
                jumping = false;
                SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.moveDirection.y > 0.01f)
            return;
    }
    float GetSpeed() {
        return moveSpeed;
    }
    public bool IsJumping() {
        return jumping;
    }
    bool IsGrounded() {
        return ( collisionFlags & CollisionFlags.CollidedBelow ) != 0;
    }
    Vector3 GetDirection() {
        return moveDirection;
    }
    public bool IsMovingBackwards() {
        return movingBack;
    }
    public float GetLockCameraTimer() {
        return lockCameraTimer;
    }
    bool IsMoving() {
        return Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f;
    }
    bool HasJumpReachedApex() {
        return jumpingReachedApex;
    }
    bool IsGroundedWithTimeout() {
        return lastGroundedTime + groundedTimeout > Time.time;
    }
    void Reset() {
        gameObject.tag = "Player";
    }
}