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


        if (num == 0)//1 인칭
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
    [SerializeField] private float finalDistance;//cam 과 player 사이의 거리
    [SerializeField] private float sensitivity = 200f;//감도
    private int curCamNum = 1;
    private int thirdViewNum = 1;
    private float followSpeed = 10f;
    private float clampAngle = 70f; //상하 각도 제한

    private float rotX;//mouse input받을 변수
    private float rotY;//mouse input받을 변수
    // Inner Properties -----------------------------------------------------------------------------
    GameObject gun;

    // Inner Functions ------------------------------------------------------------------------------

    // Unity Inspectors -----------------------------------------------------------------------------
    public Transform objectToFollow;
    [SerializeField] private float minDistance;//카메라와 player 사이의 거리
    [SerializeField] private float maxDistance;//카메라와 player 사이의 거리

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

        /*3인칭 cam에서 쓸 변수 초기화*/
        dirNormalized = camAry[thirdViewNum].transform.localPosition.normalized; //localPosition: 현재 오브젝트를 기준점으로 자식 카메라의 position
        finalDistance = camAry[thirdViewNum].transform.localPosition.magnitude;

        //마우스 커서 없애기
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if (playerMovement.canMove)
        {
            //카메라 y축 회전시는 화면 좌우로 이동하기때문에 x와 yinput 반대로,
            rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;//-안곱하면 마우스 위로올릴 때 아래 봄
            rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

            //최소,최대 각도안에서 움직임 
            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
            transform.rotation = rot;
        }
    }

    //update()끝난뒤에 실행
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed);

        if (playerMovement.canMove && curCamNum == 1)
        {
            finalDir = transform.TransformPoint(dirNormalized * maxDistance);//TransformPoint:로컬좌표를 글로벌로 변환

            //장애물에 카메라 닿을 때 카메라 옮기기
            RaycastHit hit;
            //부딛혔다면
            if (Physics.Linecast(transform.position, finalDir, out hit) && (hit.collider.gameObject.tag != "Boundary"))
            {
                finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            }//아니라면
            else
            {
                finalDistance = maxDistance;
            }
            camAry[curCamNum].transform.localPosition = Vector3.Lerp(camAry[curCamNum].transform.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
        }
    }
}
