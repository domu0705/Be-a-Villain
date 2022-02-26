using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	// Properties -----------------------------------------------------------------------------------
	[Header("Player MaxPower")]
	[SerializeField] private float maxHealth = 100;
	[SerializeField] private float maxSword = 10;
	[SerializeField] private float maxShield = 10;
	[Header("Player Info")]
	[SerializeField] private int coin = 0;
	[SerializeField] private float curHealth = 100;
	[SerializeField] private float curSwordPower = 5;
	[SerializeField] private float curShieldPower = 2;

	public PlayerMovement Movement => playerMovement;
	public bool IsPowerFull => curHealth >= maxHealth;
	public bool IsShieldFull => curShieldPower >= maxShield;
	public float Coin => coin;
	public int SwordNum => swordNum;

	// Methods --------------------------------------------------------------------------------------
	public void ResumePlayer()
	{
		playerMovement.canMove = true;
	}

	public void StopPlayer()
	{
		playerMovement.canMove = false;
	}

	public void ChangeCoin(int newCoin)
	{
		coin += newCoin;
		HudUI.Instance.UpdateCoin(coin);
	}

	public void ChangeHealth(int val)
	{
		curHealth += val;
		if (curHealth > maxHealth)
			curHealth = maxHealth;

		HudUI.Instance.UpdateHealth(curHealth , maxHealth);
	}

	public void ChangeSword()
	{
		//무기 바꿔들기
		if (swordNum >= 0)//이전에 들고있던 무기가 있다면 없애기
		{
			swordAry[swordNum].SetActive(false);
		}
		swordNum++;
		swordAry[swordNum].SetActive(true);

		//공격력 증가
		Item newSword = swordAry[swordNum].GetComponent<Item>();
		curSwordPower = newSword.value;
		HudUI.Instance.UpdateSword(curSwordPower, maxSword);
	}

	public void ChangeShield(int val)
	{
		curShieldPower += val;
		if (curShieldPower > maxShield)
			curShieldPower = maxShield;
		HudUI.Instance.UpdateShield(curShieldPower, maxShield);
	}


	// Events ---------------------------------------------------------------------------------------
	public event Action<Player, ObjectData> OnInteraction;



	// Fields : caching -----------------------------------------------------------------------------
	private Animator anim;
	private Rigidbody rigid;
	private PlayerMovement playerMovement;

	// Functions ------------------------------------------------------------------------------------
	private void interaction()
	{
		if (eDown && nearObj)
		{
			if (nearObj.tag == "Shop")
			{
				// 가능하면 변수선언은 'var'를 사용하시면 좋아요 by veramocor
				var shop = nearObj.GetComponent<Shop>();
				shop.Enter(this);
			}

			else if (nearObj.tag == "Object") //상호작용 가능한 사물 or NPC
			{
				/*이벤트를 보내고, GameManager가 이벤트에 반응*/
				var objScript = nearObj.GetComponent<ObjectData>();
				OnInteraction?.Invoke(this, objScript); //event뒤에 .Invoke() 함수는 OnInteraction에 등록된 모든 콜백함수를 실행시킴.
			}
		}
	}

	private void attack()
	{
		if (leftMouseDown && playerMovement.canMove)
		{
			anim.SetTrigger("doSwing");
		}
	}

	private void getInput()
	{
		eDown = Input.GetKeyDown("e");// KeyCode.E);
		leftMouseDown = Input.GetButton("Fire1"); //마우스 왼쪽
												  //rightMouseDown = Input.GetButtonDown("Fire2");//마우스 우클릭
	}

	// Unity Inspectors -----------------------------------------------------------------------------
	[SerializeField]
	private GameObject nearObj;//상호작용 가능한 object
	[SerializeField] private int swordNum = -1; //무기 없는상태(-1)에서 시작
	[SerializeField] private bool isDead = false;
	[Header("Other Input")]
	[SerializeField] private bool eDown;
	[SerializeField] private bool leftMouseDown;
	[SerializeField] private bool rightouseDown;
	[Header("Weapons")]
	[SerializeField] private GameObject[] swordAry;


	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		anim = GetComponentInChildren<Animator>();
		rigid = GetComponent<Rigidbody>();
		playerMovement = GetComponent<PlayerMovement>();

		/*Initialize HUD UI*/
		HudUI.Instance.UpdateCoin(coin);
		HudUI.Instance.UpdateHealth(curHealth, maxHealth);
		HudUI.Instance.UpdateSword(curSwordPower,maxSword);
		HudUI.Instance.UpdateShield(curShieldPower,maxShield);
	}

	private void Start()
	{
		/*player info UI 세팅*/
		//ChangeCoin(0);
		ChangeHealth(0);
		//swordBar.localScale = new Vector3((float)curSwordPower / maxSword, 1, 1);
		//shieldBar.localScale = new Vector3((float)curShieldPower / maxShield, 1, 1);
	}

	private void Update()
	{
		getInput();

		interaction();
		attack();
	}

	private void LateUpdate()
	{

	}

	private void OnCollisionEnter(Collision collision)
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "Coin") && (!isDead))
		{
			Coin coinScript = other.GetComponent<Coin>();
			Debug.Log("coinScript.value =" + coinScript.value + " coin=" + coin);
			ChangeCoin(coinScript.value);
			Destroy(other.gameObject);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if ((other.tag == "Shop") || (other.tag == "Object"))
		{
			nearObj = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if ((nearObj.tag == "Shop") || (nearObj.tag == "Object"))
		{
			nearObj = null;
		}
	}
}


