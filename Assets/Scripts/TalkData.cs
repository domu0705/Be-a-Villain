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
            {1, new string[] { "토끼: 이거 퀘스트 0번 대사", "토끼: 퉤스트 0번 대사 두번째야!" } },
            {2, new string[] { ": 이거 퀘스트 0번 대사", "검정: 퉤스트 0번 대사 두번째야!" } },
            {3, new string[] { "검정: 이거 퀘스트 0번 대사"} }

        });
        talkData.Add(1, new Dictionary<int, string[]> { //퀘스트 1
            {0, new string[] { "초록: 퀘스트 1번 대사.", "초록 :퉤스트 1번 대사 두번째야!" } },
            {1, new string[] { "어서오세요 여긴 시작의 숲 입구랍니다! 아 적 NPC 를 하러 왔다고? ", 
                "어디보자… 병사 36번 이 오늘 급한일이 생겨서 월차를 쓴다는데 이 자리가 좋겠네. 들어가봐!"} },
            {2, new string[] { "용사님은 이곳으로 입장할 수 없습니다.", "아 병사라고? 여긴 약한 NPC 들은 필요없어." } },
            {3, new string[] { "여긴 자네같은 NPC 가 오는 곳이 아니라네." } }


        });
        talkData.Add(2, new Dictionary<int, string[]> {//퀘스트 2
            {0, new string[] { "초록: 퀘스트 2번 대사." } },
            {1, new string[] { "토끼: 퀘스트 2번 대사" } },
            {2, new string[] { "안녕하십니까. 시작의 땅 중간 보스자리로 새로 오신 분이군요.", "좋습니다. 건투를 빕니다." } },
            {3, new string[] { "하양: 퀘스트 2번 대사" } }


        });
        talkData.Add(3, new Dictionary<int, string[]> {
            {0, new string[] { "초록: 퀘스트 3번 대사."} },
            {1, new string[] { "토끼: 이거 퀘스트 3번 대사" } },
            {2, new string[] { "검정: 이거 퀘스트 3번 대사"} },
            {3, new string[] { "오셨습니까? 모두들 마지막 보스님을 기다리고 있습니다.","이 게임이 만만하지 않다는 걸 보여주십시오." } }


        });
    }

}
