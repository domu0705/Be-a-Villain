using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Bullet : MonoBehaviour
{
	// Outer Properties -----------------------------------------------------------------------------
	public int damage;

	// Outer Functions ------------------------------------------------------------------------------
	public void ShootBullet()
	{
		GameObject dest = bulletDestination();
		gameObject.transform.position = dest.transform.position + dest.transform.forward*2;
		gameObject.SetActive(true);

		//Vector3 shootDir = 
		Rigidbody bulletRigid = gameObject.GetComponent<Rigidbody>();
		bulletRigid.velocity = dest.transform.forward * bulletSpeed;//�Ѿ��� �������ϴϱ� �ӵ��� �ٿ���
	}


	// Events ---------------------------------------------------------------------------------------



	// Properties : caching -------------------------------------------------------------------------
	private ObjectManager objectManager;
	private GameObject[] bullets;
	private CameraMovement cameraMovement;
	private Vector3 screenCenter;

	// Inner Properties -----------------------------------------------------------------------------
	private int bulletSpeed = 200;

	// Inner Functions ------------------------------------------------------------------------------
	private GameObject bulletDestination()
    {
		Camera curCamera = cameraMovement.GetCurrentCamera();
		//screenCenter = new Vector3(curCamera.pixelWidth / 2, curCamera.pixelHeight / 2);
		return curCamera.gameObject;
	}

	// Coroutine ------------------------------------------------------------------------------------
	// Event Handlers -------------------------------------------------------------------------------
	// Overrides ------------------------------------------------------------------------------------


	// Unity Inspectors -----------------------------------------------------------------------------

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		cameraMovement = GameManager.Instance.CameraMovement;
	}

	private void Start()
	{
		
	}

	private void OnTriggerEnter(Collider other)//�Ѿ��� ���� �浹�� �ٽ� ������Ʈ Ǯ�� �̵�
	{
		if (other.gameObject.tag == "Boundary")
		{
			other.gameObject.SetActive(false);
		}

	}
}
