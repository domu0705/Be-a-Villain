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

	// prefabs
	[SerializeField] private GameObject bulletPrefab;


	//pool index
	private int bulletIndex;

	// Inner Functions ------------------------------------------------------------------------------
	private void generatePool()
	{
		bullets = new GameObject[100];
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
		GameObject bullet;
		Bullet bulletScript;
		if (bulletIndex >= bullets.Length)
		{
			bulletIndex = 0;
		}
		bullet = bullets[bulletIndex];
		bulletScript = bullet.GetComponent<Bullet>();
		bulletIndex++;

		return bulletScript;
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
	}
	private void Start()
	{

	}
}
