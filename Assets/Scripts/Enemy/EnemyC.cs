using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyC : Enemy
{
	// ----------------------------------------------------------------------------------------------

	// FSM ------------------------------------------------------------------------------------------

	// Definitions ----------------------------------------------------------------------------------
	// Outer Properties -----------------------------------------------------------------------------
	// Outer Functions ------------------------------------------------------------------------------
	// Events ---------------------------------------------------------------------------------------



	// Properties : caching -------------------------------------------------------------------------
	// Inner Properties -----------------------------------------------------------------------------
	// Inner Functions ------------------------------------------------------------------------------
	// Coroutine ------------------------------------------------------------------------------------
	// Event Handlers -------------------------------------------------------------------------------
	// Overrides ------------------------------------------------------------------------------------
	protected override IEnumerator attack()
	{
		Debug.Log("attack C ()");
		isChasing = false;
		isAttacking = true;
		anim.SetBool("isWalking", false);
		anim.SetTrigger("doAttack1");
		yield return new WaitForSeconds(0.4f);

		attackArea.SetActive(true);
		while (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)//ANIM 끝날때 까지 기다리기
		{
			yield return new WaitForEndOfFrame();
		}
		attackArea.SetActive(false);
		yield return new WaitForSeconds(attackDelay);

		anim.SetBool("isWalking", true);
		isChasing = true;
		isAttacking = false;
	}


	// Unity Inspectors -----------------------------------------------------------------------------

	// Unity Messages -------------------------------------------------------------------------------

}
