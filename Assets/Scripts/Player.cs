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


    [Header("Player Power")]
    [SerializeField]
    float maxHealth = 100;
    [SerializeField]
    float maxSword = 10;
    [SerializeField]
    float maxShield = 10;

    [Header("Player Info")]
    [SerializeField]
    float curHealth = 100;
    [SerializeField]
    float curSword = 5;
    [SerializeField]
    float curShield = 2;

    Camera camera;
    Animator anim;
    Rigidbody rigid;
    CharacterController controller;
    GameObject nearObj;//상호작용 가능한 object

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

    /*other input*/
    [SerializeField]bool eDown;


    /*UI*/
    public RectTransform healthBar;
    public RectTransform swordBar;
    public RectTransform shieldBar;

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
            Interaction();
            //attack();
        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical"); // horizontal, vertical 은 edit>project settings에서 변경
        spacebarPressed = Input.GetKeyDown(KeyCode.Space);

        eDown = Input.GetKeyDown("e");// KeyCode.E);

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
            anim.SetBool("isJumping", true);//animation이 바로 착지로 가는 것을 막음.
            anim.SetTrigger("doJump");//SetBool("isJumping", true); 로 anim을 시작하면, 점프 에니메이션이 무한으로 계속 시작됨. 그래서 trigger로 anim을 한번만 실행
            onGround = false;
        }
    }

    void Interaction() 
    {
        if (eDown && nearObj)
        {
            Debug.Log("Interaction()");
            if(nearObj.tag == "Shop")
            {
                Debug.Log("in shop");
                Shop shop = nearObj.GetComponent<Shop>();
                shop.Enter();
            }
        }
    
    
    }

    void UpdateBarUI()
    {
        Debug.Log("UpdateBarUI()");
        healthBar.localScale = new Vector3((float)curHealth / maxHealth, 1, 1);
        swordBar.localScale = new Vector3((float)curSword / maxSword, 1, 1);
        shieldBar.localScale = new Vector3((float)curShield / maxShield, 1, 1);
    }

    private void LateUpdate()
    {
        if (toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }

        UpdateBarUI();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isJumping", false);
            onGround = true;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        nearObj = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
        {
            Shop shop = nearObj.GetComponent<Shop>();
            shop.Exit();
            nearObj = null;
        }
    }


}
