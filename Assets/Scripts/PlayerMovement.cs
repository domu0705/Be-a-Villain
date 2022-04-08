using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Outer Properties -----------------------------------------------------------------------------
    public bool canMove = true;

    // Outer Functions ------------------------------------------------------------------------------

    public void Stop()
    {
        Debug.Log("Stop()");

        canMove = false;

        anim.SetBool("isWalking", false); // 이거 안하면 idle됐다가 바로 다시 걸음
        anim.SetTrigger("doIdle");
    }

    public void Resume()
    {
        canMove = true;
    }

    // Inner Properties -----------------------------------------------------------------------------
    CameraMovement cameraMovement;
    Animator anim;
    Rigidbody rigid;

    // player movement
    private float hAxis;
    private float vAxis;
    private bool spacebarPressed;
    private bool onGround;
    private bool isWalking;
    private Vector3 moveVec;
    private bool inBoundary;//벽 뚫기 방지

    // Camera
    private bool lockCameraRot;
    private float smoothness = 10f;

    // Inner Functions ------------------------------------------------------------------------------
    private void Move()
    {
        Vector3 foward = transform.forward;//transform.forward는 로컬좌표로 플레이어의 앞 벡터를 반환
        Vector3 right = transform.right;

        moveVec = (foward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal")).normalized;//카메라의 시점에 따라 로컬 좌표에서 앞인 곳으로 움직이기

        if (canMove && !inBoundary)
        {
            rigid.MovePosition(transform.position + moveVec * speed * Time.deltaTime);
            
            //animation
            isWalking = (moveVec != Vector3.zero);
            anim.SetBool("isWalking", isWalking);
        }
    }

    private void Jump()
    {
        if (spacebarPressed && onGround)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJumping", true);//animation이 바로 착지로 가는 것을 막음.
            anim.SetTrigger("doJump");//SetBool("isJumping", true); 로 anim을 시작하면, 점프 에니메이션이 무한으로 계속 시작됨. 그래서 trigger로 anim을 한번만 실행
            onGround = false;
        }
    }

    private void TurnSetting()
    {
        if (Input.GetKey(KeyCode.LeftAlt))//캐릭터 고정하고 둘러보기
        {
            lockCameraRot = true;
        }
        else
        {
            lockCameraRot = false;
        }
    }

    private void Turn()
    {
        if (canMove && !lockCameraRot)
        {
            Camera camera = cameraMovement.GetCurrentCamera();
            Vector3 playerRotate = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }

    private void FreezeRotation()//주기적으로 회전속도를 0으로 만들어 자동으로 돌아가는 것을 방지.
    {
        rigid.angularVelocity = Vector3.zero; //angularVelocity 는 물리 회전 속도를 의미

        /*또한 floor,player,탄피를 각각 다른 레이어로 설정해주고
         * edit>project setting> physics 의 맨 아래에서 어느 레이어가 어느 레이어에 충돌하는지를 설정해줌,
         * 이번 실습에서는 bulletCase는 floor에만 충돌하게 체크하여, case가 player에게 충돌하여 회전이 생기게 하는 것을 막음*/
    }

    private void DetectBoundary()
    {
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.green);//이작위치,쏘는방향*ray길이, ray색
        inBoundary = Physics.Raycast(transform.position, transform.forward, rayLength, LayerMask.GetMask("Boundary"));//raycast 는 ray를 쏘아 닿는 오브젝트를 감지하는 함수임
    }

    private void StopToWall()
    {
        Debug.DrawRay(transform.position, moveVec * rayLength, Color.green);
        inBoundary = Physics.Raycast(transform.position, moveVec, rayLength, LayerMask.GetMask("Boundary"));
    }


    // Unity Inspectors -----------------------------------------------------------------------------
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float rayLength = 3;

    // Unity Messages -------------------------------------------------------------------------------
    void Awake()
    {
        cameraMovement = GameManager.Instance.CameraMovement;

        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();

        onGround = true;
    }

    void Update()
    {
        if (canMove)
        {
            GetInput();

            Jump();
            TurnSetting();
        }
    }

    private void FixedUpdate()
    {
        Move();
        FreezeRotation();
        StopToWall();
    }

    private void LateUpdate()
    {
        Turn();
    }

    private void GetInput()
    {
        spacebarPressed = Input.GetKeyDown(KeyCode.Space);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isJumping", false);
            onGround = true;
        }
    }
}
