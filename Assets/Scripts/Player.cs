using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Camera cam;
    Animator anim;
    Rigidbody rigid;
    GameObject nearObj;//상호작용 가능한 object
    PlayerMovement playerMovement;

    [Header("Player Power")]
    public float maxHealth = 100;
    public int maxSword = 10;
    public int maxShield = 10;

    [Header("Player Info")]
    public int swordNum = -1; //무기 없는상태(-1)에서 시작
    public float coin = 0;

    public float curHealth = 100;
    public float curSwordPower = 5;
    public float curShieldPower = 2;

    [SerializeField]
    bool isDead = false;


    /*other input*/
    [SerializeField]
    bool eDown;
    [SerializeField]
    bool leftMouseDown;
    [SerializeField]
    bool rightouseDown;

    /*Weapons*/
    public GameObject[] swordAry;

    /*UI*/
    public RectTransform healthBar;
    public RectTransform swordBar;
    public RectTransform shieldBar;
    public Text coinText;

    void Awake()
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

    void Update()
    {
        GetInput();

        Interaction();
        Attack();
    }

    private void LateUpdate()
    {
        UpdateBarUI();
    }

    void GetInput()
    {
        eDown = Input.GetKeyDown("e");// KeyCode.E);
        leftMouseDown = Input.GetButton("Fire1"); //마우스 왼쪽
        //rightMouseDown = Input.GetButtonDown("Fire2");//마우스 우클릭
    }


    void Interaction()
    {
        if (eDown && nearObj)
        {
            if (nearObj.tag == "Shop")
            {
                Shop shop = nearObj.GetComponent<Shop>();
                shop.Enter(this);
            }
        }
    }

    void Attack()
    {
        if (leftMouseDown)
        {
            anim.SetTrigger("doSwing");
        }
    }

    void UpdateBarUI()
    {
        healthBar.localScale = new Vector3((float)curHealth / maxHealth, 1, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Coin") && (!isDead))
        {
            Debug.Log("getcoin()");
            Coin coinScript = other.GetComponent<Coin>();
            ChangeCoin(coinScript.value);
            Destroy(other.gameObject);
        }
    }

    public void ResumePlayer()
    {
        playerMovement.canMove = true;
    }

    public void StopPlayer()
    {
        playerMovement.canMove = false;
    }

    private void OnTriggerStay(Collider other)
    {
        nearObj = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
        {
            Shop shop = nearObj.GetComponent<Shop>();
            shop.Exit();
            nearObj = null;
        }
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

        healthBar.localScale = new Vector3((float)curHealth / maxHealth, 1, 1);
    }

    public void ChangeSword()
    {
        //무기 바꿔들기
        if (swordNum >= 0)//이전에 들고있던 무기가 있다면 없애기
        {
            Debug.Log("swordNum="+ swordNum);
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
}
