using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float speed;
    [SerializeField]
    bool canMove;
    [SerializeField]
    float jumpPower;

    float hAxis;
    float vAxis;
    bool spacebarPressed;
    bool onGround;
    Vector3 moveVec;

    //animation
    bool isWalking;

    //
    Camera camera;
    Animator anim;
    Rigidbody rigid;
    CharacterController controller;
    public bool toggleCameraRotation;
    public float smoothness = 10f;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        camera = Camera.main;

        canMove = true;
        onGround = true;
    }
    void Update()
    {
        if (canMove)
        {
            GetInput();

            Move();
            Jump();
            Turn();
            //turn();
            //attack();
        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical"); // horizontal, vertical 은 edit>project settings에서 변경
        spacebarPressed = Input.GetKeyDown(KeyCode.Space);

    }

    void Turn()
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
            Debug.Log("jump()");
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJumping", true);//�ٷ� ���� anim���� �Ѿ�� �ʱ� ����
            anim.SetTrigger("doJump"); // setbool�� �ϸ� ��� anystate���� isjumping�� true ���� jumping anim�� ���� �����
            onGround = false;
        }
    }

    private void LateUpdate()
    {
        if (toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")//���� ����
        {
            anim.SetBool("isJumping", false); // Land ���ϸ��̼� ���� �ϱ�
            onGround = true;

        }
    }
}
