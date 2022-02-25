using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkUI : MonoBehaviour
{
    // Properties -----------------------------------------------------------------------------------
    public static TalkUI Instance { get; protected set; }

    // Methods --------------------------------------------------------------------------------------
    public void Talk(int currentQuestNum, ObjectData obj)
    {
        //playerMove���� isAction�� false�� �ȿ�����. �׷��� ��� �̾߱� �� �� �ִ� ����.
        string dialogue = obj.GetTalkData(currentQuestNum, talkIndex);

        if (dialogue == null)//��Ⱑ ���̻� ���� �� (��ȭ�� ������ ��, ���� ���簡 ������ ��)
        {
            hidePanel();

            talkIndex = 0;
            playerMovement.canMove = true;
            return;
        }

        talkIndex++;
        setTalkText(dialogue);
        showPanel();
    }


    
    // Functions ------------------------------------------------------------------------------------
    private void setTalkText(string dialogue)
    {
        talkText.text = dialogue;
    }

    private void showPanel()
    {
        talkPanel.anchoredPosition = Vector3.zero;
    }

    private void hidePanel()
    {
        talkPanel.anchoredPosition = Vector3.down * 500;// new Vector3(0, -500,0);
    }



    // Unity Inspectors -----------------------------------------------------------------------------
    [Header("Talk UI")]
    [SerializeField] private Text talkText;
    [SerializeField] private RectTransform talkPanel;
    [SerializeField] private RectTransform portraitImg;
    [SerializeField] private RectTransform arrowImg;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private int talkIndex = 0;

    // Unity Messages -------------------------------------------------------------------------------
    private void Awake()
    {
        hidePanel();

        if (Instance == null)
            Instance = this;
        else Debug.LogError("must be single instance.");
    }
    private void Start()
    {

    }
	private void OnDestroy()
	{
        if (Instance == this)
            Instance = null;
	}
}
