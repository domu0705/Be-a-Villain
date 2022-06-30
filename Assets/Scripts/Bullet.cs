using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Bullet : MonoBehaviour
{
	// Outer Properties -----------------------------------------------------------------------------
	public int damage;
	public enum Owner{ Player, Enemy};

	// Outer Functions ------------------------------------------------------------------------------
	public void ShootBulletFrom(Vector3 startPos,Transform direction)
	{
		gameObject.transform.position = startPos;
		gameObject.SetActive(true);

		//Vector3 shootDir = 
		Rigidbody bulletRigid = gameObject.GetComponent<Rigidbody>();
		bulletRigid.velocity = direction.forward * bulletSpeed;//총알이 나가야하니까 속도를 붙여줌
	}


	// Events ---------------------------------------------------------------------------------------



	// Properties : caching -------------------------------------------------------------------------
	private ObjectManager objectManager;
	private GameObject[] bullets;
	private CameraMovement cameraMovement;
	private Vector3 screenCenter;

	// Inner Properties -----------------------------------------------------------------------------
	[SerializeField] private int bulletSpeed = 200;
	[SerializeField] private Owner owner;

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

	private void OnTriggerEnter(Collider other)//총알이 벽과 충돌시 다시 오브젝트 풀로 이동
	{
		if ( owner == Owner.Enemy && other.gameObject.tag != "Enemy")
		{
			gameObject.SetActive(false);
		}
        else if(owner == Owner.Player)
		{
			gameObject.SetActive(false);
		}
	}
}
