using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Outer Properties -----------------------------------------------------------------------------
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
    public bool hasGun = true;

    // Outer Functions ------------------------------------------------------------------------------
    public void ResumePlayer()
    {
        playerMovement.canMove = true;
    }


    public void ChangeCoin(int newCoin)
    {
        coin += newCoin;
        hudUI.UpdateCoin(coin);
    }

    public void ChangeHealth(int val)
    {
        curHealth += val;
        if (curHealth > maxHealth)
            curHealth = maxHealth;

        hudUI.UpdateHealth(curHealth, maxHealth);
    }

    public void ChangeSword(int newSwordNum) //무기 바꿔들기
    {
        if (swordNum == 0) // 첫 검 구입이라면
        {
            inventoryUI.TurnOnSwordImg();
        }
        else if(equipWeapon && equipWeapon.type == Item.Type.Sword)// 무기를 들고있다면 object 업데이트 하기
        {

            swordAry[swordNum].SetActive(false);
            swordAry[newSwordNum].SetActive(true);
            
            Item weapon = swordAry[newSwordNum].GetComponent<Item>();
            equipWeapon = weapon;
        }

        swordNum = newSwordNum;
    }

    public void ChangeWeapon()
    {
        Item weapon;

        if (pressOne && swordNum > 0)
        {
            /*시점 변경*/
            //playerMovement.ChangeCamTo(1);
            OnCamChange?.Invoke(1);

            /*무기 변경*/
            ChangeSword(swordNum);
            swordAry[swordNum].SetActive(true);
            gun.SetActive(false);

            /*공격력 변경*/
            weapon = swordAry[swordNum].GetComponent<Item>();
            curSwordPower = weapon.damage;
            hudUI.UpdateSword(curSwordPower, maxSword);

            inventoryUI.CheckSwordUI();
            equipWeapon = weapon;
        }
        else if (pressTwo && hasGun)
        {
            /*시점 변경*/
            //playerMovement.ChangeCamTo(0);
            OnCamChange?.Invoke(0);

            /*무기 변경*/
            swordAry[swordNum].SetActive(false);
            gun.SetActive(true);

            /*공격력 변경*/
            weapon = gun.GetComponent<Item>();
            curSwordPower = weapon.damage;
            hudUI.UpdateSword(curSwordPower, maxSword);

            inventoryUI.CheckGunUI();
            equipWeapon = weapon;
        }
    }

    public void ChangeShield(int val)
    {
        curShieldPower += val;
        if (curShieldPower > maxShield)
            curShieldPower = maxShield;
        hudUI.UpdateShield(curShieldPower, maxShield);
    }


    // Events ---------------------------------------------------------------------------------------
    //public event Action<Player, ObjectData> OnInteraction;
    public Action<Player, ObjectData> OnInteraction;
    public Action<Player, Item> OnAttack;
    public Action<int> OnCamChange;


    // Fields : caching -----------------------------------------------------------------------------
    private Animator anim;
    private Rigidbody rigid;
    [SerializeField] private PlayerMovement playerMovement;

    // Inner Properties -----------------------------------------------------------------------------
    private InventoryUI inventoryUI;
    private HudUI hudUI;
    private float weaponDelay;
    private bool weaponDelayEnd;

    [Header("Curr State")]
    [SerializeField]private Item equipWeapon;
    [SerializeField] private GameObject nearObj;//상호작용 가능한 object
    [SerializeField] private int swordNum = 0; //무기 없는상태(0)에서 시작
    [SerializeField] private bool isDead = false;
    [Header("Other Input")]
    [SerializeField] private bool eDown;
    [SerializeField] private bool pressOne;
    [SerializeField] private bool pressTwo;
    [SerializeField] private bool leftMouseDown;
    [SerializeField] private bool rightouseDown;

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
            else if (nearObj.tag == "Teleporter") //상호작용 가능한 사물 or NPC
            {
                Debug.Log("Interaction | teleporter");
                /*이벤트를 보내고, GameManager가 이벤트에 반응*/
                var objScript = nearObj.GetComponent<Teleporter>();
                objScript.Teleport();
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
        if (equipWeapon == null)
            return;
        weaponDelay += Time.deltaTime;
        weaponDelayEnd = equipWeapon.delay < weaponDelay; // 공격을 이미 해서 공격 모션이 이루어지는 동안은 기다려야 함.

        if (leftMouseDown && playerMovement.canMove && weaponDelayEnd)
        {
            OnAttack?.Invoke(this, equipWeapon);
            anim.SetTrigger(equipWeapon.type == Item.Type.Sword ? "doSwing" : "doShot");
            weaponDelay = 0;
        }
    }

    private void getInput()
    {
        eDown = Input.GetKeyDown("e");// KeyCode.E);
        leftMouseDown = Input.GetButtonDown("Fire1"); //마우스 왼쪽
        pressOne = Input.GetButtonDown("Num1");// KeyCode.E);
        pressTwo = Input.GetButtonDown("Num2"); //마우스 왼쪽
                                                //rightMouseDown = Input.GetButtonDown("Fire2");//마우스 우클릭
    }

    // Unity Inspectors -----------------------------------------------------------------------------
    [Header("Weapons")]
    [SerializeField] private GameObject[] swordAry;
    [SerializeField] private GameObject gun;

    // Unity Messages -------------------------------------------------------------------------------
    private void Awake()
    {
        Debug.Log("player Awake()");
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();

        inventoryUI = GameManager.Instance.InventoryUI;
        hudUI =  GameManager.Instance.HudUI;

        /*Initialize HUD UI*/
        hudUI.UpdateCoin(coin);
        hudUI.UpdateHealth(curHealth, maxHealth);
        hudUI.UpdateSword(curSwordPower, maxSword);
        hudUI.UpdateShield(curShieldPower, maxShield);
    }

    private void Start()
    {
        ChangeHealth(0);
        swordNum = 0;
        ChangeSword(1);
        /*Initialize HUD UI*/
        hudUI.UpdateCoin(coin);
        hudUI.UpdateHealth(curHealth, maxHealth);
        hudUI.UpdateSword(curSwordPower, maxSword);
        hudUI.UpdateShield(curShieldPower, maxShield);
    }

    private void Update()
    {
        getInput();

        interaction();
        attack();
        ChangeWeapon();
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
            ChangeCoin(coinScript.value);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.tag == "Shop") || (other.tag == "Object") || (other.tag == "Teleporter"))
        {
            nearObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!nearObj) return;

        if ((nearObj.tag == "Shop") || (nearObj.tag == "Object") || (other.tag == "Teleporter"))
        {
            nearObj = null;
        }
    }
}


