using System.Collections;
using UnityEngine;
using UnityEngine.UI;


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

	// Inner Functions ------------------------------------------------------------------------------
	private void getDamage(int damage)
    {
		anim.SetTrigger("doAttacked");

		if(damage >= health)
        {
			StartCoroutine("die");
			die();
        }
		else
        {
			health -= damage;
        }
    } 


	IEnumerator die()
    {
		anim.SetTrigger("doDie");
		yield return new WaitForSeconds(1f);

		gameObject.SetActive(false);
    }

	// Coroutine ------------------------------------------------------------------------------------
	// Event Handlers -------------------------------------------------------------------------------
	// Overrides ------------------------------------------------------------------------------------



	// Unity Inspectors -----------------------------------------------------------------------------

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		anim = GetComponent<Animator>();
	}
	private void Start()
	{
		
	}

    //private void OnTriggerEnter(Collider other)

	private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Bullet")
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
		}
    }
}
