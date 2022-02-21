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
        vAxis = Input.GetAxisRaw("Vertical"); // horizontal, vertical �� edit>project settings���� ����
        spacebarPressed = Input.GetKeyDown(KeyCode.Space);
    }


    void Move()
    {
        Vector3 foward = transform.TransformDirection(Vector3.forward);//������ foward�� Ŭ�ι� ���ͷ� ��ȯ
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 localMoveVec = (foward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal")).normalized;//ī�޶��� ������ ���� ���� ��ǥ���� ���� ������ �����̱�
        
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
            anim.SetBool("isJumping", true);//animation�� �ٷ� ������ ���� ���� ����.
            anim.SetTrigger("doJump");//SetBool("isJumping", true); �� anim�� �����ϸ�, ���� ���ϸ��̼��� �������� ��� ���۵�. �׷��� trigger�� anim�� �ѹ��� ����
            onGround = false;
        }
    }

    void TurnSetting()
    {
        if (Input.GetKey(KeyCode.LeftAlt))//ĳ���� �����ϰ� �ѷ�����
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

        anim.SetBool("isWalking", false); // �̰� ���ϸ� idle�ƴٰ� �ٷ� �ٽ� ����
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
