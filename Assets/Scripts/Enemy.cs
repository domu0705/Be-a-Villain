using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	// Definitions ----------------------------------------------------------------------------------
	// Outer Properties -----------------------------------------------------------------------------
	// Outer Functions ------------------------------------------------------------------------------
	// Events ---------------------------------------------------------------------------------------



	// Properties : caching -------------------------------------------------------------------------
	private Animator anim;

	// Inner Properties -----------------------------------------------------------------------------
	[SerializeField] private int health;
	private NavMeshAgent nav;
	private Rigidbody rigid;

	// Inner Functions ------------------------------------------------------------------------------
	private void getDamage(int damage)
    {

		if(damage >= health)
        {
			StartCoroutine("die");
			die();
        }
		else
        {
			anim.SetTrigger("doAttacked");
			health -= damage;
        }
    } 

	private void deleteExternalForce()//외부의 물리적 힘에 의해 AI의 이동이 망가지지 않도록 외부 힘 제거.
    {
		rigid.velocity = Vector3.zero;
		rigid.angularVelocity = Vector3.zero;
	}

	// Coroutine ------------------------------------------------------------------------------------
	IEnumerator die()
	{
		anim.SetTrigger("doDie");
		yield return new WaitForSeconds(1f);

		gameObject.SetActive(false);
	}

	// Event Handlers -------------------------------------------------------------------------------
	// Overrides ------------------------------------------------------------------------------------



	// Unity Inspectors -----------------------------------------------------------------------------

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();
		nav = GetComponent<NavMeshAgent>();
	}
	private void Start()
	{
		
	}

    private void Update()
    {
		nav.SetDestination(GameManager.Instance.Player.transform.position);
    }

    private void FixedUpdate()
    {
		deleteExternalForce();

	}

    //private void OnTriggerEnter(Collider other)

    private void OnTriggerEnter(Collider other)
	{
        /*if(other.tag == "Bullet")
        {
			Debug.Log("총맞음");
			Bullet bullet = other.GetComponent<Bullet>();
			getDamage(bullet.damage);


		}
		else if(other.tag == "Damager")
        {
			Debug.Log("공격당함");
			Item weapon = other.GetComponent<Item>();
			getDamage(weapon.damage);
		}*/
    }

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.tag == "Bullet")
		{
			Debug.Log("총맞음");
			Bullet bullet = collision.gameObject.GetComponent<Bullet>();
			getDamage(bullet.damage);


		}
		else if (collision.gameObject.tag == "Damager")
		{
			Debug.Log("공격당함");
			Item weapon = collision.gameObject.GetComponent<Item>();
			getDamage(weapon.damage);
		}
	}
}
