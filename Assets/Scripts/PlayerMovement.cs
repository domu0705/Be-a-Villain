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

        anim.SetBool("isWalking", false); // �̰� ���ϸ� idle�ƴٰ� �ٷ� �ٽ� ����
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
    private bool inBoundary;//�� �ձ� ����

    // Camera
    private bool lockCameraRot;
    private float smoothness = 10f;

    // Inner Functions ------------------------------------------------------------------------------
    private void Move()
    {
        Vector3 foward = transform.forward;//transform.forward�� ������ǥ�� �÷��̾��� �� ���͸� ��ȯ
        Vector3 right = transform.right;

        moveVec = (foward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal")).normalized;//ī�޶��� ������ ���� ���� ��ǥ���� ���� ������ �����̱�

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
            anim.SetBool("isJumping", true);//animation�� �ٷ� ������ ���� ���� ����.
            anim.SetTrigger("doJump");//SetBool("isJumping", true); �� anim�� �����ϸ�, ���� ���ϸ��̼��� �������� ��� ���۵�. �׷��� trigger�� anim�� �ѹ��� ����
            onGround = false;
        }
    }

    private void TurnSetting()
    {
        if (Input.GetKey(KeyCode.LeftAlt))//ĳ���� �����ϰ� �ѷ�����
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

    private void FreezeRotation()//�ֱ������� ȸ���ӵ��� 0���� ����� �ڵ����� ���ư��� ���� ����.
    {
        rigid.angularVelocity = Vector3.zero; //angularVelocity �� ���� ȸ�� �ӵ��� �ǹ�

        /*���� floor,player,ź�Ǹ� ���� �ٸ� ���̾�� �������ְ�
         * edit>project setting> physics �� �� �Ʒ����� ��� ���̾ ��� ���̾ �浹�ϴ����� ��������,
         * �̹� �ǽ������� bulletCase�� floor���� �浹�ϰ� üũ�Ͽ�, case�� player���� �浹�Ͽ� ȸ���� ����� �ϴ� ���� ����*/
    }

    private void DetectBoundary()
    {
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.green);//������ġ,��¹���*ray����, ray��
        inBoundary = Physics.Raycast(transform.position, transform.forward, rayLength, LayerMask.GetMask("Boundary"));//raycast �� ray�� ��� ��� ������Ʈ�� �����ϴ� �Լ���
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
