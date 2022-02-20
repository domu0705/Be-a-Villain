using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    [Header("Talk UI")]
    public Text talkText;
    public RectTransform talkPanel;
    public RectTransform portraitImg;
    public RectTransform arrowImg;

    public PlayerMovement playerMovement;
    public int talkIndex = 0;


    private void Awake()
    {
        HidePanel();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void ShowPanel()
    {
        talkPanel.anchoredPosition = Vector3.zero;
    }

    void HidePanel()
    {
        talkPanel.anchoredPosition = Vector3.down * 500;// new Vector3(0, -500,0);
    }

    public void Talk(int currentQuestNum, ObjectData obj)//playerMove에서 isAction이 false면 안움직임. 그래서 계속 이야기 할 수 있는 것임.
    {
        string dialogue = obj.GetTalkData(currentQuestNum, talkIndex);

        if (dialogue == null)//얘기가 더이상 없을 때 (대화가 끝났을 때, 물건 조사가 끝났을 때)
        {
            HidePanel();

            talkIndex = 0;
            playerMovement.canMove = true;
            return;
        }

        talkIndex++;
        SetTalkText(dialogue);
        ShowPanel();
    }

    void SetTalkText(string dialogue)
    {
        talkText.text = dialogue;
    }
}
