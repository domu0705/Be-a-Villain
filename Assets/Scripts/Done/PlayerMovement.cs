using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public bool canMove = true;
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpPower;


    Camera cam;
    Animator anim;
    Rigidbody rigid;

    /*player movement*/
    float hAxis;
    float vAxis;
    bool spacebarPressed;
    bool onGround;
    bool isWalking;
    Vector3 moveVec;

    /*camera*/
    public bool toggleCameraRotation;
    public float smoothness = 10f;


    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        cam = Camera.main;

        onGround = true;
    }

    void Update()
    {
        if (canMove)
        {
            GetInput();

            Move();
            Jump();
            TurnSetting();
        }
    }

    private void LateUpdate()
    {
        if (canMove)
        { 
            Turn();
        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical"); // horizontal, vertical 은 edit>project settings에서 변경
        spacebarPressed = Input.GetKeyDown(KeyCode.Space);
    }


    void Move()
    {
        Vector3 foward = transform.TransformDirection(Vector3.forward);//로컬의 foward를 클로벌 벡터로 반환
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 localMoveVec = (foward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal")).normalized;//카메라의 시점에 따라 로컬 좌표에서 앞인 곳으로 움직이기
        
        transform.position += localMoveVec * speed * Time.deltaTime;

        //animation
        isWalking = (localMoveVec != Vector3.zero);
        anim.SetBool("isWalking", isWalking);
    }

    void Jump()
    {
        if (spacebarPressed && onGround)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJumping", true);//animation이 바로 착지로 가는 것을 막음.
            anim.SetTrigger("doJump");//SetBool("isJumping", true); 로 anim을 시작하면, 점프 에니메이션이 무한으로 계속 시작됨. 그래서 trigger로 anim을 한번만 실행
            onGround = false;
        }
    }

    void TurnSetting()
    {
        if (Input.GetKey(KeyCode.LeftAlt))//캐릭터 고정하고 둘러보기
        {
            toggleCameraRotation = true;
        }
        else
        {
            toggleCameraRotation = false;
        }
    }

    void Turn()
    {
        if (toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }

    public void Stop()
    {
        canMove = false;

        anim.SetBool("isWalking", false); // 이거 안하면 idle됐다가 바로 다시 걸음
        anim.SetTrigger("doIdle");
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
