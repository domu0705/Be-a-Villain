using System.Collections;
using System.Collections.Generic;//for dictionary
using UnityEngine;
using UnityEngine.UI;


public class TalkData : Singleton<TalkData>
{
    // ----------------------------------------------------------------------------------------------

    // FSM ------------------------------------------------------------------------------------------

    // Definitions ----------------------------------------------------------------------------------
    // Outer Properties -----------------------------------------------------------------------------
    public Dictionary<int, Dictionary<int, string[]>> talkData; //int:quest num, int:npc num, string:dialogue

    // Outer Functions ------------------------------------------------------------------------------
    public string GetTalkData(int questNum, int npcNum, int talkIndex)
    {
        Debug.Log("GetTalkData() | questNum=" + questNum + "npcNum" + npcNum + " talkIndex =" + talkIndex);

        if (talkIndex < talkData[questNum][npcNum].Length)
            return talkData[questNum][npcNum][talkIndex];
        else
            return null;
    }

    // Events ---------------------------------------------------------------------------------------



    // Properties : caching -------------------------------------------------------------------------
    // Inner Properties -----------------------------------------------------------------------------
    private int alphaNum = 10000;// 퀘스트 번호에 곱해줄 임의의 큰 수 

    // Inner Functions ------------------------------------------------------------------------------
    // Coroutine ------------------------------------------------------------------------------------
    // Event Handlers -------------------------------------------------------------------------------
    // Overrides ------------------------------------------------------------------------------------



    // Unity Inspectors -----------------------------------------------------------------------------


    // Unity Messages -------------------------------------------------------------------------------

    private void Awake()
    {
        talkData = new Dictionary<int, Dictionary<int, string[]>>();

        GenerateTalkData();
        //GeneratePortraitData();
    }

    void GenerateTalkData()
    {
        talkData.Add(0, new Dictionary<int, string[]> {
            {0, new string[] { "초록: 퀘스트 0번 대사.", "초록 :퉤스트 0번 대사 두번째야!" } },
            {1, new string[] { "검정: 이거 퀘스트 0번 대사", "검정: 퉤스트 0번 대사 두번째야!" } }


        });
        talkData.Add(1, new Dictionary<int, string[]> {
            {0, new string[] { "초록: 퀘스트 1번 대사.", "초록 :퉤스트 1번 대사 두번째야!" } },
            {1, new string[] { "검정: 이거 퀘스트 1번 대사", "검정: 퉤스트 1번 대사 두번째야!" } }


        });
    }

}
