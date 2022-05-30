using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class EnemyB : Enemy
{
	// Definitions ----------------------------------------------------------------------------------
	// Outer Properties -----------------------------------------------------------------------------
	// Outer Functions ------------------------------------------------------------------------------
	// Events ---------------------------------------------------------------------------------------



	// Properties : caching -------------------------------------------------------------------------
	// Inner Properties -----------------------------------------------------------------------------
	[SerializeField] private int bulletNum; //한 번에 발사되는 총알 갯수
	[SerializeField] private float bulletDelay;

	// Inner Functions ------------------------------------------------------------------------------
	// Coroutine ------------------------------------------------------------------------------------
	// Event Handlers -------------------------------------------------------------------------------
	// Overrides ------------------------------------------------------------------------------------
	protected override IEnumerator attack()
    {
		isChasing = false;
		isAttacking = true;
		anim.SetTrigger("doAttack1");
		yield return new WaitForSeconds(0.4f);

		for(int i = 0; i< bulletNum; i++)
        {
			Bullet enemyBullet = ObjectManager.Instance.GetEnemyBullet();
			enemyBullet.ShootBulletFrom(gun.transform.position, gun.transform);
			yield return new WaitForSeconds(bulletDelay);
		}

		while (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)//ANIM 끝날때 까지 기다리기
		{
			yield return new WaitForEndOfFrame();
		}
		//attackArea.SetActive(false);
		isChasing = true;
		yield return new WaitForSeconds(attackDelay);

		isAttacking = false;
	}


	// Unity Inspectors -----------------------------------------------------------------------------
	[SerializeField] private GameObject gun;

	// Unity Messages -------------------------------------------------------------------------------

}
