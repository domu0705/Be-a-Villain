using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HudUI : MonoBehaviour// gameobject로 있는건 singletone쓰지말자. 내가쓰는 singletone은 스크립트만 복제시킴. 대신 awake()에 dontdestroy쓰기
{
    // Outer Properties -----------------------------------------------------------------------------

    // Outer Functions ------------------------------------------------------------------------------
    public void UpdateCoin(int newCoin)
    {
        coinText.text = "X " + newCoin;
    }

    public void UpdateHealth(float curHealth, float maxHealth)
    {
        healthBar.localScale = new Vector3(curHealth / maxHealth, 1, 1);
    }

    public void UpdateSword(float curSwordPower, float maxSword)
    {
        swordBar.localScale = new Vector3(curSwordPower / maxSword, 1, 1);
    }

    public void UpdateShield(float curShieldPower, float maxShield)
    {
        shieldBar.localScale = new Vector3(curShieldPower / maxShield, 1, 1);
    }

    // Events ---------------------------------------------------------------------------------------



    // Fields : caching -----------------------------------------------------------------------------
    // Fields ---------------------------------------------------------------------------------------
    // Functions ------------------------------------------------------------------------------------
    // Event Handlers -------------------------------------------------------------------------------
    // Overrides ------------------------------------------------------------------------------------



    // Unity Inspectors -----------------------------------------------------------------------------
    [Header("HUD UI")]
    public RectTransform healthBar;
    public RectTransform swordBar;
    public RectTransform shieldBar;
    public Text coinText;

    // Unity Messages -------------------------------------------------------------------------------
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {

    }

    // Unity Coroutine ------------------------------------------------------------------------------
}
