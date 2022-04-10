using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : Zone
{
	// Outer Functions ------------------------------------------------------------------------------
	public void ShowItemInfo(int btnIndex) //���콺 Ŀ�� ���� ��(���� �ö������ ��) ȣ���   
	{
		if (btnIndex == 1)
		{
			npcText.text = healthItem.info;
		}
		else if (btnIndex == 2)
		{
			npcText.text = swordItem.info;
		}
		else if (btnIndex == 3)
		{
			npcText.text = shieldItem.info;
		}
	}

	public void CloseItemInfo() //���콺 Ŀ�� ���� ��(���� �ö������ ��) ȣ���   
	{
		npcText.text = middleText;
	}

	public void Buy(int btnIndex)
	{
		if (btnIndex == 1)
		{
			if (HasEnughCoin(healthItem) && !player.IsPowerFull)
			{
				player.ChangeCoin(-healthItem.price);
				ItemAry[0].SetActive(false);
				inventoryUI.TurnOnGunImg();
				player.hasGun = true;
			}
		}
		else if (btnIndex == 2)
		{
			if (HasEnughCoin(swordItem))
			{
				player.ChangeCoin(-swordItem.price);
				player.ChangeSword(curSwordNum);
			}
		}
		else if (btnIndex == 3)
		{
			Item shileditem = shieldItem;
			if (HasEnughCoin(shileditem) && !player.IsShieldFull)
			{
				player.ChangeCoin(-shileditem.price);
				player.ChangeShield(shileditem.damage);
			}
		}

		Prepare();
	}

	// Fields ---------------------------------------------------------------------------------------
	InventoryUI inventoryUI;

	// Functions ------------------------------------------------------------------------------------
	protected override void Prepare()
	{
		Debug.Log("�غ�");
		//�÷��̾��� ���⿡ ���� ���� ���⸦ ������
		curSwordNum = player.SwordNum + 1;

		EraseItem();

		//�����ۿ� ���� text ������Ʈ]
		healthName.text = healthItem.itemName;
		healthCost.text = healthItem.price + " G";
		swordName.text = swordItem.itemName;
		swordCost.text = swordItem.price + " G";
		shieldName.text = shieldItem.itemName;
		shieldCost.text = shieldItem.price + " G";
	}

	private void EraseItem()
	{
		/* ������ ���� ��� ���� ������ ���� */
		if (curSwordNum >= swordAry.Length)
			ItemAry[1].SetActive(false);
		else
			swordItem = swordAry[curSwordNum].GetComponent<Item>();

		/*  ���� �� ���� ���� ������ ���� */
		ItemAry[2].SetActive(!player.IsShieldFull);

	}

	private bool HasEnughCoin(Item item)
	{
		if (player.Coin < item.price)
		{
			npcText.text = coinLackText;
			return false;
		}
		else
			npcText.text = buyItemText;
		return true;
	}


	// Unity Inspectors -----------------------------------------------------------------------------
	[Header("UI")]
	//[SerializeField] private RectTransform zoneUI;
	public GameObject[] ItemAry;
	public Text healthName;
	public Text healthCost;
	public Text swordName;
	public Text swordCost;
	public Text shieldName;
	public Text shieldCost;

	[Header("Current Item Info")]
	[SerializeField]
	int curSwordNum = 0;//�Ǹ����� ���� ��ȣ


	[Header("All Item Info")]
	public Item healthItem;
	public Item swordItem;
	public Item shieldItem;
	public GameObject[] swordAry;
	public int maxSwordNum = 7;

	[Header("NPC Dialogue")]
	//[TextArea]
	//public string startText;
	[TextArea]
	public string coinLackText;
	[TextArea]
	public string buyItemText;
	[TextArea]
	public string middleText;//���� ���ٰ� �� �� ������ ���

	
	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		inventoryUI = GameManager.Instance.InventoryUI;

		zoneUI.anchoredPosition = Vector3.down * 1000;
		
	}
	private void Start()
	{

	}
}
