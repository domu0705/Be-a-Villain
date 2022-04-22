using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ObjectManager : Singleton<ObjectManager>
{
	// Definitions ----------------------------------------------------------------------------------
	// 모든 오브젝트 풀링을 관리

	// Outer Properties -----------------------------------------------------------------------------

	// Outer Functions ------------------------------------------------------------------------------
	// Events ---------------------------------------------------------------------------------------



	// Properties : caching -------------------------------------------------------------------------
	// Inner Properties -----------------------------------------------------------------------------
	//prefab pool
	private GameObject[] bullets;
	private GameObject[] enemyBullets;

	// prefabs
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private GameObject enemyBulletPrefab;


	//pool index
	private int bulletIndex;
	private int enemyBulletIndex;

	// Inner Functions ------------------------------------------------------------------------------
	private void generatePool()
	{
		bullets = new GameObject[100];
		enemyBullets = new GameObject[100];
	}

	private void makePrefab(GameObject prefab, GameObject[] pool)
    {
		for(int i = 0; i < pool.Length; i++)
        {
			pool[i] = Instantiate(prefab);
			pool[i].SetActive(false);
        }
    }

	public Bullet GetBullet()
	{
		Bullet bulletScript;
		if (bulletIndex >= bullets.Length)
		{
			bulletIndex = 0;
		}
		bulletScript = bullets[bulletIndex].GetComponent<Bullet>();
		bulletIndex++;

		return bulletScript;
	}

	public Bullet GetEnemyBullet()
	{
		Bullet enemyBulletScript;
		if (enemyBulletIndex >= enemyBullets.Length)
		{
			enemyBulletIndex = 0;
		}
		enemyBulletScript = enemyBullets[enemyBulletIndex].GetComponent<Bullet>();
		enemyBulletIndex++;

		return enemyBulletScript;
	}

	// Coroutine ------------------------------------------------------------------------------------
	// Event Handlers -------------------------------------------------------------------------------
	// Overrides ------------------------------------------------------------------------------------



	// Unity Inspectors -----------------------------------------------------------------------------


	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		generatePool();

		makePrefab(bulletPrefab, bullets);
		makePrefab(enemyBulletPrefab, enemyBullets);
	}
	private void Start()
	{

	}
}
