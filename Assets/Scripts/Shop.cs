using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    /*UI*/
    public RectTransform shopUI;
    public GameObject[] ItemAry;
    public Text npcText;
    public Text healthName;
    public Text healthCost;
    public Text swordName;
    public Text swordCost;
    public Text shieldName;
    public Text shieldCost;

    Player player;

    [Header("Current Item Info")]
    [SerializeField]
    int curSwordNum = 0;//�Ǹ����� ���� ��ȣ


    [Header("All Item Info")]
    public Item healthItem;
    public Item swordItem;
    public Item shieldItem;
    public GameObject[] swordAry;
    public int maxSwordNum = 5;

    [Header("Shop NPC Dialogue")]
    [TextArea]
    public string hiText;
    [TextArea]
    public string coinLackText;
    [TextArea]
    public string buyItemText;
    [TextArea]
    public string middleText;//���� ���ٰ� �� �� ������ ���

    public void Enter(Player curPlayer)
    {
        player = curPlayer;
        player.StopPlayer();
        shopUI.anchoredPosition = Vector3.zero; //UI�� ȭ�� ���߾ӿ� ���� ��.
        npcText.text = hiText;

        PrepareItem();
    }

    void PrepareItem()
    {
        //�÷��̾��� ���⿡ ���� ���� ���⸦ ������
        curSwordNum = player.swordNum + 1;

        EraseItem();

        //�����ۿ� ���� text ������Ʈ]
        healthName.text = healthItem.itemName;
        healthCost.text = healthItem.price + " G";
        swordName.text = swordItem.itemName;
        swordCost.text = swordItem.price + " G";
        shieldName.text = shieldItem.itemName;
        shieldCost.text = shieldItem.price + " G";
    }

    void EraseItem()
    {
        /* ������ ���� ��� ���� ������ ���� */
        if (curSwordNum > maxSwordNum) 
            ItemAry[1].SetActive(false);
        else
            swordItem = swordAry[curSwordNum].GetComponent<Item>();

        /* ü�� �� ���� ü�� ������ ���� */
        if(player.curHealth >= player.maxHealth)
        {
            ItemAry[0].SetActive(false);
        }
        else
        {
            ItemAry[0].SetActive(true);
        }

        /*  ���� �� ���� ���� ������ ���� */
        if (player.curShieldPower >= player.maxShield)
        {
            ItemAry[2].SetActive(false);
        }

        
    }

    public void Buy(int btnIndex)
    {
        if (btnIndex == 1)
        {
            if (HasEnughCoin(healthItem) && (player.curHealth < player.maxHealth))
            {
                player.ChangeCoin(-healthItem.price);
                player.ChangeHealth(healthItem.value);
            }
        }
        else if (btnIndex == 2)
        {
            if (HasEnughCoin(swordItem))
            {
                player.ChangeCoin(-swordItem.price);
                player.ChangeSword();
            }
        }
        else if (btnIndex == 3)
        {
            if (HasEnughCoin(shieldItem) && (player.curShieldPower < player.maxShield))
            {
                player.ChangeShield(shieldItem.value);
            }
        }

        PrepareItem();
    }

    bool HasEnughCoin(Item item)
    {
        if (player.coin < item.price)
        {
            npcText.text = hiText;
            return false;
        }
        else
            npcText.text = buyItemText;
        return true;
    }

    public void ShowItemInfo(int btnIndex) //���콺 Ŀ�� ���� ��(���� �ö������ ��) ȣ���   
    {
        if(btnIndex == 1)
        {
            npcText.text = healthItem.info;
        }
        else if(btnIndex == 2)
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

    public void Exit()
    {
        shopUI.anchoredPosition = Vector3.down * 1000;
        player.ResumePlayer();
        player = null;
    }
}
