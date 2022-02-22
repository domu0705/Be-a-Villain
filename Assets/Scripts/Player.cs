using BAV;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	// Properties -----------------------------------------------------------------------------------
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
	public void ChangeCoin(int val)
	{
		coin += val;
		coinText.text = "X " + coin;
	}
	public void ChangeHealth(int val)
	{
		curHealth += val;
		if (curHealth > maxHealth)
			curHealth = maxHealth;

		//HudUI.Instance.HealthBar.localScale = new Vector3((float)curHealth / maxHealth, 1, 1);
		//healthBar.localScale = new Vector3((float)curHealth / maxHealth, 1, 1);
	}
	public void ChangeSword()
	{
		//무기 바꿔들기
		if (swordNum >= 0)//이전에 들고있던 무기가 있다면 없애기
		{
			Debug.Log("swordNum=" + swordNum);
			swordAry[swordNum].SetActive(false);
		}
		swordNum++;
		swordAry[swordNum].SetActive(true);

		//공격력 증가
		Item newSword = swordAry[swordNum].GetComponent<Item>();
		curSwordPower = newSword.value;
		swordBar.localScale = new Vector3((float)curSwordPower / maxSword, 1, 1);
	}
	public void ChangeShield(int val)
	{
		curShieldPower += val;
		if (curShieldPower > maxShield)
			curShieldPower = maxShield;

		shieldBar.localScale = new Vector3((float)curShieldPower / maxShield, 1, 1);
	}

	// Events ---------------------------------------------------------------------------------------
	public event Action<Player, ObjectData> OnInteraction;



	// Fields : caching -----------------------------------------------------------------------------
	private Camera cam;
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
				// GameManager를 싱글톤으로 만들었기 때문에 다음과 같은 코드도 가능하지만, 
				// 가능하다면, 이벤트를 보내고, GameManager가 이벤트에 반응하는 식으로 하는것이 더 좋을 것 같아요.
				var objScript = nearObj.GetComponent<ObjectData>();
				OnInteraction?.Invoke(this, objScript);
				//GameManager.Instance.Action(objScript);
			}
		}
	}
	private void attack()
	{
		if (leftMouseDown)
		{
			anim.SetTrigger("doSwing");
		}
	}
	private void updateBarUI()
	{
		//HudUI.Instance.UpdateHealth(curHealth, maxHealth);
		healthBar.localScale = new Vector3((float)curHealth / maxHealth, 1, 1);
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
	[Header("Player Power")]
	[SerializeField] private float maxHealth = 100;
	[SerializeField] private int maxSword = 10;
	[SerializeField] private int maxShield = 10;
	[Header("Player Info")]
	[SerializeField] private int swordNum = -1; //무기 없는상태(-1)에서 시작
	[SerializeField] private float coin = 0;
	[SerializeField] private float curHealth = 100;
	[SerializeField] private float curSwordPower = 5;
	[SerializeField] private float curShieldPower = 2;
	[SerializeField] private bool isDead = false;
	[Header("Other Input")]
	[SerializeField] private bool eDown;
	[SerializeField] private bool leftMouseDown;
	[SerializeField] private bool rightouseDown;
	[Header("Weapons")]
	[SerializeField] private GameObject[] swordAry;
	[Header("UI")]
	[SerializeField] private RectTransform healthBar;
	[SerializeField] private RectTransform swordBar;
	[SerializeField] private RectTransform shieldBar;
	[SerializeField] private Text coinText;

	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		anim = GetComponentInChildren<Animator>();
		rigid = GetComponent<Rigidbody>();
		playerMovement = GetComponent<PlayerMovement>();

		cam = Camera.main;
	}
	private void Start()
	{
		/*player info UI 세팅*/
		ChangeCoin(0);
		ChangeHealth(0);
		swordBar.localScale = new Vector3((float)curSwordPower / maxSword, 1, 1);
		shieldBar.localScale = new Vector3((float)curShieldPower / maxShield, 1, 1);
	}
	private void Update()
	{
		getInput();

		interaction();
		attack();
	}
	private void LateUpdate()
	{
		updateBarUI();
	}
	private void OnCollisionEnter(Collision collision)
	{

	}
	private void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "Coin") && (!isDead))
		{
			Debug.Log("getcoin()");
			Coin coinScript = other.GetComponent<Coin>();
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


