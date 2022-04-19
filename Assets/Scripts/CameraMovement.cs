using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Outer Properties -----------------------------------------------------------------------------
    public Camera[] camAry;
    public PlayerMovement playerMovement;
    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float smoothness = 10f;

    // Outer Functions ------------------------------------------------------------------------------
    public void ChangeCamTo(int num)
    {
        camAry[curCamNum].enabled = false;
        curCamNum = num;
        camAry[curCamNum].enabled = true;


        if (num == 0)//1 ��Ī
        {
            //Cursor.lockState = CursorLockMode.Locked;
            gun.SetActive(true);
            //GameManager.Instance.Player.Obj.SetActive(false);
            //Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            gun.SetActive(false);
            //GameManager.Instance.Player.Obj.SetActive(true);
            Cursor.visible = true;
        }
    }

    public Camera GetCurrentCamera()
    {
        return camAry[curCamNum];
    }

    // Properties : caching -------------------------------------------------------------------------

    // Inner Properties -----------------------------------------------------------------------------
    [SerializeField] private float finalDistance;//cam �� player ������ �Ÿ�
    [SerializeField] private float sensitivity = 200f;//����
    private int curCamNum = 1;
    private int thirdViewNum = 1;
    private float followSpeed = 10f;
    private float clampAngle = 70f; //���� ���� ����

    private float rotX;//mouse input���� ����
    private float rotY;//mouse input���� ����
    // Inner Properties -----------------------------------------------------------------------------
    GameObject gun;

    // Inner Functions ------------------------------------------------------------------------------

    // Unity Inspectors -----------------------------------------------------------------------------
    public Transform objectToFollow;
    [SerializeField] private float minDistance;//ī�޶�� player ������ �Ÿ�
    [SerializeField] private float maxDistance;//ī�޶�� player ������ �Ÿ�

    // Unity Messages -------------------------------------------------------------------------------
    private void Awake()
    {
        gun = camAry[0].transform.GetChild(0).gameObject;

        gun.SetActive(false);
    }

    void Start()
    {
        rotX = transform.eulerAngles.x;
        rotY = transform.eulerAngles.y;

        /*3��Ī cam���� �� ���� �ʱ�ȭ*/
        dirNormalized = camAry[thirdViewNum].transform.localPosition.normalized; //localPosition: ���� ������Ʈ�� ���������� �ڽ� ī�޶��� position
        finalDistance = camAry[thirdViewNum].transform.localPosition.magnitude;

        //���콺 Ŀ�� ���ֱ�
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if (playerMovement.canMove)
        {
            //ī�޶� y�� ȸ���ô� ȭ�� �¿�� �̵��ϱ⶧���� x�� yinput �ݴ��,
            rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;//-�Ȱ��ϸ� ���콺 ���οø� �� �Ʒ� ��
            rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

            //�ּ�,�ִ� �����ȿ��� ������ 
            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
            transform.rotation = rot;
        }
    }

    //update()�����ڿ� ����
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed);

        if (playerMovement.canMove && curCamNum == 1)
        {
            finalDir = transform.TransformPoint(dirNormalized * maxDistance);//TransformPoint:������ǥ�� �۷ι��� ��ȯ

            //��ֹ��� ī�޶� ���� �� ī�޶� �ű��
            RaycastHit hit;
            //�ε����ٸ�
            if (Physics.Linecast(transform.position, finalDir, out hit) && (hit.collider.gameObject.tag != "Boundary"))
            {
                finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            }//�ƴ϶��
            else
            {
                finalDistance = maxDistance;
            }
            camAry[curCamNum].transform.localPosition = Vector3.Lerp(camAry[curCamNum].transform.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
        }
    }
}
