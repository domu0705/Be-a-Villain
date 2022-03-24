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
		bulletRigid.velocity = initialPos.transform.forward * 50;//�Ѿ��� �������ϴϱ� �ӵ��� �ٿ���
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

    private void OnCollisionEnter(Collision collision)//ź�ǰ� �ٴڰ� �浹�� ����
    {
        if(collision.gameObject.tag == "Ground")
        {
			Destroy(gameObject,3);
        }
    }

	private void OnTriggerEnter(Collider other)//�Ѿ��� ���� �浹�� ����
	{
		if (other.gameObject.tag == "Boundary")
		{
			Destroy(gameObject);
		}
	}


}
