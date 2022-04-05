using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkUI : Singleton<TalkUI>
{
    // Properties -----------------------------------------------------------------------------------

    // Outer Functions ------------------------------------------------------------------------------
    public void SetTalkText(string dialogue)
    {
        talkText.text = dialogue;
    }

    public void ShowPanel()
    {
        talkPanel.anchoredPosition = Vector3.zero;
    }

    public void HidePanel()
    {
        talkPanel.anchoredPosition = Vector3.down * 500;// new Vector3(0, -500,0);
    }
    // Functions ------------------------------------------------------------------------------------


    // Unity Inspectors -----------------------------------------------------------------------------
    [Header("Talk UI")]
    [SerializeField] private Text talkText;
    [SerializeField] private RectTransform talkPanel;
    [SerializeField] private RectTransform portraitImg;
    [SerializeField] private RectTransform arrowImg;

    // Unity Messages -------------------------------------------------------------------------------
    private void Awake()
    {
        HidePanel();
    }
    private void Start()
    {

    }
}
