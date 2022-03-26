using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Transform objectToFollow;
    public float followSpeed = 10f;
    public float sensitivity = 100f;//감도
    public float clampAngle = 70f; //상하 각도 제한

    float rotX;//mouse input받을 변수
    float rotY;//mouse input받을 변수

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;//카메라와 player 사이의 거리
    public float maxDistance;//카메라와 player 사이의 거리
    public float finalDistance;
    public float smoothness = 10f;
    
    void Start()
    {
        rotX = transform.eulerAngles.x;
        rotY = transform.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized; //localPosition: 현재 오브젝트를 기준점으로 자식 카메라의 position
        finalDistance = realCamera.localPosition.magnitude;

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
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);//TransformPoint:로컬좌표를 글로벌로 변환

        if (playerMovement.canMove && playerMovement.curCamNum == 1){
            //장애물에 카메라 닿을 때 카메라 옮기기
            RaycastHit hit;
            //부딛혔다면
            if (Physics.Linecast(transform.position, finalDir, out hit))
            {
                finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            }//아니라면
            else
            {
                finalDistance = maxDistance;
            }
        }

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
    }
}
