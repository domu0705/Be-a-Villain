using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Bullet : MonoBehaviour
{
	// Outer Properties -----------------------------------------------------------------------------
	public int damage;

	// Outer Functions ------------------------------------------------------------------------------
	public void ShootBulletFrom(Transform initialPos)
	{
		gameObject.transform.position = initialPos.position;
		gameObject.SetActive(true);

		Rigidbody bulletRigid = gameObject.GetComponent<Rigidbody>();
		bulletRigid.velocity = initialPos.transform.forward * 50;//총알이 나가야하니까 속도를 붙여줌
	}


	// Events ---------------------------------------------------------------------------------------



	// Properties : caching -------------------------------------------------------------------------
	private ObjectManager objectManager;
	private GameObject[] bullets;
	// Inner Properties -----------------------------------------------------------------------------

	// Inner Functions ------------------------------------------------------------------------------

	// Coroutine ------------------------------------------------------------------------------------
	// Event Handlers -------------------------------------------------------------------------------
	// Overrides ------------------------------------------------------------------------------------


	// Unity Inspectors -----------------------------------------------------------------------------

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
	}

	private void Start()
	{
		
	}

    private void OnCollisionEnter(Collision collision)//탄피가 바닥과 충돌시 삭제
    {
        if(collision.gameObject.tag == "Ground")
        {
			Destroy(gameObject,3);
        }
    }

	private void OnTriggerEnter(Collider other)//총알이 벽과 충돌시 삭제
	{
		if (other.gameObject.tag == "Boundary")
		{
			Destroy(gameObject);
		}
	}


}
