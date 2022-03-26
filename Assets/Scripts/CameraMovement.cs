using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Transform objectToFollow;
    public float followSpeed = 10f;
    public float sensitivity = 100f;//����
    public float clampAngle = 70f; //���� ���� ����

    float rotX;//mouse input���� ����
    float rotY;//mouse input���� ����

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;//ī�޶�� player ������ �Ÿ�
    public float maxDistance;//ī�޶�� player ������ �Ÿ�
    public float finalDistance;
    public float smoothness = 10f;
    
    void Start()
    {
        rotX = transform.eulerAngles.x;
        rotY = transform.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized; //localPosition: ���� ������Ʈ�� ���������� �ڽ� ī�޶��� position
        finalDistance = realCamera.localPosition.magnitude;

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
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);//TransformPoint:������ǥ�� �۷ι��� ��ȯ

        if (playerMovement.canMove && playerMovement.curCamNum == 1){
            //��ֹ��� ī�޶� ���� �� ī�޶� �ű��
            RaycastHit hit;
            //�ε����ٸ�
            if (Physics.Linecast(transform.position, finalDir, out hit))
            {
                finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            }//�ƴ϶��
            else
            {
                finalDistance = maxDistance;
            }
        }

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
    }
}
