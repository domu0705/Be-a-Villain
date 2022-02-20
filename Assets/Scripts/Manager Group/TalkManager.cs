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

    public void Talk(int currentQuestNum, ObjectData obj)//playerMove���� isAction�� false�� �ȿ�����. �׷��� ��� �̾߱� �� �� �ִ� ����.
    {
        string dialogue = obj.GetTalkData(currentQuestNum, talkIndex);

        if (dialogue == null)//��Ⱑ ���̻� ���� �� (��ȭ�� ������ ��, ���� ���簡 ������ ��)
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
