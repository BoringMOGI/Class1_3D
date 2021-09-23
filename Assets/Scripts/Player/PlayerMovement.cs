using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;                   // 이동 속도. (거리)
    [SerializeField] float jumpHeight;                  // 점프 높이.

    [Range(1.0f, 3.0f)]
    [SerializeField] float gravityScale;                // 중력 가속도의 비율.

    [SerializeField] Transform groundChecker;           // 지면 체크 위치.
    [SerializeField] float groundRadius;                // 지면 체크 반지름.
    [SerializeField] LayerMask groundMask;              // 지면 마스크.

    [SerializeField] Animator anim;                     // 애니메이션.
    [SerializeField] Footstep footstep;                 // 발소리.
    

    CharacterController controller;                     // 캐릭터 컨트롤러.

    float gravity => -9.81f * gravityScale;
    Vector3 velocity;                                   // 하강 속도.
    bool isGrounded;                                    // 지면 여부.
    bool isAlive;                                       // 생존 여부.

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        isAlive = true;
    }

    private void Update()
    {
        CheckGround();

        #if UNITY_STANDALONE
        if (isAlive)
        {
            Movement();
            Jump();
        }
        #endif

        Gravity();
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundRadius, groundMask);
    }
    void Gravity()
    {
        // 중력.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void Movement()
    {
        bool isAccel = Input.GetKey(KeyCode.LeftShift);

        float x = Input.GetAxisRaw("Horizontal");  // 키보드 좌,우 키.
        float z = Input.GetAxisRaw("Vertical");    // 키보드 상,하 키.
        float accel = isAccel ? 1.5f : 1.0f;
        Vector3 direction = (transform.right * x) + (transform.forward * z);

        bool isWalk = direction != Vector3.zero && !isAccel;
        bool isRun = direction != Vector3.zero && isAccel;

        anim.SetBool("isWalk", isWalk);
        anim.SetBool("isRun", isRun);

        controller.Move(direction * moveSpeed * accel * Time.deltaTime);
        footstep.OnPlay(isWalk, isRun);
    }
    public void OnMoveJoystick(Vector2 movement)
    {
        bool isAccel = false;

        float x = movement.x;
        float z = movement.y;
        float accel = isAccel ? 1.5f : 1.0f;

        Vector3 direction = (transform.right * x) + (transform.forward * z);
        bool isWalk = direction != Vector3.zero && !isAccel;
        bool isRun = direction != Vector3.zero && isAccel;

        anim.SetBool("isWalk", isWalk);
        anim.SetBool("isRun", isRun);

        controller.Move(direction * moveSpeed * accel * Time.deltaTime);
        footstep.OnPlay(isWalk, isRun);
    }


    void Jump()
    {
        // 점프.
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    public void OnJumpJoystick()
    {
        // 점프.
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    

    public void OnDead()
    {
        isAlive = false;
    }
}
