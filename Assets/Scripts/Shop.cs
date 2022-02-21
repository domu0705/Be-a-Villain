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
    int curSwordNum = 0;//판매중인 무기 번호


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
    public string middleText;//물건 고르다가 말 때 나오는 대사

    public void Enter(Player curPlayer)
    {
        player = curPlayer;
        player.StopPlayer();
        shopUI.anchoredPosition = Vector3.zero; //UI가 화면 정중앙에 오게 함.
        npcText.text = hiText;

        PrepareItem();
    }

    void PrepareItem()
    {
        //플레이어의 무기에 따라 다음 무기를 보여줌
        curSwordNum = player.swordNum + 1;

        EraseItem();

        //아이템에 따라 text 업데이트]
        healthName.text = healthItem.itemName;
        healthCost.text = healthItem.price + " G";
        swordName.text = swordItem.itemName;
        swordCost.text = swordItem.price + " G";
        shieldName.text = shieldItem.itemName;
        shieldCost.text = shieldItem.price + " G";
    }

    void EraseItem()
    {
        /* 마지막 무기 사면 무기 아이템 삭제 */
        if (curSwordNum > maxSwordNum) 
            ItemAry[1].SetActive(false);
        else
            swordItem = swordAry[curSwordNum].GetComponent<Item>();

        /* 체력 다 차면 체력 아이템 삭제 */
        if(player.curHealth >= player.maxHealth)
        {
            ItemAry[0].SetActive(false);
        }
        else
        {
            ItemAry[0].SetActive(true);
        }

        /*  방어력 다 차면 방어력 아이템 삭제 */
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

    public void ShowItemInfo(int btnIndex) //마우스 커서 오버 시(위에 올라와있을 때) 호출됨   
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

    public void CloseItemInfo() //마우스 커서 오버 시(위에 올라와있을 때) 호출됨   
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
